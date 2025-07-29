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


    const isCorrectAnswer = currentAnswer === this.answer?.isCorrect;


    this.isCorrect = isCorrectAnswer;
    this.isAnswered = true;
    this.correctAnswer = this.answer;

    this.loadingQuestion = false;



  }

  nextQuestion(): void {
    this.generateQuestion();
    this.isAnswered = false;
  }



  // TODO: Show loading
  ngOnInit(): void {
    this.generateQuestion();
  }
}
