import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BankStaffHomeComponent } from './components/bank-staff-home/bank-staff-home.component';
import { ManageAccountsComponent } from './components/manage-accounts/manage-accounts.component';
import { SavingsAccountComponent } from './components/savings-account/savings-account.component';
import { BankStaffLayoutComponent } from './components/bank-staff-layout/bank-staff-layout.component';
import { ProfileComponent } from './components/profile/profile.component';
import { TransactionsComponent } from './components/transactions/transactions.component';
import { RegisterCustomerComponent } from './components/register-customer/register-customer.component';
import { CurrentAccountComponent } from './components/current-account/current-account.component';

const routes: Routes = [
  {
    path: '',
    component: BankStaffLayoutComponent,
    children: [
      { path: '', component: BankStaffHomeComponent },
      { path: 'home', component: BankStaffHomeComponent },
      { path: 'account-management', component: ManageAccountsComponent },
      { path: 'savings-account/:customerId', component: SavingsAccountComponent},
      { path: 'profile', component: ProfileComponent },
      { path: 'transactions', component: TransactionsComponent },
      {path: 'register-customer', component:RegisterCustomerComponent},
      { path: 'savings-account/:customerId', component: SavingsAccountComponent },
      { path: 'current-account/:customerId', component: CurrentAccountComponent }
    ]
  },
  // { path: '', component: BankStaffHomeComponent },

  { path: 'savings-account', component: SavingsAccountComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BankStaffRoutingModule { }
