import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

// Beacuse a service is not a component, the Injectable decorator must be added
// When a service is added also need to be included in the providers array located at the app.module
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model);
  }
}
