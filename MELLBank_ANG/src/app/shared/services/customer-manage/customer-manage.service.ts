import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { API_BASE_URL } from '../../constants';
import { RegisterCustomerVM } from '../../models/admin/admin-vm';
import { CurrentAccountVM, CustomerVM, CustomerDetailsVM,
} from '../../models/customers/customer-vm';
import { ResponseMessageVM } from '../../models/auth/auth-vm';
import { AuthService } from '../auth/auth.service';
import { SavingsAccountVM } from '../../models/accounts/acc-management-vm';
import { AccountDetailVM, AccountVM } from '../../models/transaction/transaction-vm';

@Injectable({
  providedIn: 'root',
})
export class CustomerManageService {
  private baseUrl = API_BASE_URL + '/api/customers';

  constructor(private http: HttpClient, private auth: AuthService) {}

  get userHomePageUrl() {
    return this.auth.userHomePageUrl;
  }

  getCustomers(): Observable<CustomerVM[]> {
    const userToken = this.auth.userToken;
    const getCustomerUrl = this.baseUrl;
    const requestHeader = this.requestHeader;

    if (this.auth.isLoggedIn && userToken && this.auth.isSuperUser) {
      return this.http.get<CustomerVM[]>(getCustomerUrl, {
        headers: requestHeader,
      });
    } else {
      return of([this.emptyCustomer]);
    }
  }

  getCustomerById(id: number): Observable<CustomerDetailsVM> {
    const userToken = this.auth.userToken;
    const getCustomerUrl = this.baseUrl + `/${id}`;
    const requestHeader = this.requestHeader;

    if (this.auth.isLoggedIn && userToken && this.auth.isSuperUser) {
      return this.http.get<CustomerDetailsVM>(getCustomerUrl, {
        headers: requestHeader,
      });
    } else {
      return of(this.emptyCustomerDetails);
    }
  }

  registerCustomer(newCustomer: RegisterCustomerVM): Observable<CustomerVM> {
    const userToken = this.auth.userToken;
    const createCustomerUrl = this.baseUrl + '/register';
    const requestHeader = this.requestHeader;

    if (this.auth.isLoggedIn && userToken && this.auth.isSuperUser) {
      return this.http.post<CustomerVM>(createCustomerUrl, newCustomer, {
        headers: requestHeader,
      });
    } else {
      return of(this.emptyCustomer);
    }
  }

  updateCustomer(customer: CustomerVM): Observable<CustomerVM> {
    const userToken = this.auth.userToken;
    const updateCustomerUrl = this.baseUrl + `/${customer.customerId}`;

    if (this.auth.isLoggedIn && userToken && this.auth.isSuperUser) {
      return this.http.put<CustomerVM>(updateCustomerUrl, customer, {
        headers: this.requestHeader,
      });
    } else {
      return of(this.emptyCustomer);
    }
  }

  deleteCustomer(id: number): Observable<ResponseMessageVM> {
    const userToken = this.auth.userToken;
    const deleteCustomerUrl = this.baseUrl + `/${id}`;
    const requestHeader = this.requestHeader;

    if (this.auth.isLoggedIn && userToken && this.auth.isSuperUser) {
      return this.http.delete<ResponseMessageVM>(deleteCustomerUrl, {
        headers: requestHeader,
      });
    } else {
      return of(this.emptyResponseMessage);
    }
  }

  getAllMyAccounts(): Observable<AccountVM[]> {
    const userToken = this.auth.userToken;
    const getMyAccountsUrl = this.baseUrl + '/MyAccounts';
    const requestHeader = this.requestHeader;

    if (this.auth.isLoggedIn && userToken && this.auth.isCustomer) {
      return this.http.get<AccountVM[]>(getMyAccountsUrl, {
        headers: requestHeader,
      });
    } else {
      return of([this.emptyMyAccounts]);
    }
  }

  getMyAccountById(guid: string): Observable<AccountDetailVM> {
    const userToken = this.auth.userToken;
    const getMyAccountsUrl = this.baseUrl + `/MyAccount/${guid}`;
    const requestHeader = this.requestHeader;

    if (this.auth.isLoggedIn && userToken && this.auth.isCustomer) {
      return this.http.get<AccountDetailVM>(getMyAccountsUrl, {
        headers: requestHeader,
      });
    } else {
      return of(this.emptyAccountDetail);
    }
  }

  private get requestHeader() {
    const userToken = this.auth.userToken;
    return new HttpHeaders().set('Authorization', 'bearer ' + userToken);
  }

  emptyCustomer: RegisterCustomerVM = {
    customerId: 0,
    firstName: '',
    lastName: '',
    username: '',
    email: '',
    phoneNumber: '',
    streetAddress: '',
    city: '',
    province: '',
    postalCode: '',
    createSavingsAccount: false,
    createCurrentAccount: false,
  };

  emptySavings: SavingsAccountVM = {
    accountNumber: '',
    creationDate: new Date(),
    balance: 0,
    branchCode: '255774',
    closeDate: null,
    interestRateId: 1,
    customerId: 0,
  };

  emptyCurrent: CurrentAccountVM = {
    currentId: '',
    accountNumber: '',
    creationDate: '',
    balance: 0,
    branchCode: '',
    overdraftAmount: 0,
    overdraftRate: 0,
    closeDate: '',
    customerId: 0,
  };

  emptyMyAccounts: AccountVM = {
    accountId: '',
    accountNumber: '',
    creationDate: new Date(),
    balance: 0,
    branchCode: '',
    closeDate: null,
    customerId: 0,
    accountType: '',
  };

  emptyAccountDetail: AccountDetailVM = {
    accountId: '',
    accountNumber: '',
    creationDate: new Date(),
    balance: 0,
    branchCode: '',
    accountType: '',
    closeDate: null,
    totalNumberOfTransactions: 0,
    overdraftAmount: 0,
    overdraftRate: 0,
    interestRateId: 0,
  };

  emptyCustomerDetails: CustomerDetailsVM = {
    customerId: 0,
    firstName: '',
    lastName: '',
    username: '',
    email: '',
    phoneNumber: '',
    streetAddress: '',
    city: '',
    province: '',
    postalCode: '',
    currentAccounts: [this.emptyCurrent],
    savingsAccounts: [this.emptySavings],
  };

  emptyResponseMessage: ResponseMessageVM = {
    message: 'Unexpected Error, Please try again later',
  };
}
