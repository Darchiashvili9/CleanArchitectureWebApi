export class loginRequest {
  public email?: string = '';
  public password?: string = '';

  constructor(email?: string, password?: string) {
      this.email = email,
      this.password = password
  }
}


export class registerRequest {
  public userName?: string = '';
  public email?: string = '';
  public password?: string = '';

  constructor(userName?: string, email?: string, password?: string) {
      this.userName = userName,
      this.email = email,
      this.password = password
  }
}
