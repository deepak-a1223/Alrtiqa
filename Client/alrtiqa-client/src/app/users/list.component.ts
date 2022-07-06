import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { first } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  users: User[] = [];
  isApproving: boolean = false;
  isDelete: boolean = false;
  constructor(private accountService: AccountService, private toastr: ToastrService, private router: Router) { }

  ngOnInit() {
    this.loadUsers();
  }

  approveUser(userName: string) {
    const user = this.users.find(x => x.userName === userName);
    this.isApproving = true;
    this.accountService.approveUser(user?.userName!)
      .pipe(first())
      .subscribe({
        error: error => {
          this.toastr.error(error);
          this.isApproving = false;
        }
      });
    this.reloadCurrentRoute();
    this.toastr.success("Successfully Approved", "Approved")
    this.isApproving = false;
  }

  deleteUser(userName: string) {
    const user = this.users.find(x => x.userName === userName);
    this.isDelete = true;
    this.accountService.delete(user?.userName!)
      .pipe(first())
      .subscribe(() => this.users = this.users.filter(x => x.userName !== userName));

    this.toastr.success("Successfully Deleted", "Deleted")
    this.isDelete = false;
  }

  loadUsers() {
    this.accountService.getAll()
      .pipe(first())
      .subscribe(users => this.users = users);
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }

}
