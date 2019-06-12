import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterService } from './registerService';
import { ImageMoj } from './slika';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  fd:FormData;
  mySrc;
  canUpload:boolean;
  message:string;

  registerForm = this.fb.group({
    Email: ['', Validators.required],
    Password: ['', Validators.required],
    ConfirmPassword: ['', Validators.required],
    FirstName: ['', Validators.required],
    LastName: ['', Validators.required],
    BirthDate:['',Validators.required],
    Address: ['', Validators.required],
    ImageUrl: [''],
    IDtypeOfUser:[],

  });

  constructor(public registerService: RegisterService, private fb: FormBuilder) {
    this.canUpload=false;
    this.message="";
  }
  
  hit(){
    this.canUpload=true;
  }  
  unHit(){
    this.canUpload=false;
  }
  register() {
    if(this.registerForm.controls['Password'].value==this.registerForm.controls['ConfirmPassword'].value){
      if(this.registerForm.controls['IDtypeOfUser'].value!=null){
        this.message="";
    this.registerForm.controls['ImageUrl'].setValue(this.base64textString);
    this.registerService.registrate(this.registerForm.value).subscribe((data) => {
      this.message = data;
      
    });
  }else{
    this.message="Please tell us what you are..";

  }
  }else{
    this.message="Passwords does not match.";
  }
  }

  private base64textString:string="";
  
  handleFileSelect(evt){
      var files = evt.target.files;
      var file = files[0];
    
    if (files && file) {
        var reader = new FileReader();

        reader.onload =this._handleReaderLoaded.bind(this);

        reader.readAsBinaryString(file);
    }
  }
  
  _handleReaderLoaded(readerEvt) {
     var binaryString = readerEvt.target.result;
            this.base64textString= btoa(binaryString);
            alert(btoa(binaryString));
            this.mySrc="data:image/png;base64," + this.base64textString;
    }


}
