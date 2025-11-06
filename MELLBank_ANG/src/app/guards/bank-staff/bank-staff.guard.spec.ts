import { TestBed } from '@angular/core/testing';

import { BankStaffGuard } from './bank-staff.guard';

describe('BankStaffGuard', () => {
  let guard: BankStaffGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(BankStaffGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
