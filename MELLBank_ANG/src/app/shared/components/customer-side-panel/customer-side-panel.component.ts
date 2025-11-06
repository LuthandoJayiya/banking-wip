import { Component } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-customer-side-panel',
  templateUrl: './customer-side-panel.component.html',
  styleUrls: ['./customer-side-panel.component.css'],
})
export class CustomerSidePanelComponent {
  constructor(private auth: AuthService) {}

  logout() {
    this.auth.logout();
  }
  get fullName() {
    return this.auth.userFullName;
  }

  get userHome() {
    return this.auth.userHomePageUrl;
  }

  comingSoon() {
    alert('Feature no our next sprint');
  }
}
