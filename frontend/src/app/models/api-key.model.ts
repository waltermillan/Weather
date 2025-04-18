export class ApiKey {
  id: number;
  userId: string;
  key: string;
  provider: string;
  createdAt: Date;

  constructor() {
    this.id = 0;
    this.userId = '';
    this.key = '';
    this.provider = '';
    this.createdAt = new Date();
  }
}
