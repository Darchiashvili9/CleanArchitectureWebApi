import { Role } from "./role";

export class User {
  id: string = '';
  username: string = '';
  email: string = '';
  token: string = '';
  expiration: string = '';
  role: Role = Role.User;
}
