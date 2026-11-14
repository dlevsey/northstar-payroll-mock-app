import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { App } from './app';
import { PayrollRun } from './payroll-runs/payroll-run.models';

describe('App', () => {
  it('renders the seeded payroll run returned by the API', async () => {
    await TestBed.configureTestingModule({
      imports: [App],
      providers: [provideHttpClient(), provideHttpClientTesting()]
    }).compileComponents();

    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    const http = TestBed.inject(HttpTestingController);
    http.expectOne('/api/payroll-runs').flush([run]);
    http.expectOne('/api/import-batches').flush([]);
    fixture.detectChanges();

    expect(fixture.nativeElement.querySelector('[data-testid="run-status"]').textContent).toContain(
      'Draft'
    );
    expect(fixture.nativeElement.textContent).toContain('Aug 24–Aug 30, 2026');
    http.verify();
  });
});

const run: PayrollRun = {
  id: 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaa1001',
  organizationId: '11111111-1111-1111-1111-111111111111',
  periodStart: '2026-08-24',
  periodEnd: '2026-08-30',
  status: 'Draft',
  calculationVersion: 0,
  version: 1,
  approvedAt: null,
  lines: [],
  findings: []
};
