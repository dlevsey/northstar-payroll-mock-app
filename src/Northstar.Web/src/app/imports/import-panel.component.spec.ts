import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { ImportBatch, ImportRow } from './import.models';
import { ImportPanelComponent } from './import-panel.component';

describe('ImportPanelComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ImportPanelComponent],
      providers: [provideHttpClient(), provideHttpClientTesting()],
    }).compileComponents();
  });

  afterEach(() => {
    vi.useRealTimers();
  });

  it('starts a mixed import, polls until terminal, and renders row errors', async () => {
    vi.useFakeTimers();
    const fixture = TestBed.createComponent(ImportPanelComponent);
    const http = TestBed.inject(HttpTestingController);
    fixture.detectChanges();

    http.expectOne('/api/import-batches').flush([]);
    fixture.detectChanges();

    const button: HTMLButtonElement = fixture.nativeElement.querySelector(
      '[data-testid="import-mixed"]',
    );
    button.click();

    const create = http.expectOne('/api/import-batches');
    expect(create.request.method).toBe('POST');
    expect(create.request.body).toEqual({
      periodStart: '2026-08-24',
      periodEnd: '2026-08-30',
      providerScenario: 'mixed',
    });
    expect(create.request.headers.get('Idempotency-Key')).toBeTruthy();
    create.flush({ ...batch, status: 'Pending' });

    await vi.advanceTimersByTimeAsync(0);
    http.expectOne(`/api/import-batches/${batch.id}`).flush({
      ...batch,
      status: 'Processing',
    });

    await vi.advanceTimersByTimeAsync(1_000);
    http.expectOne(`/api/import-batches/${batch.id}`).flush(batch);
    http.expectOne(`/api/import-batches/${batch.id}/errors`).flush([rowError]);
    fixture.detectChanges();

    expect(
      fixture.nativeElement.querySelector('[data-testid="import-status"]').textContent,
    ).toContain('CompletedWithErrors');
    expect(fixture.nativeElement.querySelectorAll('[data-testid="import-error-row"]')).toHaveLength(
      1,
    );
    http.verify();
  });

  it('shows duplicate rows and retries a retryable failed batch', async () => {
    vi.useFakeTimers();
    const fixture = TestBed.createComponent(ImportPanelComponent);
    const http = TestBed.inject(HttpTestingController);
    const failedBatch: ImportBatch = {
      ...batch,
      status: 'Failed',
      totalRows: 1,
      importedRows: 0,
      errorRows: 0,
      duplicateRows: 1,
      failureCode: 'PROVIDER_TRANSIENT_FAILURE',
      failureMessage: 'The provider remained unavailable.',
      canRetry: true,
    };

    fixture.detectChanges();
    http.expectOne('/api/import-batches').flush([failedBatch]);
    http.expectOne(`/api/import-batches/${batch.id}/errors`).flush([rowError]);
    fixture.detectChanges();

    expect(
      fixture.nativeElement.querySelector('[data-testid="import-duplicate-count"]').textContent,
    ).toContain('1');

    const retryButton: HTMLButtonElement = fixture.nativeElement.querySelector(
      '[data-testid="retry-import"]',
    );
    retryButton.click();

    const retry = http.expectOne(`/api/import-batches/${batch.id}/retry`);
    expect(retry.request.method).toBe('POST');
    expect(retry.request.headers.get('Idempotency-Key')).toBeTruthy();
    retry.flush({
      ...failedBatch,
      status: 'Pending',
      failureCode: null,
      failureMessage: null,
      canRetry: false,
    });

    await vi.advanceTimersByTimeAsync(0);
    http.expectOne(`/api/import-batches/${batch.id}`).flush({
      ...batch,
      status: 'Completed',
      errorRows: 0,
    });
    fixture.detectChanges();

    expect(
      fixture.nativeElement.querySelector('[data-testid="import-status"]').textContent,
    ).toContain('Completed');
    http.verify();
  });
});

const batch: ImportBatch = {
  id: 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbb001',
  organizationId: '11111111-1111-1111-1111-111111111111',
  periodStart: '2026-08-24',
  periodEnd: '2026-08-30',
  providerScenario: 'mixed',
  status: 'CompletedWithErrors',
  totalRows: 3,
  importedRows: 2,
  errorRows: 1,
  duplicateRows: 0,
  attemptCount: 1,
  failureCode: null,
  createdAt: '2026-08-30T13:00:00Z',
  completedAt: '2026-08-30T13:00:02Z',
  failureMessage: null,
  canRetry: false,
  version: 3,
};

const rowError: ImportRow = {
  id: 'cccccccc-cccc-cccc-cccc-ccccccccc001',
  importBatchId: batch.id,
  rowNumber: 3,
  externalTimeEntryId: 'DEMO-DUPLICATE-001',
  status: 'Rejected',
  errorCode: 'DUPLICATE_TIME_ENTRY',
  errorMessage: 'The external time-entry ID already exists.',
};
