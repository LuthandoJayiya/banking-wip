import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomerDetailsVM } from 'src/app/shared/models/customers/customer-vm';
import { CustomerManageService } from 'src/app/shared/services/customer-manage/customer-manage.service';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css'],
})
export class CustomerDetailComponent {
  customerDetails: CustomerDetailsVM | null = null;

  constructor(
    private route: ActivatedRoute,
    private cmService: CustomerManageService,
    private location: Location
  ) {}

  ngOnInit(): void {
    const customerId = +this.route.snapshot.paramMap.get('id')!;
    this.cmService.getCustomerById(customerId).subscribe(
      (data: CustomerDetailsVM) => {
        this.customerDetails = data;
      },
      (error) => {
        console.error('Failed to fetch customer details', error);
      }
    );
  }

  goBack(): void {
    this.location.back();
  }
}
