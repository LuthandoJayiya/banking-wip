import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import * as constants from 'src/app/shared/constants';
import { Observable, catchError, of } from 'rxjs';
import { CurrentAccount } from '../../models/accounts/acc-management-vm';

@Injectable({
  providedIn: 'root'
})
export class CurrentAccountManagementService {
  private apiUrl = constants.API_BASE_URL;

  public currUser: string = localStorage.getItem('userName') || '';
  public currUserFullName: string = localStorage.getItem('fullName') || '';

  constructor(private http: HttpClient, private authenticationService: AuthService) { }

  getAllCurrentAccounts(): Observable<CurrentAccount[]>{
    return this.http.get<CurrentAccount[]>(this.apiUrl + '/api/CurrentAccount');
  }

  getAccountById(accID:string): Observable<CurrentAccount>{
    let userToken: any = this.authenticationService.userToken;
    console.log("getAccountById: ", userToken);

    if (this.authenticationService.isLoggedIn && userToken) {
      let reqHeader = new HttpHeaders().set("Authorization", "bearer " + userToken);
      console.log("getAccountById=TRUE", userToken);
      return this.http.get<CurrentAccount>(this.apiUrl + '/api/CurrentAccount/' + accID, { headers: reqHeader }); 
    } else {
      return of()
    }
  }

  getAllCurrentAccountsForCustomer(id:number): Observable<CurrentAccount[]>{
    let userToken: any = this.authenticationService.userToken;
    console.log("getAllCurrentAccountsForCustomer(): ", userToken);

    if (this.authenticationService.isLoggedIn && userToken) {
      let reqHeader = new HttpHeaders().set("Authorization", "bearer " + userToken);
      console.log("getAllCurrentAccountsForCustomer()=TRUE", userToken);
      return this.http.get<CurrentAccount[]>(this.apiUrl + '/api/CurrentAccount/AllAccountsForCustomer/' + id, { headers: reqHeader }); 
    } else {
      return of([])
    }
  }

  getCurrentAccountWithCustomerDetails(accountID: string){}

  openNewAccount(custId: number): Observable<CurrentAccount> {
    let userToken: any = this.authenticationService.userToken;
    console.log("openNewAccount(): ", userToken)

    if (this.authenticationService.isLoggedIn && userToken) {
      console.log("openNewAccount(): ", this.authenticationService.isLoggedIn);
      let reqHeader = new HttpHeaders().set("Authorization", "Bearer " + userToken);
      const newAccount: CurrentAccount = {
        CurrentId:'3fa85f64-5717-4562-b3fc-2c963f66afa6',//place holder it will be regenerated automatically
        AccountNumber:'',
        CreationDate:new Date(),
        Balance: 0,
        BranchCode:"255774",
        OverdraftAmount :0,
        OverdraftRate:14.5,
        CloseDate: null,
        CustomerId:custId
      };
      console.log("openNewAccount(): ", newAccount);
      return this.http.post<CurrentAccount>(`${this.apiUrl}/api/CurrentAccount?custId=${custId}`, newAccount, { headers: reqHeader });
    } else {
      return of();
    }
  }

  updateAccount(accID: string){}

  closeAccount(accID: string):Observable<CurrentAccount>{
    let userToken: any = this.authenticationService.userToken;
    console.log("closeAccount(): ", userToken);

    if (this.authenticationService.isLoggedIn && userToken) {
      let reqHeader = new HttpHeaders().set("Authorization", "bearer " + userToken);
      console.log("getAllCurrentAccountsForCustomer()=TRUE", userToken);
      return this.http.put<CurrentAccount>(this.apiUrl + '/api/CurrentAccount/CloseAccount/' + accID, { headers: reqHeader }); 
    } else {
      return of()
    }
  }

  
}
