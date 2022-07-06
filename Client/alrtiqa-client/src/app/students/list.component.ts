import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AgGridAngular } from 'ag-grid-angular';
import { CellClickedEvent, ColDef, GridReadyEvent } from 'ag-grid-community';
import { Observable, Subscription } from 'rxjs';
import { ButtonRendererComponent } from './render/button-renderer.component';
import { ActivatedRoute, Router } from '@angular/router';
import { LanguageService } from '../_services/language.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit, OnDestroy {
  frameworkComponents: any;
  private gridApi: any;
  private gridColumnApi: any;
  rtl: boolean = false;
  showGrid: boolean = false;
  subs$: Subscription = new Subscription();

  // Each Column Definition results in one Column.
  public columnDefs: ColDef[] = [];

  // DefaultColDef sets props common to all Columns
  public defaultColDef: ColDef = {
    sortable: true,
    filter: true,
  };

  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute
    , private langService: LanguageService) {
    this.frameworkComponents = {
      buttonRenderer: ButtonRendererComponent
    }
  }
  ngOnDestroy(): void {
    this.subs$.unsubscribe();
  }

  ngOnInit(): void {
    this.subs$ = this.langService.getLanguage().subscribe((message) => {
      this.toggleLang(message);
    });
    this.columnDefs = [

      { headerName: "Make", field: 'make', sortingOrder: ["asc", "desc"] },
      { headerName: "Model", field: 'model' },
      { headerName: "Price", field: 'price' },
      {
        headerName: 'Button Col 1',
        cellRenderer: 'buttonRenderer',
        cellRendererParams: {
          onClick: this.onBtnViewClick.bind(this),
          label: 'View'
        }
      },]
  }
  // Example load data from sever
  onGridReady(params: GridReadyEvent) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;

    let data: any = [
      { "make": "Honda", "model": "2017", "price": "2000" },
      { "make": "Abc", "model": "2017", "price": "2000" },
      { "make": "Honda", "model": "2017", "price": "2000" }
    ]
    params.api.setRowData(data);
    // this.rowData$ = this.http
    //   .get<any[]>('https://www.ag-grid.com/example-assets/row-data.json');
  }

  onBtnViewClick(e: any) {
    this.router.navigate(['detail', e.rowData.make], { relativeTo: this.route });
  }

  toggleLang(lang: string): void {
    if (lang === 'en') {
      this.rtl = false;
    } else {
      this.rtl = true;
    }
    this.showGrid = false;
    setTimeout(() => {
      this.showGrid = true;
    });
  }
}
