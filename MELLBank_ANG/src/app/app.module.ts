import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { GuestModule } from './modules/guest/guest.module';
import { AdminModule } from './modules/admin/admin.module';
import { BankStaffModule } from './modules/bank-staff/bank-staff.module';
import { SharedModule } from './shared/shared.module';
import { CustomerModule } from './modules/customer/customer.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    GuestModule,
    AdminModule,
    CustomerModule,
    SharedModule,
    BankStaffModule,

  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
