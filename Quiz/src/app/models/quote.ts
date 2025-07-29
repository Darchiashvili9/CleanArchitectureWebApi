export class quote {
  public quoteText?: string = '';
  public answers?: AnswerDataModel [];

  constructor(quoteText?: string, answers?: AnswerDataModel[]) {
    this.quoteText = quoteText,
      this.answers = answers
  }
}


export class AnswerDataModel {
  id?: number;          // optional (nullable in C#)
  text?: string = '';       // required
  isCorrect?: boolean;   // required
}
