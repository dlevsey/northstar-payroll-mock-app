import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateImportBatchRequest, ImportBatch, ImportRow } from './import.models';

@Injectable({ providedIn: 'root' })
export class ImportApiService {
  private readonly http = inject(HttpClient);

  listBatches(): Observable<ImportBatch[]> {
    return this.http.get<ImportBatch[]>('/api/import-batches');
  }

  createBatch(request: CreateImportBatchRequest, idempotencyKey: string): Observable<ImportBatch> {
    return this.http.post<ImportBatch>('/api/import-batches', request, {
      headers: new HttpHeaders({ 'Idempotency-Key': idempotencyKey }),
    });
  }

  getBatch(batchId: string): Observable<ImportBatch> {
    return this.http.get<ImportBatch>(`/api/import-batches/${batchId}`);
  }

  getErrors(batchId: string): Observable<ImportRow[]> {
    return this.http.get<ImportRow[]>(`/api/import-batches/${batchId}/errors`);
  }

  retryBatch(batchId: string, idempotencyKey: string): Observable<ImportBatch> {
    return this.http.post<ImportBatch>(
      `/api/import-batches/${batchId}/retry`,
      {},
      {
        headers: new HttpHeaders({ 'Idempotency-Key': idempotencyKey }),
      },
    );
  }
}
