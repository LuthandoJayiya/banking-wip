import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerHomeComponent } from './components/customer-home/customer-home.component';
import { DepositComponent } from './components/deposit/deposit.component';
import { WithdrawComponent } from './components/withdraw/withdraw.component';
import { CustomerLayoutComponent } from './components/customer-layout/customer-layout.component';
import { CustomerTransactionsComponent } from './components/customer-transactions/customer-transactions.component';
import { ViewtransactionsComponent } from './components/viewtransactions/viewtransactions.component';

const routes: Routes = [
  {
    path: '',
    component: CustomerLayoutComponent,
    children: [
      { path: '', component: CustomerHomeComponent },
      { path: 'home', component: CustomerHomeComponent },
      {
        path: 'customer-transactions',
        component: CustomerTransactionsComponent,
        children: [
          // { path: 'deposit', component: DepositComponent },
          // { path: 'withdraw', component: WithdrawComponent },
        ],
      },

      { path: 'viewtransactions', component: ViewtransactionsComponent },
      { path: 'deposit/:id', component: DepositComponent },
      { path: 'withdraw/:id', component: WithdrawComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CustomerRoutingModule {}
