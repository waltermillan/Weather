export class HistoricalQuery {
  id: number;
  userId: number;
  queryParams: string;
  response: string;
  queriedAt: string;

  constructor() {
    this.id = 0;
    this.userId = 0;
    this.queryParams = '';
    this.response = '';
    this.queriedAt = '';
  }
}
