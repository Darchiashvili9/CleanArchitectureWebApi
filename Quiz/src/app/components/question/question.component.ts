import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { DialogComponent } from '../dialog/dialog.component';
import { QuizService } from '../../services/quiz.service';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.scss']
})
export class QuestionComponent implements OnInit {
  question: any;
  answer: any;
  answers: any;
  errorMessage = '';
  loading = false;
  loadingQuestion = false;
  mode = '';
  isAnswered = false;
  isCorrect = false;
  correctAnswer: any;
  selectedCorrectAnswer: any;

  constructor(
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
    private quizService: QuizService) { }

  generateQuestion(): void {
    this.loading = true;

    this.quizService.gameMode$.subscribe(data => this.mode = data);

    this.quizService.questionGenerator().subscribe(data => {
      this.question = data.question;

      if (this.mode === 'BINARY') {
        this.generateAnswer(data.answers);
      } else if (this.mode === 'MULTIPLE') {
        this.answers = data.answers;
      }

      this.loading = false;
    }, error => {
      this.errorMessage = error;
      this.loading = false;
    });

  }

  generateAnswer(answers: any): void {
      const random = Math.floor(Math.random() * answers.length);
      this.answer = answers[random];
  }

  sendResult(currentAnswerId?: number, currentAnswer?: boolean): void {
    this.loadingQuestion = true;
    this.isAnswered = true;

    const answerId = this.mode === 'BINARY' ? this.answer.id : currentAnswerId;

    this.quizService.questionChecking(this.mode, answerId, currentAnswer).subscribe((data) => {
      this.isCorrect = data.isCorrect;
      this.correctAnswer = data.correctAnswer;
      this.loadingQuestion = false;
    });
  }

  nextQuestion(): void {
    this.generateQuestion();
    this.isAnswered = false;
  }

  restartQuiz(): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        title: 'Are you sure you want to restart the game?',
        content: 'All user history will be deleted!',
        cancel: 'Cancel',
        confirm: 'Restart'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.quizService.restartQuiz().subscribe(() => {
          this.generateQuestion();
          this.errorMessage = '';

          this.snackBar.open('The game is successfully restarted!', '', {
            duration: 2000,
            panelClass: 'snackbar-success'
          });
        }, error => {
          this.snackBar.open(error, 'Something went wrong!', {
            duration: 2000,
            panelClass: 'snackbar-error'
          });
        });
      }
    });
  }

  // TODO: Show loading
  ngOnInit(): void {
    this.generateQuestion();
  }
}
