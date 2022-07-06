import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { LanguageService } from '../_services/language.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  flagClass: string = "flag-icon flag-icon-gb";
  selectedLang: string = "English";
  @Output() langChanged = new EventEmitter<string>();
  constructor(public accountservice: AccountService, private langService: LanguageService) { }

  ngOnInit(): void {
  }

  logout() {
    this.accountservice.logout();
  }

  changeLanguage(language: string) {
    if (language === 'en') {
      this.flagClass = "flag-icon flag-icon-gb";
      this.selectedLang = "English";
    } else if (language === 'ar') {
      this.flagClass = "flag-icon flag-icon-ae";
      this.selectedLang = "Arablic";
    }
    this.langService.setLanguage(language);
    this.langChanged.emit(language);
  }

}
