import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { QuotesDialogComponent } from '../quotes-dialog/quotes-dialog.component';
import { DialogComponent } from '../dialog/dialog.component';
import { QuotesService } from '../../services/quotes.service';

@Component({
  selector: 'app-quotes',
  templateUrl: './quotes.component.html',
  styleUrls: ['./quotes.component.scss']
})
export class QuotesComponent implements OnInit {
  quotes: any;
  loading = false;

  constructor(
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
    private quotesService: QuotesService) {}

  submitQuoteForm() {

  }

  getQuotes(): void {
    this.loading = true;
    this.quotesService.getQuotes().subscribe((data) => {
      this.quotes = data;
      this.loading = false;
    });
  }

  createQuote() {
    const dialogRef = this.dialog.open(QuotesDialogComponent, {
      data: {
        action: 'create',
        quoteText: '',
        answers: [{'text': ''},{'text': ''},{'text': ''}],
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.snackBar.open('Successfully created!', '', {
          duration: 2000,
          panelClass: 'snackbar-success'
        });
        this.getQuotes();
      }
    });
  }

  editQuote(quote: any): void {
    const dialogRef = this.dialog.open(QuotesDialogComponent, {
      data: {
        action: 'edit',
        quoteId: quote.question.id,
        quoteText: quote.question.text,
        answers: quote.answers,
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.snackBar.open('Successfully edited!', '', {
          duration: 2000,
          panelClass: 'snackbar-success'
        });
        this.getQuotes();
      }
    });
  }

  deleteQuote(quoteId: number):void {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        title: 'Are you sure you want to delete the quote?',
        cancel: 'Cancel',
        confirm: 'Delete'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.quotesService.deleteQuote(quoteId).subscribe(() => {
          this.snackBar.open('Successfully deleted!', '', {
            duration: 2000,
            panelClass: 'snackbar-success'
          });

          this.getQuotes();
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
    this.getQuotes();
  }
}
