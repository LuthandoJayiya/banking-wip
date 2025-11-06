import { Component } from '@angular/core';
import { AuthService } from 'src/app/shared/services/auth/auth.service';

@Component({
  selector: 'app-customer-navbar',
  templateUrl: './customer-navbar.component.html',
  styleUrls: ['./customer-navbar.component.css']
})
export class CustomerNavbarComponent {

  constructor(private auth: AuthService) { }

  logout() {
    this.auth.logout();
  }


  comingSoon() {
    alert('Feature no our next sprint');
  }
}
