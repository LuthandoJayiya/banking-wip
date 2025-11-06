import { TestBed } from '@angular/core/testing';

import { CurrentAccountManagementService } from './current-account-management.service';

describe('CurrentAccountManagementService', () => {
  let service: CurrentAccountManagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CurrentAccountManagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
