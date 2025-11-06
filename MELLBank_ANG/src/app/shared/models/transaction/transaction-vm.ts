export interface TransactionVM {
  transactionId: number;
  accountId: string;
  //customerId: number;
  accountType: string;
  amount: number;
}

export interface TransactionDetailVM {
  transDetailId: number;
  transactionDate: Date;
  amount: number;
  transactionType: string;
  description: string;
  transactionId: number;
}
export interface DepositResponse {
  transaction: TransactionVM;
  transactionDetail: TransactionDetailVM;
}

export interface DepositRequest {
  //customerId: number;
  accountId: string;
  accountType: string;
  amount: number;}


  export interface TransDetailsVM {
    transDetailId: number;
    transactionDate: Date;
    amount: number;
    transactionType: string;
    description?: string;
    transactionId: number;
}


export interface AccountVM {
  accountId: string;
  accountNumber: string;
  creationDate: Date;
  balance: number;
  branchCode: string;
  closeDate: Date | null;
  customerId: number;
  accountType: string;
}

export interface AccountDetailVM {
  accountId: string;
  accountNumber: string;
  creationDate: Date;
  balance: number;
  branchCode: string;
  accountType: string;
  closeDate: string | null;
  totalNumberOfTransactions: number;
  overdraftAmount: number;
  overdraftRate: number;
  interestRateId: number;
}
