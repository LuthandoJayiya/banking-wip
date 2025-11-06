import { Component } from '@angular/core';
import { AuthService } from 'src/app/shared/services/auth/auth.service';

@Component({
  selector: 'app-bank-staff-home',
  templateUrl: './bank-staff-home.component.html',
  styleUrls: ['./bank-staff-home.component.css'],
})
export class BankStaffHomeComponent {
  constructor(private auth: AuthService) {}
  comingSoon() {
    alert('Feature no our next sprint');
  }

  get UserHomeUrl() {
    return this.auth.userHomePageUrl;
  }
}
