import { SavingsAccountVM } from "../accounts/acc-management-vm";

export interface CustomerVM {
  customerId: number;
  firstName: string;
  lastName: string;
  username: string;
  email: string;
  phoneNumber: string;
  streetAddress: string;
  city: string;
  province: string;
  postalCode: string;
}


export interface CurrentAccountVM {
  currentId: string;
  accountNumber: string;
  creationDate: string;
  balance: number;
  branchCode: string;
  overdraftAmount: number;
  overdraftRate: number;
  closeDate: string;
  customerId: number;
}

export interface CustomerDetailsVM {
  customerId: number;
  firstName: string;
  lastName: string;
  username: string;
  email: string;
  phoneNumber: string;
  streetAddress: string;
  city: string;
  province: string;
  postalCode: string;
  currentAccounts: [CurrentAccountVM];
  savingsAccounts: [SavingsAccountVM];
}
