using System.Net;
using System.Text;
using Microsoft.Extensions.Options;
using Northstar.Application.Abstractions;
using Northstar.Infrastructure.Integrations;

namespace Northstar.Api.IntegrationTests;

public sealed class FakePayrollProviderClientTests
{
    private static readonly Uri ProviderBaseAddress = new("http://provider.test/");

    [Fact]
    public async Task GetTimeEntriesAsync_RetriesTwoRateLimitsThenReturnsRecords()
    {
        var handler = new StubHttpMessageHandler((call, _, _) =>
            Task.FromResult(call < 3
                ? new HttpResponseMessage(HttpStatusCode.TooManyRequests)
                : JsonResponse(SuccessPayload)));
        using var httpClient = CreateHttpClient(handler);
        var client = CreateClient(httpClient, maxAttempts: 3);

        var records = await client.GetTimeEntriesAsync(Request(), CancellationToken.None);

        Assert.Equal(3, handler.CallCount);
        var record = Assert.Single(records);
        Assert.Equal("A-TIME-IMPORT-001", record.ExternalTimeEntryId);
        Assert.Equal("A-EMP-001", record.ExternalEmployeeId);
    }

    [Fact]
    public async Task GetTimeEntriesAsync_MapsMalformedJsonToContractFailure()
    {
        var handler = new StubHttpMessageHandler((_, _, _) =>
            Task.FromResult(JsonResponse("{\"records\":[")));
        using var httpClient = CreateHttpClient(handler);
        var client = CreateClient(httpClient);

        var exception = await Assert.ThrowsAsync<PayrollProviderContractException>(() =>
            client.GetTimeEntriesAsync(Request(), CancellationToken.None));

        Assert.Contains("malformed JSON", exception.Message, StringComparison.Ordinal);
        Assert.Equal(1, handler.CallCount);
    }

    [Fact]
    public async Task GetTimeEntriesAsync_MapsNullRecordToContractFailure()
    {
        var handler = new StubHttpMessageHandler((_, _, _) =>
            Task.FromResult(JsonResponse("{\"records\":[null],\"nextCursor\":null}")));
        using var httpClient = CreateHttpClient(handler);
        var client = CreateClient(httpClient);

        var exception = await Assert.ThrowsAsync<PayrollProviderContractException>(() =>
            client.GetTimeEntriesAsync(Request(), CancellationToken.None));

        Assert.Contains("null record", exception.Message, StringComparison.Ordinal);
        Assert.Equal(1, handler.CallCount);
    }

    [Fact]
    public async Task GetTimeEntriesAsync_FollowsEncodedPaginationCursor()
    {
        var handler = new StubHttpMessageHandler((call, request, _) =>
        {
            if (call == 1)
            {
                Assert.DoesNotContain("cursor=", request.RequestUri!.Query, StringComparison.Ordinal);
                return Task.FromResult(JsonResponse(PageOnePayload));
            }

            Assert.Contains("cursor=page-2", request.RequestUri!.Query, StringComparison.Ordinal);
            return Task.FromResult(JsonResponse(SuccessPayload));
        });
        using var httpClient = CreateHttpClient(handler);
        var client = CreateClient(httpClient);

        var records = await client.GetTimeEntriesAsync(Request(), CancellationToken.None);

        Assert.Equal(2, handler.CallCount);
        Assert.Equal(2, records.Count);
        Assert.Equal("A-TIME-PAGE-001", records[0].ExternalTimeEntryId);
        Assert.Equal("A-TIME-IMPORT-001", records[1].ExternalTimeEntryId);
    }

    [Fact]
    public async Task GetTimeEntriesAsync_RejectsRepeatedPaginationCursor()
    {
        var handler = new StubHttpMessageHandler((_, _, _) =>
            Task.FromResult(JsonResponse(PageOnePayload)));
        using var httpClient = CreateHttpClient(handler);
        var client = CreateClient(httpClient);

        var exception = await Assert.ThrowsAsync<PayrollProviderContractException>(() =>
            client.GetTimeEntriesAsync(Request(), CancellationToken.None));

        Assert.Contains("repeated a pagination cursor", exception.Message, StringComparison.Ordinal);
        Assert.Equal(2, handler.CallCount);
    }

    [Fact]
    public async Task GetTimeEntriesAsync_PropagatesCallerCancellation()
    {
        var requestEntered = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var handler = new StubHttpMessageHandler(async (_, _, cancellationToken) =>
        {
            requestEntered.SetResult();
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            return JsonResponse(SuccessPayload);
        });
        using var httpClient = CreateHttpClient(handler);
        var client = CreateClient(httpClient, maxAttempts: 3);
        using var cancellationSource = new CancellationTokenSource();

        var operation = client.GetTimeEntriesAsync(Request(), cancellationSource.Token);
        await requestEntered.Task;
        await cancellationSource.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => operation);
        Assert.Equal(1, handler.CallCount);
    }

    [Fact]
    public async Task GetTimeEntriesAsync_MapsAttemptTimeoutToTransientFailure()
    {
        var handler = new StubHttpMessageHandler(async (_, _, cancellationToken) =>
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            return JsonResponse(SuccessPayload);
        });
        using var httpClient = CreateHttpClient(handler);
        var client = CreateClient(httpClient, maxAttempts: 2, attemptTimeoutMilliseconds: 25);

        var exception = await Assert.ThrowsAsync<PayrollProviderTransientException>(() =>
            client.GetTimeEntriesAsync(Request(), CancellationToken.None));

        Assert.Contains("timed out", exception.Message, StringComparison.Ordinal);
        Assert.Equal(2, handler.CallCount);
    }

    [Fact]
    public async Task GetTimeEntriesAsync_TimesOutAndRetriesWhenResponseBodyStalls()
    {
        var handler = new StubHttpMessageHandler((_, _, _) =>
            Task.FromResult(StallingJsonResponse()));
        using var httpClient = CreateHttpClient(handler);
        var client = CreateClient(httpClient, maxAttempts: 2, attemptTimeoutMilliseconds: 25);

        var exception = await Assert.ThrowsAsync<PayrollProviderTransientException>(() =>
            client.GetTimeEntriesAsync(Request(), CancellationToken.None));

        Assert.Contains("timed out", exception.Message, StringComparison.Ordinal);
        Assert.Equal(2, handler.CallCount);
    }

    private static FakePayrollProviderClient CreateClient(
        HttpClient httpClient,
        int maxAttempts = 1,
        int attemptTimeoutMilliseconds = 1_000) =>
        new(
            httpClient,
            Options.Create(new PayrollProviderOptions
            {
                BaseUrl = ProviderBaseAddress,
                MaxAttempts = maxAttempts,
                BaseRetryDelayMilliseconds = 0,
                AttemptTimeoutMilliseconds = attemptTimeoutMilliseconds
            }));

    private static HttpClient CreateHttpClient(HttpMessageHandler handler) =>
        new(handler)
        {
            BaseAddress = ProviderBaseAddress,
            Timeout = Timeout.InfiniteTimeSpan
        };

    private static ProviderImportRequest Request() =>
        new(
            "DEMO-A",
            new DateOnly(2026, 8, 24),
            new DateOnly(2026, 8, 30),
            "success");

    private static HttpResponseMessage JsonResponse(string content) =>
        new(HttpStatusCode.OK)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };

    private static HttpResponseMessage StallingJsonResponse()
    {
        var content = new StreamContent(new StallingJsonStream());
        content.Headers.ContentType = new("application/json");
        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = content
        };
    }

    private const string SuccessPayload = """
        {
          "records": [
            {
              "externalTimeEntryId": "A-TIME-IMPORT-001",
              "externalEmployeeId": "A-EMP-001",
              "externalProjectId": "A-PRJ-001",
              "classificationCode": "ELEC",
              "workDate": "2026-08-24",
              "regularHours": 8,
              "overtimeHours": 0,
              "shiftCode": "DAY"
            }
          ],
          "nextCursor": null
        }
        """;

    private const string PageOnePayload = """
        {
          "records": [
            {
              "externalTimeEntryId": "A-TIME-PAGE-001",
              "externalEmployeeId": "A-EMP-001",
              "externalProjectId": "A-PRJ-001",
              "classificationCode": "ELEC",
              "workDate": "2026-08-24",
              "regularHours": 8,
              "overtimeHours": 0,
              "shiftCode": "DAY"
            }
          ],
          "nextCursor": "page-2"
        }
        """;

    private sealed class StubHttpMessageHandler(
        Func<int, HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync)
        : HttpMessageHandler
    {
        private readonly Func<int, HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _sendAsync = sendAsync;
        private int _callCount;

        public int CallCount => Volatile.Read(ref _callCount);

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var call = Interlocked.Increment(ref _callCount);
            return _sendAsync(call, request, cancellationToken);
        }
    }

    private sealed class StallingJsonStream : Stream
    {
        private static readonly byte[] Prefix = Encoding.UTF8.GetBytes("{\"records\":[");
        private int _position;

        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => throw new NotSupportedException();

        public override long Position
        {
            get => _position;
            set => throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count) =>
            throw new NotSupportedException();

        public override async ValueTask<int> ReadAsync(
            Memory<byte> buffer,
            CancellationToken cancellationToken = default)
        {
            if (_position < Prefix.Length)
            {
                var count = Math.Min(buffer.Length, Prefix.Length - _position);
                Prefix.AsMemory(_position, count).CopyTo(buffer);
                _position += count;
                return count;
            }

            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            return 0;
        }

        public override long Seek(long offset, SeekOrigin origin) =>
            throw new NotSupportedException();

        public override void SetLength(long value) => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count) =>
            throw new NotSupportedException();

        public override void Flush()
        {
        }
    }
}
