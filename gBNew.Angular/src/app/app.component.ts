import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserService } from './services/users.service';
import { User } from './models/users.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'gBNew.Angular';

  users: User[] = []

  constructor(private userService: UserService) {
    this.obterUsersCadastrados();
  }

  obterUsersCadastrados() {
    this.userService.obterUsers().subscribe(users => this.users = users);
  }

}
