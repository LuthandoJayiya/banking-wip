import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TransactionVM, TransactionDetailVM, DepositRequest, DepositResponse, TransDetailsVM } from '../transaction/transaction-vm'
import * as constants from 'src/app/shared/constants';
import { AuthService } from '../../services/auth/auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TranserviceService {

  private _baseurl = constants.API_BASE_URL;

  localSetting = "en-GB";


  public currUser: string = localStorage.getItem('userName') || '';
  public currUserFullName: string = localStorage.getItem('fullName') || '';


  sampleCreateWithdraw: TransactionVM = {
    transactionId: 0,
    accountId: '',
    //customerId: 0,
    accountType: '',
    amount: 0,
    
}

sampleMyTransaction: TransDetailsVM = {
  transDetailId: 0,
  transactionDate: new Date(), 
  amount: 0,
  transactionType: '',
  description: '',
  transactionId: 0,
};




  constructor(private http: HttpClient, private authService: AuthService) { }


  createDeposit(depositData: DepositRequest) {
   console.log(depositData.accountId);
   console.log(depositData.accountType);
   console.log(depositData.amount);
  //  depositData.customerId = 1;
  //  depositData.accountId = "c92d94c3-048f-4e7e-8adc-e391f9bc5f13";
  //  depositData.accountType = "Savings";
  //  depositData.amount = 300;
   
   
    return this.http.post(`${this._baseurl}/api/Transactions/Deposit`, depositData, {headers: this.requestHeader});
  }

  getTransactions(): Observable<TransDetailsVM[]> {
    return this.http.get<TransDetailsVM[]>(this._baseurl + '/api/Transactions/MyTransactionDetails?transactionId=1', {headers: this.requestHeader});
  }

  createWithdraw(withdrawData:TransactionVM): Observable<TransactionVM> {
    console.log(withdrawData.accountId);
    console.log(withdrawData.amount)
    //console.log(withdrawData.transactionId)
    const requestHeader =  this.requestHeader;
    const userToken = this.authService.userToken;
    console.log(userToken);
    console.log(requestHeader);
    return this.http.post<TransactionVM>(this._baseurl + '/api/Transactions/Withdraw', withdrawData, {headers: this.requestHeader});
  }

  

  private get requestHeader() {
    const userToken = this.authService.userToken;
    return new HttpHeaders().set('Authorization', 'bearer ' + userToken);
  }
}
