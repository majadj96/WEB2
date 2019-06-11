import { Component, OnInit } from '@angular/core';
import { Line } from 'src/app/models/Line';
import { FormGroup, FormBuilder } from '@angular/forms';
import { LineMeshAdminService } from '../../services/line-mesh-admin.service';


@Component({
  selector: 'app-line-mesh-admin',
  templateUrl: './line-mesh-admin.component.html',
  styleUrls: ['./line-mesh-admin.component.css']
})
export class LineMeshAdminComponent implements OnInit {

  public isBtnAddClicked: boolean;
  public isBtnEditClicked: boolean;
  lines: Line[];
  message: string;
  messageEdit: string;

  line: Line;
  public editForm: FormGroup;
  public addForm: FormGroup;

  TypeLine:Array<Object> = [
    {name: "City"},
    {name: "Village"}
  
  ];

  constructor(private fb: FormBuilder,private LineMeshAdminService: LineMeshAdminService) {
    this.addForm = this.fb.group({
      number: [''],
      typeOfLine: ['']

    });
    this.editForm = this.fb.group({
      number: [''],
      typeOfLine: ['']
    });

    this.isBtnAddClicked = false;
    this.isBtnEditClicked = false;
    this.lines = new Array<Line>();
    this.line = new Line();
    this.message="";
    this.messageEdit="";
   }

  async ngOnInit() {
    this.lines = await this.LineMeshAdminService.getLines();
    this.message="";
    this.messageEdit="";
  }

  public async addLine(){
    this.isBtnAddClicked = true;
  }

  public async onSubmit(){

    
    
    this.line.Number= this.addForm.controls['number'].value;
    this.line.TypeOfLine = this.addForm.controls['typeOfLine'].value;
    
    this.LineMeshAdminService.addLine(this.line).subscribe(data=>{
      this.message=data; 
      this.getLines();
      }, err => console.log(err));

    
  }

  public async getLines(){
    
    if(this.message == "ok"){
      this.isBtnAddClicked = false;
    this.message=" ";
    }
    if(this.messageEdit == "ok"){
      alert("okkkkk");
      this.isBtnEditClicked = false;
    this.messageEdit=" ";
    }

    this.lines = await this.LineMeshAdminService.getLines();
  }


  public  editLine(line){
    this.isBtnEditClicked = true;
    this.editForm = this.fb.group({
      number: [line.Number],
      typeOfLine: [line.TypeOfLine],
      IDTypeOfLine:[line.IDTypeOfLine]

    });
  }


  public async onSubmitEdit(){

    this.line.Number= this.editForm.controls['number'].value;
    this.line.TypeOfLine = this.editForm.controls['typeOfLine'].value;
    this.line.IDtypeOfLine = this.editForm.controls['IDTypeOfLine'].value;
alert(this.editForm.controls['typeOfLine'].value);
   this.LineMeshAdminService.editLine(this.line).subscribe(data=>{
      this.getLines();
      this.messageEdit=data;
     
    }, err => console.log(err));

    this.isBtnEditClicked = false;

  }

  public deleteLine(line){
    this.LineMeshAdminService.deleteLine(line).subscribe(()=>{
      this.getLines();
     
    }, err => console.log(err));
  }
}
