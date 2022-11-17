import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';

const routes: Routes = [
  {path: "", component: HomeComponent},
  {
    path: "",
    canActivate : [AuthGuard],
    runGuardsAndResolvers : 'always',
    children : [
      {path: "members", component: MemberListComponent, canActivate: [AuthGuard] },
      {path: "members/:id", component: MemberDetailComponent},
      {path: "lists", component: ListsComponent},
      {path: "messages", component: MessagesComponent},
    ]
  },


  // {path: "members", component: MemberListComponent, canActivate: [AuthGuard] },
  // {path: "members/:id", component: MemberDetailComponent},
  // {path: "lists", component: ListsComponent},
  // {path: "messages", component: MessagesComponent},

  {path: "errors", component: TestErrorsComponent,pathMatch: "full"},

  {path: "not-found", component: NotFoundComponent,pathMatch: "full"},
  {path: "server-error", component: ServerErrorComponent,pathMatch: "full"},
  {path: "**", component: NotFoundComponent,pathMatch: "full"},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

