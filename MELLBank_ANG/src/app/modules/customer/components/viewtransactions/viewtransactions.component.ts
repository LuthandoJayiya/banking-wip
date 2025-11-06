import { Component, OnInit } from '@angular/core';
import { TransDetailsVM } from 'src/app/shared/models/transaction/transaction-vm';
import { TranserviceService } from 'src/app/shared/models/transervice/transervice.service';
import { AuthService } from 'src/app/shared/services/auth/auth.service';

@Component({
  selector: 'app-viewtransactions',
  templateUrl: './viewtransactions.component.html',
  styleUrls: ['./viewtransactions.component.css'],
})
export class ViewtransactionsComponent implements OnInit {
  transactions: TransDetailsVM[] = [];

  constructor(
    private transervice: TranserviceService,
    private auth: AuthService
  ) {}

  ngOnInit(): void {
    this.getTransactions();
  }

  getTransactions(): void {
    this.transervice.getTransactions().subscribe(
      (data) => {
        console.log('Fetched data:', data);
        this.transactions = data;
      },
      (error) => {
        console.log('Error fetching transactions:', error);
      }
    );

    console.log('Customer ViewtransactionsComponents: ', this.transactions);
  }

  logout() {
    this.auth.logout();
  }

}
