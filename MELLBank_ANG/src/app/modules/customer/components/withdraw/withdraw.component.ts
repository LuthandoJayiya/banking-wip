import { Location } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {
  AccountDetailVM,
  TransactionVM
} from 'src/app/shared/models/transaction/transaction-vm';
import { TranserviceService } from 'src/app/shared/models/transervice/transervice.service';
import { AuthService } from 'src/app/shared/services/auth/auth.service';
import { CustomerManageService } from 'src/app/shared/services/customer-manage/customer-manage.service';

@Component({
  selector: 'app-withdraw',
  templateUrl: './withdraw.component.html',
  styleUrls: ['./withdraw.component.css'],
})
export class WithdrawComponent {
  account: AccountDetailVM = this.cmService.emptyAccountDetail;
  accountId: string | undefined;

  constructor(
    private cmService: CustomerManageService,
    private transervice: TranserviceService,
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private location: Location
  ) {}

  ngOnInit() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    this.accountId = String(id);

    if (this.accountId) {
      this.cmService.getMyAccountById(this.accountId).subscribe((data) => {
        console.log('Data returned: ' + data);
        this.account = data;
        this.withdrawalRequest.accountId = this.account.accountId;
        this.withdrawalRequest.accountType = this.account.accountType;
        console.log('The account Selected: ' + JSON.stringify(this.account));
      });
    }
  }

  withdrawalRequest: TransactionVM = {
    accountId: '',
    accountType: '',
    amount: 0,
    transactionId: 0
  };

  showAlert: boolean = false;
  response: any;

  submitWithdrawForm(form: NgForm) {
    this.transervice
      .createWithdraw(this.withdrawalRequest)
      .subscribe((response: any) => {
        console.log(JSON.stringify(response));
        this.showAlert = true;
        form.resetForm();
      });
  }

  get fullName() {
    return this.authService.userFullName;
  }

  goBack() {
    this.location.back();
  }
}
