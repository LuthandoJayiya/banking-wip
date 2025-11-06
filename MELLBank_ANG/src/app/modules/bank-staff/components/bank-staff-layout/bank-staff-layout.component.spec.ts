import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BankStaffLayoutComponent } from './bank-staff-layout.component';

describe('BankStaffLayoutComponent', () => {
  let component: BankStaffLayoutComponent;
  let fixture: ComponentFixture<BankStaffLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BankStaffLayoutComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BankStaffLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
