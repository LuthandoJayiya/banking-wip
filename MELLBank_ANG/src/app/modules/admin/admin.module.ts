import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminHomeComponent } from './components/admin-home/admin-home.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CustomerRegisterComponent } from './components/customer-register/customer-register.component';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { CustomerDetailComponent } from './components/customer-detail/customer-detail.component';

@NgModule({
  declarations: [
    AdminHomeComponent,
    CustomerRegisterComponent,
    CustomerListComponent,
    CustomerDetailComponent,
  ],
  imports: [CommonModule, AdminRoutingModule, RouterModule, FormsModule],
})
export class AdminModule {}
