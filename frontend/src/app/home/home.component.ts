import { Component, OnInit } from '@angular/core';
import { GLOBAL } from '../configuration/configuration.global';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  appName: string = '';
  legalNameApp: string = '';
  versionApp: string = '';
  welcomeMessage: string = '';
  appFullName: string = '';
  userName: string = '';
  externalApiResource: string = '';

  constructor(private authService: AuthService) {
    this.appFullName = GLOBAL.appFullName;
    this.appName = GLOBAL.appName;
    this.legalNameApp = GLOBAL.appLegalName.replace(
      '__YEAR__',
      GLOBAL.currentYear
    );
    this.versionApp = GLOBAL.appVersion;
    this.welcomeMessage = GLOBAL.welcomeMessage;
    this.externalApiResource = GLOBAL.externalApiResource;
  }

  ngOnInit() {
    this.authService.userName$.subscribe((userName) => {
      this.userName = userName;
    });
  }
}
