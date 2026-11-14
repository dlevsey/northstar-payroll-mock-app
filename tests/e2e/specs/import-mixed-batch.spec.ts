import { expect, test } from '@playwright/test';

test('imports a mixed provider batch and exposes row-level diagnostics', async ({ page }) => {
  await page.goto('/');

  await expect(page.getByTestId('import-panel')).toBeVisible();
  await page.getByTestId('import-mixed').click();

  await expect(page.getByTestId('import-status')).toHaveText('CompletedWithErrors');
  await expect(page.getByTestId('import-total-count')).toHaveText('4');
  await expect(page.getByTestId('import-error-count')).toHaveText('2');
  await expect(page.getByTestId('import-duplicate-count')).toHaveText('1');
  await expect(page.getByTestId('import-row-errors')).toContainText('EMPLOYEE_NOT_FOUND');
  await expect(page.getByTestId('import-row-errors')).toContainText('INVALID_HOURS');
  await expect(page.getByTestId('import-row-errors')).toContainText('DUPLICATE_TIME_ENTRY');
  await expect(page.getByTestId('import-error-row')).toHaveCount(3);
});
