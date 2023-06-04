import { Component, Inject, OnInit } from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

import { AuthenticationService } from './../../services/authentication.service';
import { UsersService } from '../../services/users.service';

interface DialogData {
  action: string
  userId: string;
  userName: string;
  email: string;
  role: string,
  isDisabled: boolean;
}

@Component({
  selector: 'app-users-dialog',
  templateUrl: './users-dialog.component.html',
  styleUrls: ['./users-dialog.component.scss']
})
export class UsersDialogComponent implements OnInit {
  userForm: FormGroup;
  action = '';
  loading = false;
  currentUserId = '';
  selectedUserId = '';

  constructor(
    public dialogRef: MatDialogRef<UsersDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private fb: FormBuilder,
    private authenticationService: AuthenticationService,
    private usersService: UsersService) {
    this.userForm = this.fb.group({
      id: new FormControl(''),
      userName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required]),
      password: new FormControl(''),
      role: new FormControl('User', [Validators.required]),
      isDisabled: new FormControl(false),
    });
  }

  editUser(): void {

  }

  removeUser(): void {

  }

  submitUserForm(): void {
    if (this.userForm.invalid) {
      return;
    }

    this.loading = true;

    const data = {
      id: this.userForm.get('id')?.value,
      userName: this.userForm.get('userName')?.value,
      email: this.userForm.get('email')?.value,
      password: this.userForm.get('password')?.value,
      role: this.userForm.get('role')?.value,
      isDisabled: this.userForm.get('isDisabled')?.value
    }

    if (this.action === 'edit') {
      this.usersService.editUser(data).subscribe(() => {
        this.dialogRef.close(true);
        this.loading = false;
      });
    } else if (this.action === 'create') {
      this.usersService.createUser(data).subscribe(() => {
        this.dialogRef.close(true);
        this.loading = false;
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {
    this.authenticationService.currentUser$.subscribe(data => this.currentUserId = data.id)
    this.selectedUserId = this.data.userId;
    this.action = this.data.action;

    this.userForm.get('id')?.setValue(this.data.userId);
    this.userForm.get('userName')?.setValue(this.data.userName);
    this.userForm.get('email')?.setValue(this.data.email);
    this.userForm.get('role')?.setValue(this.data.role);
    this.userForm.get('isDisabled')?.setValue(this.data.isDisabled);

    if (this.action === 'create') {
      this.userForm.get('password')?.setValidators(Validators.required);
    }
  }
}
