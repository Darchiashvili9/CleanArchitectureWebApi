import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

import { QuizService } from '../../services/quiz.service';
import { AnswerDataModel } from '../../models/quote';

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
  quizForm: FormGroup;
  loading = false;
  errorMessage = '';


  constructor(
    formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private quizService: QuizService,
    private fb: FormBuilder) {
    this.settingsForm = formBuilder.group({
      binary: true,
      multiple: false
    });
    this.quizForm = this.fb.group({
      question: ['', Validators.required],
      answers: this.fb.array([
        this.fb.group({ text: [''], isCorrect: [false] }),
        this.fb.group({ text: [''], isCorrect: [false] })
      ])
    });
  }

  toggleBinary(): void {
    const binary = this.settingsForm.get('binary')?.value;
    this.settingsForm.get('multiple')?.setValue(!binary);
  }

  toggleMultiple(): void {
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

  get answers(): FormArray {
    return this.quizForm.get('answers') as FormArray;
  }

  addAnswer(): void {
    this.answers.push(
      this.fb.group({
        text: [''],
        isCorrect: [false]
      })
    );
  }

  removeAnswer(index: number): void {
    this.answers.removeAt(index);
  }

  //submitQuiz(): void {
  //  if (this.quizForm.valid) {
  //    const quoteText = this.quizForm.value.question;
  //    const answers = this.quizForm.value.answers.map((a: any) => a.text);

  //    this.quizService.addQuote(quoteText, answers).subscribe({
  //      next: (res) => {
  //        console.log('Quote added successfully:', res);
  //        this.quizForm.reset();
  //        // optionally reinitialize answer fields:
  //        this.answers.clear();
  //        this.addAnswer();
  //        this.addAnswer();

  //        this.loading = false;
  //      }, error: (error) => {

  //        this.errorMessage = error;
  //        this.loading = false;
  //      });
  //  }

  //}



  submitQuiz(): void {
    if (this.quizForm.valid) {
      const quoteText = this.quizForm.value.question;
      const answers: AnswerDataModel[] = this.quizForm.value.answers.map((a: any) => {
        const answer = new AnswerDataModel();
        answer.text = a.text;
        answer.isCorrect = a.isCorrect ?? false;
        return answer;
      });

      this.quizService.addQuote(quoteText, answers).subscribe({
        next: (res) => {
          console.log('Quote added successfully:', res);
          this.quizForm.reset();
          // optionally reinitialize answer fields:
          this.answers.clear();
          this.addAnswer();
          this.addAnswer();
        },
        error: (err) => {
          console.error('Error adding quote:', err);
        }
      });
    }
  }
}
