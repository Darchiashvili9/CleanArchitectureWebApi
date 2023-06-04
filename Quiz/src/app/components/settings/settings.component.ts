import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

import { QuizService } from '../../services/quiz.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {
  settingsForm: FormGroup;
  binary = true;
  multiple = false;
  mode = '';

  constructor(
    formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private quizService: QuizService) {
    this.settingsForm = formBuilder.group({
      binary: true,
      multiple: false
    });
  }

  toggleBinary(): void {
    const binary = this.settingsForm.get('binary')?.value;
    this.settingsForm.get('multiple')?.setValue(!binary);
  }

  toggleMultiple():void {
    const multiple = this.settingsForm.get('multiple')?.value;
    this.settingsForm.get('binary')?.setValue(!multiple);
  }

  submitSettingsForm(): void {
    if (this.settingsForm.get('binary')?.value) {
      this.quizService.selectGameMode('BINARY');
    }

    if (this.settingsForm.get('multiple')?.value) {
      this.quizService.selectGameMode('MULTIPLE');
    }

    this.snackBar.open('Successfully changed!', '', {
      duration: 2000,
      panelClass: 'snackbar-success'
    });
  }

  ngOnInit(): void {
    this.quizService.gameMode$.subscribe(data => this.mode = data);

    if (this.mode === 'BINARY') {
      this.settingsForm.get('binary')?.setValue(true);
      this.settingsForm.get('multiple')?.setValue(false);
    } else if (this.mode === 'MULTIPLE') {
      this.settingsForm.get('binary')?.setValue(false);
      this.settingsForm.get('multiple')?.setValue(true);
    }
  }

}
