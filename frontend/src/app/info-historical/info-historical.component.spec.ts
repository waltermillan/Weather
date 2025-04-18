import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InfoHistoricalComponent } from './info-historical.component';

describe('InfoHistoricalComponent', () => {
  let component: InfoHistoricalComponent;
  let fixture: ComponentFixture<InfoHistoricalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InfoHistoricalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(InfoHistoricalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
