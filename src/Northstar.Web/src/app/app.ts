import { CurrencyPipe, DatePipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ImportPanelComponent } from './imports/import-panel.component';
import { PayrollApiService } from './payroll-runs/payroll-api.service';
import { PayrollRun } from './payroll-runs/payroll-run.models';

@Component({
  selector: 'app-root',
  imports: [CurrencyPipe, DatePipe, ImportPanelComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {
  private readonly payrollApi = inject(PayrollApiService);

  protected readonly run = signal<PayrollRun | null>(null);
  protected readonly loading = signal(true);
  protected readonly action = signal<string | null>(null);
  protected readonly error = signal<string | null>(null);
  protected readonly blockerCount = computed(
    () => this.run()?.findings.filter((finding) => finding.severity === 'Blocker').length ?? 0
  );
  protected readonly warningCount = computed(
    () => this.run()?.findings.filter((finding) => finding.severity === 'Warning').length ?? 0
  );

  ngOnInit(): void {
    this.load();
  }

  protected calculate(): void {
    const run = this.run();
    if (!run) return;

    this.beginAction('Calculating payroll');
    this.payrollApi.calculate(run.id, crypto.randomUUID()).subscribe({
      next: (calculated) => this.completeAction(calculated),
      error: (error: HttpErrorResponse) => this.failAction(error)
    });
  }

  protected addMissingRate(): void {
    this.beginAction('Adding carpenter rate');
    this.payrollApi.addMissingCarpenterRate().subscribe({
      next: () => {
        this.action.set(null);
        this.calculate();
      },
      error: (error: HttpErrorResponse) => this.failAction(error)
    });
  }

  protected approve(): void {
    const run = this.run();
    if (!run) return;

    this.beginAction('Approving payroll');
    this.payrollApi.approve(run).subscribe({
      next: (approved) => this.completeAction(approved),
      error: (error: HttpErrorResponse) => this.failAction(error)
    });
  }

  private load(): void {
    this.loading.set(true);
    this.error.set(null);
    this.payrollApi.listRuns().subscribe({
      next: (runs) => {
        this.run.set(runs[0] ?? null);
        this.loading.set(false);
      },
      error: (error: HttpErrorResponse) => {
        this.loading.set(false);
        this.failAction(error);
      }
    });
  }

  private beginAction(label: string): void {
    this.action.set(label);
    this.error.set(null);
  }

  private completeAction(run: PayrollRun): void {
    this.run.set(run);
    this.action.set(null);
  }

  private failAction(error: HttpErrorResponse): void {
    this.action.set(null);
    this.error.set(
      error.error?.detail ?? error.error?.title ?? 'The operation failed. Check that the API is running.'
    );
  }
}
