import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'

import { CustomerRoutingModule } from './customer-routing.module'
import { CustomerHomeComponent } from './components/customer-home/customer-home.component'
import { FormsModule } from '@angular/forms'
import { RouterModule } from '@angular/router'
import { DepositComponent } from './components/deposit/deposit.component';
import { WithdrawComponent } from './components/withdraw/withdraw.component';
import { CustomerLayoutComponent } from './components/customer-layout/customer-layout.component';
import { CustomerNavbarComponent } from './components/customer-navbar/customer-navbar.component';
import { CustomerTransactionsComponent } from './components/customer-transactions/customer-transactions.component';
import { ViewtransactionsComponent } from 'src/app/modulescustomercomponents/viewtransactions/viewtransactions.component'

@NgModule({
  declarations: [CustomerHomeComponent, DepositComponent, WithdrawComponent, ViewtransactionsComponent, CustomerLayoutComponent, CustomerNavbarComponent, CustomerTransactionsComponent],
  imports: [CommonModule, CustomerRoutingModule, RouterModule, FormsModule]
})
export class CustomerModule {}
