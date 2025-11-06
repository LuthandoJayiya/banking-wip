import { Injectable } from '@angular/core';
import { API_BASE_URL } from '../../constants';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { SavingsAccountVM } from '../../models/accounts/acc-management-vm';
import { Observable, of } from 'rxjs';
import { CustomerVM } from '../../models/customers/customer-vm';

@Injectable({
  providedIn: 'root',
})
export class AccountManagementService {
  private _baseUrl: string = API_BASE_URL;

  constructor(private http: HttpClient) {}

  getSavingsAccounts(customerId: number): Observable<SavingsAccountVM[]> {
    let UserToken: string = this.getUserToken();
    let reqHeader = new HttpHeaders().set(
      'Authorization',
      'bearer ' + UserToken
    );
    return this.http.get<SavingsAccountVM[]>(
      `${this._baseUrl}/api/SavingsAccount/customer/${customerId}`,
      { headers: reqHeader }
    );
  }

  addSavingsAccount(
    newAccount: SavingsAccountVM
  ): Observable<SavingsAccountVM> {
    let UserToken: string = this.getUserToken();
    console.log(this.isLoggedIn());
    console.log(UserToken);
    if (this.isLoggedIn() && UserToken) {
      let reqHeader = new HttpHeaders().set(
        'Authorization',
        'bearer ' + UserToken
      );
      return this.http.post<SavingsAccountVM>(
        this._baseUrl + '/api/SavingsAccount',
        newAccount,
        {
          headers: reqHeader,
        }
      );
    } else {
      console.log('User Not authenticated');
      return of();
    }
  }

  deleteSavingsAccount(
    accountNumber: string
  ): Observable<{ savingsId: string; closeDate: Date }> {
    let UserToken: string = this.getUserToken();
    if (this.isLoggedIn() && UserToken) {
      let reqHeader = new HttpHeaders().set(
        'Authorization',
        'bearer ' + UserToken
      );
      return this.http.put<{ savingsId: string; closeDate: Date }>(
        `${this._baseUrl}/api/SavingsAccount/CloseAccount/${accountNumber}`,
        {},
        { headers: reqHeader }
      );
    } else {
      console.log('User Not authenticated');
      return of({ savingsId: '', closeDate: new Date() });
    }
  }

  getCustomers(): Observable<CustomerVM[]> {
    let UserToken: string = this.getUserToken();
    console.log(this.isLoggedIn());
    console.log(UserToken);
    if (this.isLoggedIn() && UserToken) {
      let reqHeader = new HttpHeaders().set(
        'Authorization',
        'bearer ' + UserToken
      );
      return this.http.get<CustomerVM[]>(this._baseUrl + '/api/Customers', {
        headers: reqHeader,
      });
    } else {
      console.log('User Not authenticated');
      return of();
    }
  }

  getUserToken() {
    let currUserToken: string = localStorage.getItem('UserToken') || 'N/A';
    console.log('getUserToken: currUserToken' + currUserToken);

    if (this.isLoggedIn() && currUserToken && currUserToken !== 'N/A') {
      return currUserToken;
    } else {
      return '';
    }
  }

  isLoggedIn() {
    let loggedInUser = localStorage.getItem('user');
    let currUser: string = localStorage.getItem('UserName') || '';
    console.log('isLoggedIn - loggedInUser: ' + loggedInUser);
    console.log('isLoggedIn - currUser: ' + currUser);

    if (loggedInUser && currUser) {
      return true;
    } else {
      return false;
    }
  }
}
