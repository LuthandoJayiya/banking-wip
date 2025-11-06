import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { GuestHeaderComponent } from './components/guest-header/guest-header.component';
import { GuestFooterComponent } from './components/guest-footer/guest-footer.component';
import { RouterModule } from '@angular/router';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AdminSidePanelComponent } from './components/admin-side-panel/admin-side-panel.component';
import { CustomerLayoutComponent } from './layouts/customer-layout/customer-layout.component';
import { CustomerSidePanelComponent } from './components/customer-side-panel/customer-side-panel.component';

@NgModule({
  declarations: [
    PageNotFoundComponent,
    GuestHeaderComponent,
    GuestFooterComponent,
    AdminLayoutComponent,
    AdminSidePanelComponent,
    CustomerLayoutComponent,
    CustomerSidePanelComponent,
  ],
  imports: [CommonModule, RouterModule],
  exports: [GuestHeaderComponent, GuestFooterComponent],
})
export class SharedModule {}
