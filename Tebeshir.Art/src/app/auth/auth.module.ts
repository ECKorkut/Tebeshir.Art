import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthRoutingModule } from './auth-routing.module';
import { SharedModule } from '../shared/shared.module';
import { SigninComponent } from './signin/signin.component';



@NgModule({
  declarations: [
    SigninComponent
  ],
  imports: [
    CommonModule, AuthRoutingModule, ReactiveFormsModule, SharedModule
  ]
})
export class AuthModule { }
