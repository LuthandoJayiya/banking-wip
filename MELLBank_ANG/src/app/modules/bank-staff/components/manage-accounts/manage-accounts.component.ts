import { Component, OnInit } from '@angular/core';
import { AccountManagementService } from '../../../../shared/services/accounts/account-management.service';
import { Router } from '@angular/router';
import { CustomerVM } from 'src/app/shared/models/customers/customer-vm';
import { AuthService } from 'src/app/shared/services/auth/auth.service';

@Component({
  selector: 'app-manage-accounts',
  templateUrl: './manage-accounts.component.html',
  styleUrls: ['./manage-accounts.component.css'],
})
export class ManageAccountsComponent implements OnInit {
  customers: CustomerVM[] = [];
  customer: CustomerVM = {
    customerId: 0,
    firstName: '',
    lastName: '',
    username: '',
    email: '',
    phoneNumber: '',
    streetAddress: '',
    city: '',
    province: '',
    postalCode: '',
  };

  constructor(
    private accService: AccountManagementService, private router: Router, private auth:AuthService) {}

  ngOnInit(): void {
    this.viewCustomers();
  }

  logout(){
    this.auth.logout();
  }
  viewCustomers(): void {
    this.accService
      .getCustomers()
      .subscribe((customers: CustomerVM[]) => (this.customers = customers));
    console.log(this.customers);
  }

  viewSavings(customerId: number): void {
    console.log('View Savings for customer:', customerId);
    this.router.navigate(['/bank-staff/savings-account', customerId]);
  }

  viewCurrent(customerId: number): void {
    console.log('View Current Account for customer:', customerId);
    this.router.navigate(['/bank-staff/current-account', customerId]);
  }
}
