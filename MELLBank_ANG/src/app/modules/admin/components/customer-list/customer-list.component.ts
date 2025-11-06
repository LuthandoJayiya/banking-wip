import { Component } from '@angular/core';
import { CustomerVM } from 'src/app/shared/models/customers/customer-vm';
import { CustomerManageService } from 'src/app/shared/services/customer-manage/customer-manage.service';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css'],
})
export class CustomerListComponent {
  customers: CustomerVM[] = [];

  constructor(private cmService: CustomerManageService) {}

  ngOnInit(): void {
    this.cmService.getCustomers().subscribe(
      (data) => {
        this.customers = data;
      },
      (error) => {
        console.error('Failed to fetch customers', error);
      }
    );
  }

  get userHome() {
    return this.cmService.userHomePageUrl;
  }
  comingSoon() {
    alert('Feature no our next sprint');
  }
}
