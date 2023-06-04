import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { tap } from 'rxjs/operators';

import { UsersDialogComponent } from '../users-dialog/users-dialog.component';
import { DialogComponent } from '../dialog/dialog.component';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  displayedColumns: string[] = ['id', 'userName', 'email', 'role', 'actions'];
  dataSource$: any;
  users: any;
  loading = false;

  constructor(
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
    private usersService: UsersService) { }

  getUsers(): void {
    this.loading = true;
    this.dataSource$ = this.usersService.getUsers().pipe(tap(() => {
      this.loading = false;
    }));
  }

  editUser(user: any): void {
    const dialogRef = this.dialog.open(UsersDialogComponent, {
      data: {
        action: 'edit',
        userId: user.id,
        userName: user.userName,
        email: user.email,
        role: user.role,
        isDisabled: user.isDisabled
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.snackBar.open('Successfully edited!', '', {
          duration: 2000,
          panelClass: 'snackbar-success'
        });
        this.loading = false;
        this.getUsers();
      }
    });
  }

  createUser(): void {
    const dialogRef = this.dialog.open(UsersDialogComponent, {
      data: {
        action: 'create',
        userId: '',
        userName: '',
        email: '',
        role: 'User',
        isDisabled: false
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.snackBar.open('Successfully edited!', '', {
          duration: 2000,
          panelClass: 'snackbar-success'
        });
        this.getUsers();
      }
    });
  }

  deleteUser(userId: string): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        title: 'Are you sure you want to delete the user?',
        cancel: 'Cancel',
        confirm: 'Delete'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.usersService.deleteUser(userId).subscribe(() => {
          this.snackBar.open('Successfully deleted!', '', {
            duration: 2000,
            panelClass: 'snackbar-success'
          });

          this.getUsers();
        }, error => {
          this.snackBar.open(error, 'Something went wrong!', {
            duration: 2000,
            panelClass: 'snackbar-error'
          });
        });
      }
    });
  }

  ngOnInit(): void {
    this.getUsers();
  }

}
