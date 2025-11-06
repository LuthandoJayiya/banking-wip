import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BankStaffRoutingModule } from './bank-staff-routing.module';
import { BankStaffHomeComponent } from './components/bank-staff-home/bank-staff-home.component';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SavingsAccountComponent } from './components/savings-account/savings-account.component';
import { ManageAccountsComponent } from './components/manage-accounts/manage-accounts.component';
import { BankStaffLayoutComponent } from './components/bank-staff-layout/bank-staff-layout.component';
import { BankStaffNavbarComponent } from './components/bank-staff-navbar/bank-staff-navbar.component';
import { ProfileComponent } from './components/profile/profile.component';
import { TransactionsComponent } from './components/transactions/transactions.component';
import { RegisterCustomerComponent } from './components/register-customer/register-customer.component';
import { CurrentAccountComponent } from './components/current-account/current-account.component';


@NgModule({
  declarations: [BankStaffHomeComponent, SavingsAccountComponent, ManageAccountsComponent, BankStaffLayoutComponent, BankStaffNavbarComponent, ProfileComponent, TransactionsComponent, RegisterCustomerComponent, CurrentAccountComponent],
  imports: [CommonModule, BankStaffRoutingModule, RouterModule, FormsModule],
})
export class BankStaffModule {}
