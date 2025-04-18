import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowDataWeatherComponent } from './show-data-weather.component';

describe('ShowDataWeatherComponent', () => {
  let component: ShowDataWeatherComponent;
  let fixture: ComponentFixture<ShowDataWeatherComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShowDataWeatherComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ShowDataWeatherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
