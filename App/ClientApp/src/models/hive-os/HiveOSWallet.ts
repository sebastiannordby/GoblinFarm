export interface HiveOSWallet {
  id: number;
  name: string;
  wal: string;
  source: string;
  balance: HiveOSWalletBalance;
}

export interface HiveOSWalletBalance {
  description: string;
  value: number;
  valueFiat: number;
}
