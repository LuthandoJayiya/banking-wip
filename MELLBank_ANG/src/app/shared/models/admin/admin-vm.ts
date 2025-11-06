import { CustomerVM } from "../customers/customer-vm";

export interface RegisterCustomerVM extends CustomerVM {
  createSavingsAccount: boolean;
  createCurrentAccount: boolean;
}
