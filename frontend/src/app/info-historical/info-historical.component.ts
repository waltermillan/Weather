import { Component, OnInit } from '@angular/core';
import { HistoricalQueryDTO } from '../models/historical-query-dto.model';
import { HistoricalQueryService } from '../services/historical-query.service';
import { Weather } from '../models/weather.model';

@Component({
  selector: 'app-info-historical',
  templateUrl: './info-historical.component.html',
  styleUrl: './info-historical.component.css',
})
export class InfoHistoricalComponent implements OnInit {
  historicalQueries: HistoricalQueryDTO[];

  constructor(private historicalQueryService: HistoricalQueryService) {
    this.historicalQueries = [];
  }

  ngOnInit(): void {
    this.getHistoricalQueries();
  }

  getHistoricalQueries() {
    this.historicalQueryService.getAll().subscribe({
      next: (data) => {
        this.historicalQueries = data.sort((a, b) =>
          b.queriedAt.localeCompare(a.queriedAt)
        );
      },
      error: (error) => {
        console.error('Error getting historical Queries.');
      },
    });
  }

  selectedResponse: Weather | null = null;
  selectedCity: string = '';
  showPopup: boolean = false;

  openPopup(queryParams: string, response: string) {
    try {
      this.selectedResponse = JSON.parse(response) as Weather;
    } catch (e) {
      console.error('Error parsing weather response:', e);
      this.selectedResponse = null;
    }

    this.selectedCity = queryParams;
    this.showPopup = true;
  }

  closePopup() {
    this.showPopup = false;
  }
}
