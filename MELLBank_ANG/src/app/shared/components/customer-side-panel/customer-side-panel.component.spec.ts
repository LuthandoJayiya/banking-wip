import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerSidePanelComponent } from './customer-side-panel.component';

describe('CustomerSidePanelComponent', () => {
  let component: CustomerSidePanelComponent;
  let fixture: ComponentFixture<CustomerSidePanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerSidePanelComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomerSidePanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
