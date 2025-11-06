import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CurrentAccount } from 'src/app/shared/models/accounts/acc-management-vm';
import { CurrentAccountManagementService } from 'src/app/shared/services/current-account-management/current-account-management.service';

@Component({
  selector: 'app-current-account',
  templateUrl: './current-account.component.html',
  styleUrls: ['./current-account.component.css']
})
export class CurrentAccountComponent implements OnInit {
  currentAccounts: CurrentAccount[] = [];
  customerId?: any;

  constructor(
    private currAccMana: CurrentAccountManagementService,
    private route: ActivatedRoute, private router: Router
  ) { }


  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('customerId');
    if (id) {
      this.customerId = + id;
      this.getAllCurrentAccountForCustomer(this.customerId);
    } else {
      console.error('No Customer Id is not provided in the route');
    }
  }

  getCurrentAccounts() {
    this.currAccMana.getAllCurrentAccounts().subscribe(
      (response: any) => {
        if (Array.isArray(response)) {
          response.forEach((account: any) => {
            let currAccounts = {
              CurrentId: account.currentId,
              AccountNumber: account.accountNumber,
              CreationDate: account.creationDate,
              Balance: account.balance,
              BranchCode: account.branchCode,
              OverdraftAmount: account.overdraftAmount,
              OverdraftRate: account.overdraftRate,
              CloseDate: account.closeDate,
              CustomerId: account.customerId,
            };
            this.currentAccounts.push(currAccounts);
          });
        } else {
        }
        console.log('getCurrentAccounts(): ', this.currentAccounts);
      },
      (error) => {
        console.error('Error fetching current accounts', error);
      }
    );
  }

  getAllCurrentAccountForCustomer(id: number) {
    if (this.customerId !== undefined) {
      this.currAccMana.getAllCurrentAccountsForCustomer(id).subscribe(
        (response: any) => {
          if (Array.isArray(response)) {
            response.forEach((account: any) => {
              let currAccounts = {
                CurrentId: account.currentId,
                AccountNumber: account.accountNumber,
                CreationDate: account.creationDate,
                Balance: account.balance,
                BranchCode: account.branchCode,
                OverdraftAmount: account.overdraftAmount,
                OverdraftRate: account.overdraftRate,
                CloseDate: account.closeDate,
                CustomerId: account.customerId,
              };
              this.currentAccounts.push(currAccounts);
            });
            console.log("getAllCurrentAccountForCustomer(): ", this.currentAccounts);
          }
        }
      )
    }
  }

  closeCurrentAccount(accountId: string) {
    if (confirm('Are you sure you want to close this account?')) {
      this.currAccMana.closeAccount(accountId).subscribe({
        next: (account) => {
          if (account) {
            console.log('Account closed successfully', account);
          } else {
            console.log('Account closure failed or no response received.');
          }
        },
        error: (error) => console.error('Error closing account:', error)
      });
    }
  }

  openAccount(): void {
    if (confirm("Are you sure you want to open a new account?")) {
      const customerId = this.customerId;
      console.log("openAccount(): ", customerId);

      this.currAccMana.openNewAccount(customerId).subscribe({
        next: (account) => {
          console.log('New account created successfully', account);
          alert('New account created successfully!');
          //this.router.navigate(['/bank-staff/account-management']);
          this.getCurrentAccounts();

        },
        error: (error) => console.error('Error creating new account:', error)
      });
    }
  }
}
