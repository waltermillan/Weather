export interface Weather {
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
  }
  