import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { AccountService } from './../../_services/account.service';
import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { MembersService } from 'src/app/_services/members.service';
import { Member } from 'src/app/_models/Member';
import { User } from 'src/app/_models/User';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  //get reference to the variable
  @ViewChild('editForm') editForm: NgForm;
  member: Member;
  user: User;

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private accountService: AccountService, private membersService: MembersService, private toastr: ToastrService) {
    //we are getting the current user from the observale
    accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadMember()
  }

  loadMember() {
    // we are getting the current user details from the api
    this.membersService.getMember(this.user.username).subscribe(member => this.member = member);
  }

  updateMember() {
    console.log(this.member);
    this.membersService.updateMember(this.member).subscribe(() => {
      this.toastr.success("profile updated succesfully");
      //we reset the form // we add this.member so we can fill some of those values
      this.editForm.reset(this.member);

      //[(ngModel)] gives of model binding
      // name property is used by ng
    })

  }

}
