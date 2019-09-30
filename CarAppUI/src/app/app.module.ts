import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {appRoutingModule} from './app.routing';
import {AppComponent} from './app.component';
import {fakeBackendProvider, JwtInterceptor, ErrorInterceptor } from './_helpers';
import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { RegisterComponent } from './register';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { AlertComponent } from './_components';
import { AuthenticationService, UserService } from './_services';


 

@NgModule({
    imports:[
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        appRoutingModule

    
    ],
    declarations:[AppComponent,
        HomeComponent,
        LoginComponent,
        RegisterComponent,
        AlertComponent
    
    ],
    
    providers:[
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
      
    
    ],

    bootstrap:[AppComponent]

  

})

export class AppModule{};