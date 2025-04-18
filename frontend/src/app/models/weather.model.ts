export class Weather {
  //Properties
  datetime: string;
  location: string;
  temperature: number;
  tempmax: number;
  tempmin: number;
  humidity: number;
  precipprob: number;
  sunrise: Date;
  sunset: Date;
  description: string;
  pressure: number;
  icon: string;

  //Constructor
  constructor() {
    this.datetime = '';
    this.location = '';
    this.temperature = 0;
    this.tempmax = 0;
    this.tempmin = 0;
    this.humidity = 0;
    this.precipprob = 0;
    this.sunrise = new Date();
    this.sunset = new Date();
    this.description = '';
    this.pressure = 0;
    this.icon = '';
  }
}
