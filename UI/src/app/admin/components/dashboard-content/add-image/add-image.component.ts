import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ImagesService } from 'src/app/services/Images/images.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';

@Component({
  selector: 'app-add-image',
  templateUrl: './add-image.component.html',
  styleUrls: ['./add-image.component.css']
})
export class AddImageComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<AddImageComponent>,private is:ImagesService,  private ts:TranslateToEngService) { }


  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;


  addImage:FormGroup;
  empty:boolean=false;

  
  errorMsg:string;
  resp:string;

  onFileChange(event:any) {
   
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      if(file.type==""){
        this.empty=true;
      }
      else{
        this.empty=false;
      }
      this.addImage.patchValue({
        fileSource: file,
        imageType:file.type,
      });
    }
  } 



  ngOnInit(): void {
    this.translateChng()
    this.addImage=new FormGroup({
    
        fileSource: new FormControl(''),
        
        //.jpg, .png, .jpeg, .gif, .JPG, .PNG, .JPEG, .GIF.
        imageType: new FormControl('',[Validators.pattern('(image\/(jpe?g|JPE?G|png|PNG|gif|GIF))')])
    });
  }

  onSubmit():void{
    
    var formData: any = new FormData();

    formData.append('Image', this.addImage.get('fileSource')?.value);

    this.is.addImage(formData).subscribe({
      next: data => {
        this.errorMsg='';
        if(!this.translate){
          this.resp='Slika je uspešno dodata'
        }
        else{
          this.resp='Image successfully added'
        }
        setTimeout(() => {
          this.dialogRef.close(true);
        }, 2000);
      },
      error: err => {
        console.log(err)
        if(!this.translate){
          this.errorMsg=err.error.message;
        }
      }
    })

  }


  translateChng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
    })
  }




}
