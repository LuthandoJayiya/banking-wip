import { Component, OnInit } from '@angular/core';
import {SavingsAccountVM} from '../../../../shared/models/accounts/acc-management-vm';
import { AccountManagementService } from 'src/app/shared/services/accounts/account-management.service';
import { ActivatedRoute } from '@angular/router';
import { registerLocaleData } from '@angular/common';

@Component({
  selector: 'app-savings-account',
  templateUrl: './savings-account.component.html',
  styleUrls: ['./savings-account.component.css'],
})
export class SavingsAccountComponent implements OnInit {
  savingsAccounts: SavingsAccountVM[] = [];
  customerId: number | undefined;

  constructor(
    private accService: AccountManagementService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('customerId');
    if (id) {
      this.customerId = +id;
      this.viewSavingsAccounts();
    } else {
      console.error('No Customer Id is not provided in the route');
    }
  }

  viewSavingsAccounts(): void {
    if (this.customerId !== undefined) {
      this.accService
        .getSavingsAccounts(this.customerId)
        .subscribe((accounts: SavingsAccountVM[]) => {
          this.savingsAccounts = accounts;
        });
    }
  }

  openSavingsAccount(): void {
    if (confirm('Are you sure you want to open a new savings account?')) {
      const newAccount: SavingsAccountVM = {
        accountNumber: '',
        creationDate: new Date(),
        balance: 0,
        customerId: this.customerId,
        branchCode: '255774',
        closeDate: null,
        interestRateId: 1,
      };

      this.accService
        .addSavingsAccount(newAccount)
        .subscribe((addedAccount: SavingsAccountVM) => {
          this.savingsAccounts.push(addedAccount);
          this.viewSavingsAccounts();
        });
    }
  }

  closeSavingsAccount(accountNumber: string): void {
    if (confirm('Are you sure you want to close this account?')) {
      this.accService.deleteSavingsAccount(accountNumber).subscribe(
        (updatedAccount: { closeDate: Date | null }) => {
          const accountToUpdate = this.savingsAccounts.find(
            (account) => account.accountNumber === accountNumber
          );
          if (accountToUpdate) {
            accountToUpdate.closeDate = updatedAccount.closeDate;
          }
        },
        (error: any) => {
          console.error('Error closing account:', error);
        }
      );
    }
  }
}
