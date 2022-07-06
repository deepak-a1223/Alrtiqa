import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AccountService } from './_services/account.service';
import { Inject } from "@angular/core";
import { DOCUMENT } from "@angular/common";
import { Router, Event, NavigationStart, NavigationEnd, NavigationCancel, NavigationError } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'alrtiqa-client';
  showLoading = true;
  constructor(public accountservice: AccountService,
    public translate: TranslateService,
    @Inject(DOCUMENT) private document: Document, private router: Router) {
    translate.addLangs(['en', 'ar']);
    translate.setDefaultLang('en');
    const browserLang = translate.getBrowserLang();
    translate.use('en');
    this.router.events.subscribe((routerEvent: Event) => {
      if (routerEvent instanceof NavigationStart) {
        this.showLoading = true;
      }
      if (routerEvent instanceof NavigationEnd ||
        routerEvent instanceof NavigationCancel || routerEvent instanceof NavigationError) {
        this.showLoading = false;
      }
    })
  }

  languageChange(lang: string) {
    let htmlTag = this.document.getElementsByTagName("html")[0] as HTMLHtmlElement;
    htmlTag.dir = lang === "ar" ? "rtl" : "ltr";
    this.translate.use(lang);
  }
}
