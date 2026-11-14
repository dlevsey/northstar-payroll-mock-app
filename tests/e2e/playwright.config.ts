import { defineConfig, devices } from '@playwright/test';

export default defineConfig({
  testDir: './specs',
  fullyParallel: false,
  workers: 1,
  forbidOnly: Boolean(process.env['CI']),
  retries: 0,
  reporter: [['list'], ['html', { open: 'never' }]],
  use: {
    baseURL: 'http://127.0.0.1:4200',
    trace: 'retain-on-failure',
    screenshot: 'only-on-failure'
  },
  projects: [
    {
      name: 'chromium',
      use: { ...devices['Desktop Chrome'] }
    }
  ],
  webServer: [
    {
      command:
        'dotnet run --project ../../tools/Northstar.FakePayrollProvider/Northstar.FakePayrollProvider.csproj --no-launch-profile --urls http://127.0.0.1:5130',
      url: 'http://127.0.0.1:5130/health',
      timeout: 120_000,
      reuseExistingServer: !process.env['CI']
    },
    {
      command:
        'ConnectionStrings__Northstar="Data Source=/tmp/northstar-e2e-$$.db" dotnet run --project ../../src/Northstar.Api/Northstar.Api.csproj --no-launch-profile --urls http://127.0.0.1:5120',
      url: 'http://127.0.0.1:5120/health',
      timeout: 120_000,
      reuseExistingServer: !process.env['CI']
    },
    {
      command: 'npm --prefix ../../src/Northstar.Web start -- --host 127.0.0.1 --port 4200',
      url: 'http://127.0.0.1:4200',
      timeout: 120_000,
      reuseExistingServer: !process.env['CI']
    }
  ]
});
