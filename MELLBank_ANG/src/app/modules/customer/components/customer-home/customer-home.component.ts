import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountVM } from 'src/app/shared/models/transaction/transaction-vm';
import { AuthService } from 'src/app/shared/services/auth/auth.service';
import { CurrentAccountManagementService } from 'src/app/shared/services/current-account-management/current-account-management.service';
import { CustomerManageService } from 'src/app/shared/services/customer-manage/customer-manage.service';

@Component({
  selector: 'app-customer-home',
  templateUrl: './customer-home.component.html',
  styleUrls: ['./customer-home.component.css'],
})
export class CustomerHomeComponent implements OnInit {
  accounts: AccountVM[] = [];

  constructor(
    private auth: AuthService,
    private cmService: CustomerManageService
  ) {}

  ngOnInit(): void {
    this.cmService.getAllMyAccounts().subscribe(
      (data) => {
        this.accounts = data;
      },
      (error) => {
        console.error(
          'Unable to fetch all accounts. Please try again later',
          error
        );
      }
    );
  }

  get fullName() {
    return this.auth.userFullName;
  }

  get userHome() {
    return this.auth.userHomePageUrl;
  }

  logout() {
    this.auth.logout();
  }

  showComingSoonAlert() {
    alert('This feature is coming soon!');
  }
}
