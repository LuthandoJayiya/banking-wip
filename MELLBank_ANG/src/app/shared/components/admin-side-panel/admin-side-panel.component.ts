import { Component } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-admin-side-panel',
  templateUrl: './admin-side-panel.component.html',
  styleUrls: ['./admin-side-panel.component.css'],
})
export class AdminSidePanelComponent {
  constructor(private auth: AuthService) {}

  logout() {
    this.auth.logout();
  }

  get userHome() {
    return this.auth.userHomePageUrl;
  }
}
