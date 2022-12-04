import { Router } from '@angular/router';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from "ngx-toastr";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() usersFromHomeComponent: any;

  @Output() cancelRegister = new EventEmitter();
  registerForm : FormGroup = new FormGroup({});
  maxDate: Date = new Date();
  validationErrors : string[] | undefined;

  intializeForm(){
    // this.registerForm = new FormGroup({
    //   username: new FormControl('',[Validators.required]),
    //   password: new FormControl('',[Validators.required,Validators.minLength(4),Validators.maxLength(8)]),
    //   confirmPassword: new FormControl('',[Validators.required,this.matchValues('password')])
    // })


    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['',[Validators.required]],
      knownAs: ['',[Validators.required]],
      dateOfBirth: ['',[Validators.required]],
      city: ['',[Validators.required]],
      country: ['',[Validators.required]],
      password: ['',[Validators.required,Validators.minLength(4),Validators.maxLength(8)]],
      confirmPassword: ['',[Validators.required,this.matchValues('password')]]
    })



    this.registerForm.controls['password'].valueChanges.subscribe({
      next : () => this.registerForm.controls["confirmPassword"].updateValueAndValidity()
    })
  }


  matchValues(matchTo: string) : ValidatorFn {
    return (control : AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : {notMatching: true}
    }

  }

  constructor(private accountService : AccountService,private toastr : ToastrService, private fb : FormBuilder,private router: Router) { }

  ngOnInit(): void {
    this.intializeForm();

    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18)
  }



  register() {
    const dob = this.getDateOnly(this.registerForm.controls['dateOfBirth'].value);
    const values = {...this.registerForm.value, dateOfBirth: dob};
   this.accountService.register(values).subscribe({
    next : () =>{
      this.router.navigateByUrl("/memebers");
    },
    error : error =>{
    this.validationErrors = error;
    }
   })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  private getDateOnly(dob: string | undefined) {
    if (!dob) return;
    let theDob = new Date(dob);
    //extracting the date only from
    return new Date(theDob.setMinutes(theDob.getMinutes()-theDob.getTimezoneOffset()))
      .toISOString().slice(0,10);
  }


}
