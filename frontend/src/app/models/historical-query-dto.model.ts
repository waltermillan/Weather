export class HistoricalQueryDTO {
  id: number;
  userId: number;
  userName: string;
  queryParams: string;
  response: string;
  queriedAt: string;

  constructor() {
    this.id = 0;
    this.userId = 0;
    this.userName = '';
    this.queryParams = '';
    this.response = '';
    this.queriedAt = '';
  }
}
