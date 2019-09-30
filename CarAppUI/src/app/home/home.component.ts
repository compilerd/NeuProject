import {Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { AuthenticationService, UserService } from '../_services';


@Component({templateUrl:'home.component.html'})
export class HomeComponent implements OnInit{

    currentUser: any;
    users =  [];

    constructor(
        private authenticationService: AuthenticationService,
        private userService: UserService
    ){
        this.currentUser = authenticationService.currentUserValue;

    }

    ngOnInit(){
        this.loadAllUsers();
    }
 
    deleteUser(id){
        this.userService.delete(id)
        .pipe(first())
        .subscribe(() => this.loadAllUsers())
    }
   
    private loadAllUsers(){
        this.userService.getAll()
        .pipe(first())
        .subscribe(users => this.users = users);
    }


}