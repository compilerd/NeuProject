import {Component, OnInit} from '@angular/core';
import { Router } from '@angular/Router';
import { FormBuilder, FormGroup,  Validators} from '@angular/forms';
import { first } from 'rxjs/operators';

import { UserService, AuthenticationService, AlertService} from '../_services'


@Component({ templateUrl:'register.component.html'})

export class RegisterComponent implements OnInit{
    registerForm: FormGroup;
    loading = false;
    submitted = false;
    error:string;


    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private userService: UserService,
        private authenticService: AuthenticationService,
        private alertService: AlertService
    ){
        if (this.authenticService.currentUserValue){
            this.router.navigate(['/']);

        }
    }

    ngOnInit(){
        this.registerForm = this.formBuilder.group({
            firstName: ['', Validators.required],
            secondName:['', Validators.required],
            email:['', Validators.required],
            password:['', Validators.required]
        });
    }

    get f() { return this.registerForm.controls}

    onSubmit(){
        this.submitted = true;

        this.alertService.clear();

        if(this.registerForm.invalid){
            return;
        }

        this.loading = true;

        this.userService.register(this.registerForm.value)
        .pipe(first())
        .subscribe(
            data => {
                this.router.navigate(['/login'], { queryParams: { registered: true }});
            },
            error => {
                this.alertService.error(error);
                this.loading = false;
            });
        }
    }
    
