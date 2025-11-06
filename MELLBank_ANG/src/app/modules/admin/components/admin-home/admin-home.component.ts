import { Component } from '@angular/core';
import { AuthService } from '../../../../shared/services/auth/auth.service';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css'],
})
export class AdminHomeComponent {
  constructor(private auth: AuthService) {}

  logout() {
    console.log("CLICKED CLICKED");
    this.auth.logout();
  }

  get userHome() {
    return this.auth.userHomePageUrl;
  }
}
