import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StudentsRoutingModule } from './students-routing.module';
import { LayoutComponent } from './layout.component';
import { ListComponent } from './list.component';
import { AgGridModule } from 'ag-grid-angular';
import { ButtonRendererComponent } from './render/button-renderer.component';
import { StudentDetailComponent } from './student-detail.component'
import { TranslateModule } from '@ngx-translate/core';
import { AddStudentComponent } from './add-student.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    LayoutComponent,
    ListComponent,
    ButtonRendererComponent,
    StudentDetailComponent,
    AddStudentComponent
  ],
  imports: [
    CommonModule,
    StudentsRoutingModule,
    AgGridModule.withComponents([ButtonRendererComponent]),
    TranslateModule,
    ReactiveFormsModule
  ]
})
export class StudentsModule { }
