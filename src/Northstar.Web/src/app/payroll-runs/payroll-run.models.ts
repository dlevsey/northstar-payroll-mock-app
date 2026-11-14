export interface PayrollLine {
  id: string;
  employeeId: string;
  employeeName: string;
  projectName: string;
  classificationName: string;
  workDate: string;
  regularHours: number;
  overtimeHours: number;
  resolvedWageRateId: string | null;
  requiredBaseWages: number | null;
  requiredFringe: number | null;
  actualBaseWages: number;
  actualFringe: number;
  variance: number | null;
}

export interface ComplianceFinding {
  id: string;
  severity: 'Blocker' | 'Warning' | 'Information';
  code: string;
  message: string;
  timeEntryId: string | null;
}

export interface PayrollRun {
  id: string;
  organizationId: string;
  periodStart: string;
  periodEnd: string;
  status: string;
  calculationVersion: number;
  version: number;
  approvedAt: string | null;
  lines: PayrollLine[];
  findings: ComplianceFinding[];
}
