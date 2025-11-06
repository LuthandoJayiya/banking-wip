import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { TIMEOUT } from 'src/app/shared/constants';
import { LoginVM } from 'src/app/shared/models/auth/auth-vm';
import { AuthService } from 'src/app/shared/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  userLogin: LoginVM = {
    userName: '',
    password: '',
  };
  get userName() {
    return localStorage.getItem('newUserName');
  }
  constructor(private auth: AuthService, private router: Router) {}

  ngOnInit() {
    if (this.userName) {
      this.userLogin.userName = this.userName;
    }
  }

  onSubmit(form: NgForm) {
    if (form.invalid) {
      return;
    }

    this.auth
      .login(this.userLogin.userName, this.userLogin.password)
      .subscribe((data) => {
        setTimeout(() => {
          const userHomePageUrl = this.auth.userHomePageUrl;
          if (userHomePageUrl != '/') {
            this.router.navigate([userHomePageUrl]);
          }
        }, TIMEOUT);
      });
  }
}
