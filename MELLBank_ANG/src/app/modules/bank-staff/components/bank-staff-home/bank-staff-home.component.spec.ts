import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BankStaffHomeComponent } from './bank-staff-home.component';

describe('BankStaffHomeComponent', () => {
  let component: BankStaffHomeComponent;
  let fixture: ComponentFixture<BankStaffHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BankStaffHomeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BankStaffHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
