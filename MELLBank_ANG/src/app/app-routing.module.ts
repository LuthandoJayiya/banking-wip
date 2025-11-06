import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminGuard } from './guards/admin/admin.guard';
import { CustomerGuard } from './guards/customer/customer.guard';
import { BankStaffGuard } from './guards/bank-staff/bank-staff.guard';
import { PageNotFoundComponent } from './shared/components/page-not-found/page-not-found.component';
import { AdminLayoutComponent } from './shared/layouts/admin-layout/admin-layout.component';
import { CustomerLayoutComponent } from './shared/layouts/customer-layout/customer-layout.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [CustomerGuard],
    loadChildren: () =>
      import('./modules/guest/guest.module').then((m) => m.GuestModule),
  },
  {
    path: 'customer',
    canActivate: [CustomerGuard],
    // component: CustomerLayoutComponent,
    loadChildren: () =>
      import('./modules/customer/customer.module').then(
        (m) => m.CustomerModule
      ),
  },
  {
    path: 'bank-staff',
    canActivate: [BankStaffGuard],
    loadChildren: () =>
      import('./modules/bank-staff/bank-staff.module').then(
        (m) => m.BankStaffModule
      ),
  },
  {
    path: 'admin',
    canActivate: [AdminGuard],
    component: AdminLayoutComponent,
    loadChildren: () =>
      import('./modules/admin/admin.module').then((m) => m.AdminModule),
  },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
