import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../models/users.model';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  private url = environment.api

  constructor(private httpClient: HttpClient) { }

  obterUsers() {
    return this.httpClient.get<User[]>(this.url + '/Users')
  }

}
