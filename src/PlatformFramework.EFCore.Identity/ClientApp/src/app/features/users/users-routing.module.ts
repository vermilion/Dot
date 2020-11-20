import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { UserResolver } from "./resolvers/user.resolver";
import { UserEditComponent } from "./user-edit/user-edit.component";
import { UsersListComponent } from "./users-list/users-list.component";
import { UsersComponent } from "./users.component";

const routes: Routes = [
    {
        path: "",
        component: UsersComponent,
        children: [{
            path: "",
            component: UsersListComponent
        },
        {
            path: "user/:id",
            component: UserEditComponent,
            resolve: {
                user: UserResolver
            }
        }]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class UsersRoutingModule { }