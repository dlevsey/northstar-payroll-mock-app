export type ImportBatchStatus =
  'Pending' | 'Processing' | 'Completed' | 'CompletedWithErrors' | 'Failed';

export type ProviderScenario = 'success' | 'mixed' | 'transient';

export interface ImportBatch {
  id: string;
  organizationId: string;
  periodStart: string;
  periodEnd: string;
  providerScenario: ProviderScenario | string;
  status: ImportBatchStatus;
  totalRows: number;
  importedRows: number;
  errorRows: number;
  duplicateRows: number;
  attemptCount: number;
  failureCode: string | null;
  createdAt: string;
  completedAt: string | null;
  failureMessage: string | null;
  canRetry: boolean;
  version: number;
}

export interface ImportRow {
  id: string;
  importBatchId: string;
  rowNumber: number;
  externalTimeEntryId: string | null;
  status: string;
  errorCode: string | null;
  errorMessage: string | null;
}

export interface CreateImportBatchRequest {
  periodStart: string;
  periodEnd: string;
  providerScenario: ProviderScenario;
}

export function isActiveImportStatus(status: ImportBatchStatus): boolean {
  return status === 'Pending' || status === 'Processing';
}
