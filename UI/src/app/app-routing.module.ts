import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin/admin.component';
import { AdminCategoriesComponent } from './admin/components/admin-categories/admin-categories.component';
import { AdminCommentsComponent } from './admin/components/admin-comments/admin-comments.component';
import { AdminPostsComponent } from './admin/components/admin-posts/admin-posts.component';
import { AdminTagsComponent } from './admin/components/admin-tags/admin-tags.component';
import { AdminUsersComponent } from './admin/components/admin-users/admin-users.component';
import { AdminViewCategoryComponent } from './admin/components/admin-view-category/admin-view-category.component';

import { AdminViewPostComponent } from './admin/components/admin-view-post/admin-view-post.component';
import { AdminViewTagComponent } from './admin/components/admin-view-tag/admin-view-tag.component';
import { AdminViewUserComponent } from './admin/components/admin-view-user/admin-view-user.component';
import { DashboardContentComponent } from './admin/components/dashboard-content/dashboard-content.component';
import { LoginComponent } from './auth/login/login.component';
import { HomeComponent } from './home/home/home.component';
import { PostComponent } from './home/home/post/post.component';
import { ViewPostComponent } from './home/home/post/view-post/view-post.component';
import { RegisterComponent } from './home/home/register/register/register.component';
import { NotFoundComponent } from './not-found/not-found.component';

const routes: Routes = [
  {
    path: "",
    redirectTo: "/home/posts",
    pathMatch: "full"
  },
  {
    path: "home/login",
    component:LoginComponent
  },
  {
    path:"home/register",
    component:RegisterComponent
  },
  {
    path: "home",
    component: HomeComponent,
    children: [
      {
        path:"",
        pathMatch:"full",
        redirectTo:"posts",
      },
      {
        path:"posts",
        component:PostComponent
      },
      {
        path:"posts/:id",
        component:ViewPostComponent
      },
    
    ]
  },
  {
    
    path: "admin",
    component: AdminComponent,
    children: [
      {
        path:"",
        pathMatch:"full",
        redirectTo:"dashboard",
      },
      {
        path: "posts",
        component:AdminPostsComponent
      },
    
      {
        path: "posts/:id",
        component:AdminViewPostComponent
      },
      {
        path: "dashboard",
        component:DashboardContentComponent
      },

      {
        path: "categories",
        component:AdminCategoriesComponent
      },
      {
        path: "categories/:id",
        component:AdminViewCategoryComponent
      },
      {
        path: "tags",
        component:AdminTagsComponent
      },
      {
        path: "tags/:id",
        component:AdminViewTagComponent
      },
      {
        path: "users",
        component:AdminUsersComponent
      },
      {
        path: "users/:id",
        component:AdminViewUserComponent
      },
      {
        path: "comments",
        component:AdminCommentsComponent
      },
    ]
  },
  {
    path: "admin",
    redirectTo: "/dashboard",
    pathMatch: "full"
  },
  {
    path: "**",
    component: NotFoundComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
