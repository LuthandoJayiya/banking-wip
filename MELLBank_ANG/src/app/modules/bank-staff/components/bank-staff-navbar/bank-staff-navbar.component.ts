import { Component } from '@angular/core';
import { AuthService } from 'src/app/shared/services/auth/auth.service';

@Component({
  selector: 'app-bank-staff-navbar',
  templateUrl: './bank-staff-navbar.component.html',
  styleUrls: ['./bank-staff-navbar.component.css'],
})
export class BankStaffNavbarComponent {
  isMenuClosed: boolean = false;

  constructor(private auth: AuthService) {}
  toggleMenu() {
    this.isMenuClosed = !this.isMenuClosed;
  }

  logout() {
    this.auth.logout();
  }

  get userHome() {
    return this.auth.userHomePageUrl;
  }
}
