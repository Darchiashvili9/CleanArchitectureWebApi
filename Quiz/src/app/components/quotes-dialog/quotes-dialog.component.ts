import { Component, Inject, OnInit } from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

import { QuotesService } from '../../services/quotes.service';

interface DialogData {
  quoteId: number;
  quoteText: string;
  answers: any;
  action: string;
}

@Component({
  selector: 'app-quotes-dialog',
  templateUrl: './quotes-dialog.component.html',
  styleUrls: ['./quotes-dialog.component.scss']
})
export class QuotesDialogComponent implements OnInit {
  quoteForm: FormGroup;
  action = '';
  quoteId = -1;
  loading = false;

  constructor(
    public dialogRef: MatDialogRef<QuotesDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private fb: FormBuilder,
    private quotesService: QuotesService) {
    this.quoteForm = this.fb.group({
      quoteText: new FormControl('', [Validators.required]),
      quoteAnswer1: new FormControl('', [Validators.required]),
      quoteAnswerId1: new FormControl(''),
      quoteAnswer2: new FormControl('', [Validators.required]),
      quoteAnswerId2: new FormControl(''),
      quoteAnswer3: new FormControl('', [Validators.required]),
      quoteAnswerId3: new FormControl(''),
      correctAnswerIndex: new FormControl('', [Validators.required]),
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  submitQuoteForm() {
    if (this.quoteForm.invalid) {
      return;
    }

    this.loading = true;

    const data = {
      quoteId: this.quoteId,
      quoteText: this.quoteForm.get('quoteText')?.value,
      answers: [
        {
          id: this.quoteForm.get('quoteAnswerId1')?.value,
          text: this.quoteForm.get('quoteAnswer1')?.value
        },
        {
          id: this.quoteForm.get('quoteAnswerId2')?.value,
          text: this.quoteForm.get('quoteAnswer2')?.value
        },
        {
          id: this.quoteForm.get('quoteAnswerId3')?.value,
          text: this.quoteForm.get('quoteAnswer3')?.value
        }
      ]
    };

    const correctAnswerIndex = this.quoteForm.get('correctAnswerIndex')?.value;

    data.answers.forEach((el: any, i: any) => {
      if (i === correctAnswerIndex) {
        el.isCorrect = true;
      } else {
        el.isCorrect = false;
      }
    });

    if (this.action === 'edit') {
      this.quotesService.editQuote(data).subscribe(() => {
        this.dialogRef.close(true);
        this.loading = false;
      });
    } else if (this.action === 'create') {
      this.quotesService.createQuote(data).subscribe(() => {
        this.dialogRef.close(true);
        this.loading = false;
      });
    }
  }

  ngOnInit(): void {
    this.action = this.data.action;
    this.quoteId = this.data.quoteId;

    this.quoteForm.get('quoteText')?.setValue(this.data.quoteText);
    this.quoteForm.get('quoteAnswer1')?.setValue(this.data.answers[0].text);
    this.quoteForm.get('quoteAnswerId1')?.setValue(this.data.answers[0].id);
    this.quoteForm.get('quoteAnswer2')?.setValue(this.data.answers[1].text);
    this.quoteForm.get('quoteAnswerId2')?.setValue(this.data.answers[1].id);
    this.quoteForm.get('quoteAnswer3')?.setValue(this.data.answers[2].text);
    this.quoteForm.get('quoteAnswerId3')?.setValue(this.data.answers[2].id);

    this.data.answers.forEach((el: { isCorrect: any; }, i: any) => {
      if (el.isCorrect) {
        this.quoteForm.get('correctAnswerIndex')?.setValue(i);
      }
    });
  }
}
