import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators'

interface SigninCredentials {
  username: string,
  password: string
}
interface SigninResponse {
  succeeded: boolean,
  message: string,
  token: {
    token: string,
    expirate: Date,
    signedIn: boolean
  }
}
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  rootUrl = 'https://localhost:7084/User';
  signedin = new BehaviorSubject<boolean>(false);
  token = new BehaviorSubject<string>(null);
  expiration = new BehaviorSubject<Date>(null)

  constructor(private http: HttpClient) { }

  signin(credentials: SigninCredentials) {
    const headers = { 'Content-Type': 'application/json' };
    return this.http.post<SigninResponse>(this.rootUrl + '/login', {
      email: credentials.username,
      password: credentials.password
    }, { headers }

    )

      .pipe(
        tap(({ token }) => {
          if (token.token !== null && token.token !== undefined) {
            this.signedin.next(true);
            this.token.next(token.token);
            this.expiration.next(token.expirate);
          } else {
            this.signedin.next(false);
          }


        })
      );
  }
}
