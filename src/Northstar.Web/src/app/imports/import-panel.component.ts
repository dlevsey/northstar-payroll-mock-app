import { DatePipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, DestroyRef, computed, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Subscription, switchMap, takeWhile, timer } from 'rxjs';
import { ImportApiService } from './import-api.service';
import {
  ImportBatch,
  ImportBatchStatus,
  ImportRow,
  isActiveImportStatus,
  ProviderScenario,
} from './import.models';

const periodStart = '2026-08-24';
const periodEnd = '2026-08-30';
const pollIntervalMilliseconds = 1_000;

@Component({
  selector: 'app-import-panel',
  imports: [DatePipe],
  templateUrl: './import-panel.component.html',
  styleUrl: './import-panel.component.scss',
})
export class ImportPanelComponent implements OnInit {
  private readonly importApi = inject(ImportApiService);
  private readonly destroyRef = inject(DestroyRef);
  private pollingSubscription?: Subscription;
  private rowErrorsSubscription?: Subscription;

  protected readonly batches = signal<ImportBatch[]>([]);
  protected readonly selectedBatch = signal<ImportBatch | null>(null);
  protected readonly rowErrors = signal<ImportRow[]>([]);
  protected readonly loading = signal(true);
  protected readonly action = signal<string | null>(null);
  protected readonly error = signal<string | null>(null);
  protected readonly isActive = computed(() => {
    const batch = this.selectedBatch();
    return batch !== null && isActiveImportStatus(batch.status);
  });

  ngOnInit(): void {
    this.loadBatches();
  }

  protected startImport(providerScenario: ProviderScenario): void {
    this.pollingSubscription?.unsubscribe();
    this.rowErrorsSubscription?.unsubscribe();
    this.action.set(providerScenario);
    this.error.set(null);
    this.rowErrors.set([]);

    this.importApi
      .createBatch({ periodStart, periodEnd, providerScenario }, globalThis.crypto.randomUUID())
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (batch) => {
          this.selectBatch(batch);
          if (isActiveImportStatus(batch.status)) {
            this.pollBatch(batch.id);
          } else {
            this.finishBatch(batch);
          }
        },
        error: (httpError: HttpErrorResponse) => this.failAction(httpError),
      });
  }

  protected retryImport(batch: ImportBatch): void {
    this.pollingSubscription?.unsubscribe();
    this.rowErrorsSubscription?.unsubscribe();
    this.action.set('retry');
    this.error.set(null);
    this.rowErrors.set([]);

    this.importApi
      .retryBatch(batch.id, globalThis.crypto.randomUUID())
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (retried) => {
          this.selectBatch(retried);
          if (isActiveImportStatus(retried.status)) {
            this.pollBatch(retried.id);
          } else {
            this.finishBatch(retried);
          }
        },
        error: (httpError: HttpErrorResponse) => this.failAction(httpError),
      });
  }

  protected selectFromHistory(batch: ImportBatch): void {
    this.pollingSubscription?.unsubscribe();
    this.rowErrorsSubscription?.unsubscribe();
    this.action.set(null);
    this.error.set(null);
    this.rowErrors.set([]);
    this.selectedBatch.set(batch);

    if (isActiveImportStatus(batch.status)) {
      this.pollBatch(batch.id);
    } else {
      this.loadErrors(batch);
    }
  }

  protected statusClass(status: ImportBatchStatus): string {
    switch (status) {
      case 'Completed':
        return 'complete';
      case 'CompletedWithErrors':
        return 'warning';
      case 'Failed':
        return 'failed';
      default:
        return 'active';
    }
  }

  private loadBatches(): void {
    this.loading.set(true);
    this.error.set(null);

    this.importApi
      .listBatches()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (batches) => {
          const ordered = this.orderNewestFirst(batches);
          this.batches.set(ordered);
          this.loading.set(false);

          const latest = ordered[0] ?? null;
          this.selectedBatch.set(latest);
          if (!latest) return;

          if (isActiveImportStatus(latest.status)) {
            this.pollBatch(latest.id);
          } else {
            this.loadErrors(latest);
          }
        },
        error: (httpError: HttpErrorResponse) => {
          this.loading.set(false);
          this.failAction(httpError);
        },
      });
  }

  private pollBatch(batchId: string): void {
    this.pollingSubscription?.unsubscribe();
    this.pollingSubscription = timer(0, pollIntervalMilliseconds)
      .pipe(
        switchMap(() => this.importApi.getBatch(batchId)),
        takeWhile((batch) => isActiveImportStatus(batch.status), true),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe({
        next: (batch) => {
          this.selectBatch(batch);
          if (!isActiveImportStatus(batch.status)) {
            this.finishBatch(batch);
          }
        },
        error: (httpError: HttpErrorResponse) => this.failAction(httpError),
      });
  }

  private selectBatch(batch: ImportBatch): void {
    this.selectedBatch.set(batch);
    this.batches.update((current) =>
      this.orderNewestFirst([batch, ...current.filter((item) => item.id !== batch.id)]),
    );
  }

  private finishBatch(batch: ImportBatch): void {
    this.action.set(null);
    this.loadErrors(batch);
  }

  private loadErrors(batch: ImportBatch): void {
    this.rowErrorsSubscription?.unsubscribe();

    if (batch.errorRows === 0 && batch.duplicateRows === 0 && batch.status !== 'Failed') {
      this.rowErrors.set([]);
      return;
    }

    this.rowErrorsSubscription = this.importApi
      .getErrors(batch.id)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (errors) => this.rowErrors.set(errors),
        error: (httpError: HttpErrorResponse) => this.failAction(httpError),
      });
  }

  private orderNewestFirst(batches: ImportBatch[]): ImportBatch[] {
    return [...batches].sort(
      (left, right) => Date.parse(right.createdAt) - Date.parse(left.createdAt),
    );
  }

  private failAction(httpError: HttpErrorResponse): void {
    this.action.set(null);
    this.error.set(
      httpError.error?.detail ??
        httpError.error?.title ??
        'The import operation failed. Check that the API is running.',
    );
  }
}
