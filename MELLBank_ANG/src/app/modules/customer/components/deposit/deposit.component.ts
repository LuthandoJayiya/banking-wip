import { Location } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {
  AccountDetailVM,
  DepositRequest,
  DepositResponse,
} from 'src/app/shared/models/transaction/transaction-vm';
import { TranserviceService } from 'src/app/shared/models/transervice/transervice.service';
import { AuthService } from 'src/app/shared/services/auth/auth.service';
import { CustomerManageService } from 'src/app/shared/services/customer-manage/customer-manage.service';

@Component({
  selector: 'app-deposit',
  templateUrl: './deposit.component.html',
  styleUrls: ['./deposit.component.css'],
})
export class DepositComponent {
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
        this.depositRequest.accountId = this.account.accountId;
        this.depositRequest.accountType = this.account.accountType;
        console.log('The account Selected: ' + JSON.stringify(this.account));
      });
    }
  }

  depositRequest: DepositRequest = {
    accountId: '',
    accountType: '',
    amount: 0,
  };

  showAlert: boolean = false;
  response: DepositResponse | null = null;

  submitDepositForm(form: NgForm) {
    this.transervice
      .createDeposit(this.depositRequest)
      .subscribe((response: any) => {
        console.log(response);
        console.log(response.message);
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
