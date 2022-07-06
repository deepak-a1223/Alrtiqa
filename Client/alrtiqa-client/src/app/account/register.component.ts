import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AccountService } from '../_services/account.service'
import { ToastrService } from 'ngx-toastr';
import { roles } from '../_models/roles';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form: FormGroup;
  loading = false;
  submitted = false;
  Role: roles[] = [{ id: 1, roleName: "Admin" }, { id: 2, roleName: "ReportManager" }];

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private toastr: ToastrService
  ) {
    this.form = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      roleName: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  changeRole(e: any) {
    this.roleName?.setValue(e.target.value, {
      onlySelf: true,
    });
  }
  // Access formcontrols getter
  get roleName() {
    return this.f['roleName'];
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.accountService.register(this.form.value)
      .pipe(first())
      .subscribe({
        next: () => {
          this.toastr.success('Registration successful');
          this.router.navigate(['../login'], { relativeTo: this.route });
        },
        error: error => {
          this.toastr.error(error);
          this.loading = false;
        }
      });
  }

}
