import {Injectable} from '@angular/core';
import {HttpClient } from '@angular/common/http';
import {BehaviorSubject, Observable} from 'rxJS';
import { map } from 'rxjs/operators';
import { environment } from '@environments/environment';


@Injectable({providedIn: 'root'})
export class AuthenticationService {

    private currentUserSubject: BehaviorSubject<any>;
    public currentUser: Observable<any>;

    constructor(private http: HttpClient){
        this.currentUserSubject = new BehaviorSubject<any>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(){
        return this.currentUserSubject.value;
    }

    login(email, password) {
        return this.http.post<any>(`${environment.apiUrl}/users/authenticate`, {email,password})
        .pipe(map(user => {
             
            localStorage.setItem('currentUser', JSON.stringify(user));
            this.currentUserSubject.next(user);
            return user;

            }));
    }

    logout() {
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
}