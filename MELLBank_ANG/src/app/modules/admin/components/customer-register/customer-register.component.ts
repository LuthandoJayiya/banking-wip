import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { TIMEOUT } from 'src/app/shared/constants';
import { RegisterCustomerVM } from 'src/app/shared/models/admin/admin-vm';
import { CustomerManageService } from 'src/app/shared/services/customer-manage/customer-manage.service';

@Component({
  selector: 'app-customer-register',
  templateUrl: './customer-register.component.html',
  styleUrls: ['./customer-register.component.css'],
})
export class CustomerRegisterComponent {
  constructor(
    private customerService: CustomerManageService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private location: Location
  ) {}

  customer: RegisterCustomerVM = {
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

  get userHome() {
    return this.customerService.userHomePageUrl;
  }
  onSubmit(form: NgForm) {
    if (form.invalid) {
      return;
    }

    console.log(JSON.stringify(this.customer));

    this.customerService.registerCustomer(this.customer).subscribe((data) => {
      console.log('After Customer Update: ' + data.firstName);
      console.log('After Customer Update: ' + data.lastName);

      setTimeout(() => {
        this.location.back();
      }, TIMEOUT);
    });
  }

  goBack(): void {
    this.location.back();
  }
}
