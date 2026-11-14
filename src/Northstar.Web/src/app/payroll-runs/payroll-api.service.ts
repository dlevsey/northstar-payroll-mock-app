import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PayrollRun } from './payroll-run.models';

const demoProjectId = 'a2000000-0000-0000-0000-000000000001';
const carpenterClassificationId = 'a3000000-0000-0000-0000-000000000003';

@Injectable({ providedIn: 'root' })
export class PayrollApiService {
  private readonly http = inject(HttpClient);

  listRuns(): Observable<PayrollRun[]> {
    return this.http.get<PayrollRun[]>('/api/payroll-runs');
  }

  calculate(runId: string, idempotencyKey: string): Observable<PayrollRun> {
    return this.http.post<PayrollRun>(
      `/api/payroll-runs/${runId}/calculate`,
      {},
      { headers: new HttpHeaders({ 'Idempotency-Key': idempotencyKey }) }
    );
  }

  addMissingCarpenterRate(): Observable<unknown> {
    return this.http.post('/api/wage-rates', {
      scope: 'ProjectClassification',
      jurisdictionCode: 'MN',
      projectId: demoProjectId,
      employeeId: null,
      classificationId: carpenterClassificationId,
      effectiveFrom: '2026-08-24',
      effectiveTo: null,
      baseRate: 30,
      fringeRate: 7,
      overtimeMultiplier: 1.5,
      priority: 0,
      sourceReference: 'UI-DEMO-CORRECTION'
    });
  }

  approve(run: PayrollRun): Observable<PayrollRun> {
    return this.http.post<PayrollRun>(`/api/payroll-runs/${run.id}/approve`, {
      expectedVersion: run.version
    });
  }
}
