import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { HiveOSWallet } from '../../models/hive-os/HiveOSWallet';
import { HiveOSWorker } from '../../models/hive-os/HiveOSWorker';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public workers: HiveOSWorker[];
  public wallets: HiveOSWallet[];
  public coinbaseResult: CoinbaseResult;
  public hiveOnEarnings: HiveOnEarnings;
  public earningsForTodayNok: string;
  public earningsForWeekNok: string;
  public summary: GoblinSummary;
  public electricityResult: ElectricityResult;

  constructor(@Inject('BASE_URL') private baseUrl: string, private http: HttpClient) {

  }

  public ngOnInit(): void {
    this.http.get<GoblinSummary>(this.baseUrl + 'Goblin/Summary').subscribe((result) => {
      this.summary = result;
    }, error => console.error(error));

    this.http.get<ElectricityResult>(this.baseUrl + 'Goblin/ElectricityCost').subscribe((result) => {
      this.electricityResult = result;
    }, error => console.error(error));

    //this.http.get<CoinbaseAccount>(this.baseUrl + 'Goblin/WalletBalance/ETH').subscribe(result => {
    //  console.log('CoinbaseBalance: ', result);
    //  this.ethAccount = result;
    //});
  }
}

interface CoinbaseResult {
  data: {
    currency: string;
    rates: {
      NOK: string;
    }
  }
}

interface HiveOnEarnings {
  expectedReward24H: number;
  expectedRewardWeek: number;
}

interface GoblinSummary {
  moneySummaries: GoblinMoney[];
  workerSummaries: GoblinWorkerSummary[];
}

interface GoblinWorkerSummary {
  workerName: string;
  estimated24HNOK: number;
  walletSummaries: GoblinWalletSummary[];
}

interface GoblinWalletSummary {
  walletName: string;
  walletId: number;
  walletAddress: string;
  coin: string;
  nokExchangeRate: number;
  estimated24HNOK: string;
  money: GoblinMoney;
}

interface ElectricityResult {
  accumulatedWattageUse: number;
  totalElectricityCostOre: number;
  totalElectricityCostKrone: number;
}

interface GoblinMoney {
  amount: number;
  amountInNOK: number;
  currency: string;
  base: string;
}


//interface CoinbaseAccount {
//  name: string;
//  primary: boolean;
//  type: string;
//  currency: CoinbaseAccountCurrency;
//  balance: CoinbaseAccountMoney;
//  createdAt: Date;
//  updatedAt: Date;
//}

//interface CoinbaseAccountCurrency {
//  code: string;
//  name: string;
//  color: string;
//  sortIndex: string;
//  exponent: number;
//  type: string;
//  addressRegex: string;
//  assetId: string;
//}
