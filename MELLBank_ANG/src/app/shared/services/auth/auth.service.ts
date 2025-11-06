import { Injectable } from '@angular/core';
import { API_BASE_URL, LOGIN_ENDPOINT } from '../../constants';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { CurrentUserVM, LoginVM } from '../../models/auth/auth-vm';
import { catchError, map, Observable, of } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _baseUrl: string = API_BASE_URL;
  errorMessage: string = '';

  constructor(private http: HttpClient, private router: Router) {}

  get userHomePageUrl() {
    return localStorage.getItem('UserHomePageUrl') ?? '/';
  }

  get isLoggedIn(): boolean {
    const user = localStorage.getItem('user');
    const userToken = this.userToken;
    const userName = this.userName;
    if (user && userToken && userName) {
      return true;
    } else {
      return false;
    }
  }

  get isAdmin(): boolean {
    const userRole = localStorage.getItem('UserRole');
    return userRole === 'Administrator' && this.isLoggedIn;
  }
  get isBankStaff(): boolean {
    const userRole = localStorage.getItem('UserRole');
    return userRole === 'BankStaff' && this.isLoggedIn;
  }

  get isCustomer(): boolean {
    const userRole = localStorage.getItem('UserRole');
    return userRole === 'Customer' && this.isLoggedIn;
  }

  get isSuperUser() {
    return this.isAdmin || this.isBankStaff;
  }

  login(userName: string, password: string): Observable<any> {
    const loginUrl = LOGIN_ENDPOINT;
    const loginVM: LoginVM = {
      userName: userName,
      password: password,
    };

    let currentLoggedInUser: CurrentUserVM;
    let userHomePageUrl = '';
    localStorage.clear();

    return this.http.post(loginUrl, loginVM).pipe(
      map(async (response: any) => {
        console.log(response);
        if (response && response.token) {
          let result = JSON.stringify(response);
          localStorage.setItem('user', JSON.stringify(result));

          currentLoggedInUser = JSON.parse(result || '{}');
          const [firstRole] = currentLoggedInUser.roles;

          if (firstRole == 'Administrator') {
            userHomePageUrl = '/admin';
          } else if (firstRole == 'BankStaff') {
            userHomePageUrl = '/bank-staff';
          } else {
            userHomePageUrl = '/customer';
          }

          localStorage.setItem('UserHomePageUrl', userHomePageUrl);
          localStorage.setItem('UserToken', currentLoggedInUser.token);
          localStorage.setItem(
            'UserTokenValidTo',
            currentLoggedInUser.expiration
          );
          localStorage.setItem('FirstName', currentLoggedInUser.firstName);
          localStorage.setItem('LastName', currentLoggedInUser.lastName);
          const fullName =
            currentLoggedInUser.firstName + ' ' + currentLoggedInUser.lastName;
          localStorage.setItem('FullName', fullName);
          localStorage.setItem('UserName', currentLoggedInUser.userName);
          localStorage.setItem('UserRole', firstRole);
        }
      }),
      catchError(this.handleError)
    );
  }


  get userName() {
    return localStorage.getItem('UserName');
  }

  get userFullName() {
    return localStorage.getItem('FullName');
  }

  get userToken() {
    return localStorage.getItem('UserToken');
  }

  private handleError(error: HttpErrorResponse) {
    console.log(error);
    let errorMessage = 'An unexpected error occured. Please try again later!';

    if (error.error && error.error.message) {
      errorMessage = error.error.message;
    }
    alert(errorMessage);
    this.errorMessage = errorMessage;
    return of(errorMessage);
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/']);
  }
}
