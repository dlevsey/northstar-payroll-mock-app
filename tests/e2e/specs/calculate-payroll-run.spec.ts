import { expect, test } from '@playwright/test';

test('calculate, resolve a missing rate, and approve the seeded run', async ({ page }) => {
  await page.goto('/');

  await expect(page.getByRole('heading', { name: 'Review compliance before approval.' })).toBeVisible();
  await expect(page.getByTestId('run-status')).toHaveText('Draft');

  await page.getByTestId('calculate-run').click();
  await expect(page.getByTestId('run-status')).toHaveText('NeedsReview');
  await expect(page.getByText('RATE NOT FOUND')).toBeVisible();
  await expect(page.getByText('FRINGE SHORTFALL')).toBeVisible();
  await expect(page.getByText('Rate missing')).toBeVisible();

  await page.getByTestId('add-missing-rate').click();
  await expect(page.getByTestId('run-status')).toHaveText('ReadyForApproval');
  await expect(page.getByText('RATE NOT FOUND')).toHaveCount(0);

  await page.getByTestId('approve-run').click();
  await expect(page.getByTestId('run-status')).toHaveText('Approved');
});
