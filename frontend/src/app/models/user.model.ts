export class User {
  //Properties
  id: string;
  userName: string;
  password: string;
  createdAt: Date;

  //Constructor
  constructor() {
    this.id = '';
    this.userName = '';
    this.password = '';
    this.createdAt = new Date();
  }
}
