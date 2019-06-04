import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterService } from './registerService';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {


  public imagePath;
  imgURL: any;
  public message: string;
 
 

  message1: string;

  registerForm = this.fb.group({
    FirstName: ['', Validators.required],
    LastName: ['', Validators.required],
    BirthDate:['',Validators.required],
    Address: ['', Validators.required],
    username: ['', Validators.required],
    password: ['', Validators.required],
    reppass: ['', Validators.required],
    ImageUrl: ['', Validators.required],

  });


  constructor(public registerService: RegisterService, public router: Router, private fb: FormBuilder) {
  }
  
  register() {
    this.registerService.login(this.registerForm.value).subscribe((data) => {
      console.log("porukaaa");
      console.log(data);
    });
  }

  preview(files) {
    if (files.length === 0)
      return;
 
    var mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.message = "Only images are supported.";
      return;
    }
 
    var reader = new FileReader();
    this.imagePath = files;
    reader.readAsDataURL(files[0]); 
    reader.onload = (_event) => { 
      this.imgURL = reader.result; 
    }
  }

}