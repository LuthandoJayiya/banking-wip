export interface SavingsAccountVM {
  accountNumber: string;
  creationDate: Date;
  balance: number;
  customerId: number | undefined;
  branchCode: string;
  closeDate: Date | null;
  interestRateId: number;
}
export interface CurrentAccount {
  CurrentId:string;
  AccountNumber:string;
  CreationDate:Date|null;
  Balance: number;
  BranchCode:string;
  OverdraftAmount :number;
  OverdraftRate:number;
  CloseDate: Date | null;
  CustomerId:number | null;
  }
  