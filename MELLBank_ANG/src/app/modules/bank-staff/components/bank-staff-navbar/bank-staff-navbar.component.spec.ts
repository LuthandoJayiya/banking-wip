import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BankStaffNavbarComponent } from './bank-staff-navbar.component';

describe('BankStaffNavbarComponent', () => {
  let component: BankStaffNavbarComponent;
  let fixture: ComponentFixture<BankStaffNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BankStaffNavbarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BankStaffNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
