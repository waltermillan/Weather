import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InfoForecastComponent } from './info-forecast.component';

describe('InfoForecastComponent', () => {
  let component: InfoForecastComponent;
  let fixture: ComponentFixture<InfoForecastComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InfoForecastComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(InfoForecastComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
