(self.webpackChunkclient_app=self.webpackChunkclient_app||[]).push([[154],{7154:(t,n,e)=>{"use strict";e.r(n),e.d(n,{UsersModule:()=>F});var o=e(1116),i=e(4375),r=e(1524),a=e(6594),s=e(6252),c=e(1318),l=e(2831),u=e(6728),g=e(2467),p=e(5366),d=e(2693);let z=(()=>{class t{constructor(t,n){this.baseUrl=t,this.http=n}resolve(t){const n=t.paramMap.get("id");return this.http.get(`${this.baseUrl}/api/usersApi/getById?userId=${n}`)}}return t.\u0275fac=function(n){return new(n||t)(p.LFG(g.C),p.LFG(d.eN))},t.\u0275prov=p.Yz7({token:t,factory:t.\u0275fac}),t})();var m=e(2767),h=e(8971),f=e(1843);function Z(t,n){if(1&t&&p._UZ(0,"nz-option",20),2&t){const t=p.oxw().$implicit;p.Q6J("nzValue",t.roleId)("nzLabel",t.title)}}function T(t,n){if(1&t&&(p.ynx(0),p.YNc(1,Z,1,2,"nz-option",19),p.BQk()),2&t){const t=p.oxw(2);p.xp6(1),p.Q6J("ngIf",!t.isLoading)}}function b(t,n){1&t&&(p.TgZ(0,"nz-option",21),p._UZ(1,"i",22),p._uU(2," Loading Data... "),p.qZA())}function q(t,n){if(1&t&&(p.TgZ(0,"nz-card",6),p.TgZ(1,"nz-form-item"),p.TgZ(2,"nz-form-label",7),p._uU(3,"Username"),p.qZA(),p.TgZ(4,"nz-form-control",8),p._UZ(5,"input",9),p.qZA(),p.qZA(),p.TgZ(6,"nz-form-item"),p.TgZ(7,"nz-form-label",7),p._uU(8,"First Name"),p.qZA(),p.TgZ(9,"nz-form-control",8),p._UZ(10,"input",10),p.qZA(),p.qZA(),p.TgZ(11,"nz-form-item"),p.TgZ(12,"nz-form-label",7),p._uU(13,"Last Name"),p.qZA(),p.TgZ(14,"nz-form-control",11),p._UZ(15,"input",12),p.qZA(),p.qZA(),p.TgZ(16,"nz-form-item"),p.TgZ(17,"nz-form-label",7),p._uU(18,"Email"),p.qZA(),p.TgZ(19,"nz-form-control",13),p._UZ(20,"input",14),p.qZA(),p.qZA(),p.TgZ(21,"nz-form-item",15),p.TgZ(22,"nz-form-label",7),p._uU(23,"Role"),p.qZA(),p.TgZ(24,"nz-form-control"),p.TgZ(25,"nz-select",16),p.YNc(26,T,2,1,"ng-container",17),p.YNc(27,b,3,0,"nz-option",18),p.qZA(),p.qZA(),p.qZA(),p.qZA()),2&t){const t=p.oxw();p.xp6(26),p.Q6J("ngForOf",t.rolesList),p.xp6(1),p.Q6J("ngIf",t.isLoading)}}function _(t,n){if(1&t){const t=p.EpF();p.TgZ(0,"button",23),p.NdJ("click",function(){return p.CHM(t),p.oxw().create()}),p._uU(1,"Create"),p.qZA()}}function A(t,n){if(1&t){const t=p.EpF();p.TgZ(0,"button",24),p.NdJ("click",function(){return p.CHM(t),p.oxw().back()}),p._uU(1,"Cancel"),p.qZA()}}let C=(()=>{class t{constructor(t,n,e,o,i){this.baseUrl=t,this.notificationsService=n,this.router=e,this.fb=o,this.http=i,this.isLoading=!1,this.rolesList=[]}ngOnInit(){this.form=this.fb.group({userId:[0,[u.kI.required]],username:[null,[u.kI.required]],firstName:[null,[u.kI.required]],lastName:[null,[u.kI.required]],email:[null,[u.kI.required]],role:this.fb.group({roleId:[null,[u.kI.required]]})}),this.fetchRoles()}create(){this.isLoading=!0,this.http.post(`${this.baseUrl}/api/usersApi/add`,this.form.value).subscribe({next:t=>{this.notificationsService.success("Success","User added"),this.back()},error:t=>{console.log(t),this.notificationsService.error("Error",t)},complete:()=>this.isLoading=!1})}back(){this.router.navigate(["users"])}fetchRoles(){this.isLoading=!0,this.http.post(`${this.baseUrl}/api/rolesApi/getAll`,{excludeAnonymous:!0}).subscribe({next:t=>{this.rolesList=t.items},error:t=>{console.log(t)},complete:()=>this.isLoading=!1})}}return t.\u0275fac=function(n){return new(n||t)(p.Y36(g.C),p.Y36(h.zb),p.Y36(m.F0),p.Y36(u.qu),p.Y36(d.eN))},t.\u0275cmp=p.Xpm({type:t,selectors:[["app-user-create"]],decls:9,vars:1,consts:[["nz-form","","nzLayout","vertical",3,"formGroup"],["nzDirection","vertical"],["nzTitle","General",4,"nzSpaceItem"],["nzDirection","horizontal"],["nz-button","","nzType","primary",3,"click",4,"nzSpaceItem"],["nz-button","","nzType","secondary",3,"click",4,"nzSpaceItem"],["nzTitle","General"],["nzRequired",""],["nzErrorTip","Please input your first name!"],["type","text","nz-input","","formControlName","username","placeholder","UserName"],["type","text","nz-input","","formControlName","firstName","placeholder","First Name"],["nzErrorTip","Please input your last name!"],["type","text","nz-input","","formControlName","lastName","placeholder","Last Name"],["nzErrorTip","Please input your Email!"],["type","text","nz-input","","formControlName","email","placeholder","Email"],["formGroupName","role"],["formControlName","roleId"],[4,"ngFor","ngForOf"],["nzDisabled","","nzCustomContent","",4,"ngIf"],[3,"nzValue","nzLabel",4,"ngIf"],[3,"nzValue","nzLabel"],["nzDisabled","","nzCustomContent",""],["nz-icon","","nzType","loading",1,"loading-icon"],["nz-button","","nzType","primary",3,"click"],["nz-button","","nzType","secondary",3,"click"]],template:function(t,n){1&t&&(p.TgZ(0,"nz-layout"),p.TgZ(1,"nz-content"),p.TgZ(2,"form",0),p.TgZ(3,"nz-space",1),p.YNc(4,q,28,2,"nz-card",2),p.qZA(),p.qZA(),p.qZA(),p.TgZ(5,"nz-footer"),p.TgZ(6,"nz-space",3),p.YNc(7,_,2,0,"button",4),p.YNc(8,A,2,0,"button",5),p.qZA(),p.qZA(),p.qZA()),2&t&&(p.xp6(2),p.Q6J("formGroup",n.form))},directives:[a.hw,a.OK,u._Y,u.JL,r.Lr,u.sg,c.NU,c.$1,a.nX,i.bd,f.SK,r.Nx,f.t3,r.iK,r.Fd,u.Fj,u.JJ,u.u,u.x0,s.Vq,o.sg,o.O5,s.Ip],styles:["[_nghost-%COMP%]{display:contents}[_nghost-%COMP%]   nz-layout[_ngcontent-%COMP%]{height:100%}[_nghost-%COMP%]   nz-layout[_ngcontent-%COMP%]   nz-content[_ngcontent-%COMP%]{padding:.5rem;overflow:auto}[_nghost-%COMP%]   nz-layout[_ngcontent-%COMP%]   nz-content[_ngcontent-%COMP%]   nz-space[_ngcontent-%COMP%]{width:100%}[_nghost-%COMP%]   nz-layout[_ngcontent-%COMP%]   nz-footer[_ngcontent-%COMP%]{text-align:end;padding:.5rem}[_nghost-%COMP%]   nz-layout[_ngcontent-%COMP%]     .ant-card-body, [_nghost-%COMP%]   nz-layout[_ngcontent-%COMP%]     .ant-card-head, [_nghost-%COMP%]   nz-layout[_ngcontent-%COMP%]     .ant-card-head-title{padding:.5rem}"]}),t})();function y(t,n){if(1&t&&p._UZ(0,"nz-option",20),2&t){const t=p.oxw().$implicit;p.Q6J("nzValue",t.roleId)("nzLabel",t.title)}}function U(t,n){if(1&t&&(p.ynx(0),p.YNc(1,y,1,2,"nz-option",19),p.BQk()),2&t){const t=p.oxw(2);p.xp6(1),p.Q6J("ngIf",!t.isLoading)}}function N(t,n){1&t&&(p.TgZ(0,"nz-option",21),p._UZ(1,"i",22),p._uU(2," Loading Data... "),p.qZA())}function O(t,n){if(1&t&&(p.TgZ(0,"nz-card",6),p.TgZ(1,"nz-form-item"),p.TgZ(2,"nz-form-label",7),p._uU(3,"Username"),p.qZA(),p.TgZ(4,"nz-form-control",8),p._UZ(5,"input",9),p.qZA(),p.qZA(),p.TgZ(6,"nz-form-item"),p.TgZ(7,"nz-form-label",7),p._uU(8,"First Name"),p.qZA(),p.TgZ(9,"nz-form-control",8),p._UZ(10,"input",10),p.qZA(),p.qZA(),p.TgZ(11,"nz-form-item"),p.TgZ(12,"nz-form-label",7),p._uU(13,"Last Name"),p.qZA(),p.TgZ(14,"nz-form-control",11),p._UZ(15,"input",12),p.qZA(),p.qZA(),p.TgZ(16,"nz-form-item"),p.TgZ(17,"nz-form-label",7),p._uU(18,"Email"),p.qZA(),p.TgZ(19,"nz-form-control",13),p._UZ(20,"input",14),p.qZA(),p.qZA(),p.TgZ(21,"nz-form-item",15),p.TgZ(22,"nz-form-label",7),p._uU(23,"Role"),p.qZA(),p.TgZ(24,"nz-form-control"),p.TgZ(25,"nz-select",16),p.YNc(26,U,2,1,"ng-container",17),p.YNc(27,N,3,0,"nz-option",18),p.qZA(),p.qZA(),p.qZA(),p.qZA()),2&t){const t=p.oxw();p.xp6(26),p.Q6J("ngForOf",t.rolesList),p.xp6(1),p.Q6J("ngIf",t.isLoading)}}function L(t,n){if(1&t){const t=p.EpF();p.TgZ(0,"button",23),p.NdJ("click",function(){return p.CHM(t),p.oxw().save()}),p._uU(1,"Save"),p.qZA()}}function P(t,n){if(1&t){const t=p.EpF();p.TgZ(0,"button",24),p.NdJ("click",function(){return p.CHM(t),p.oxw().back()}),p._uU(1,"Cancel"),p.qZA()}}let x=(()=>{class t{constructor(t,n,e,o,i,r){this.baseUrl=t,this.notificationsService=n,this.router=e,this.activatedRoute=o,this.fb=i,this.http=r,this.isLoading=!1,this.rolesList=[]}ngOnInit(){this.form=this.fb.group({userId:[null,[u.kI.required]],username:[null,[u.kI.required]],firstName:[null,[u.kI.required]],lastName:[null,[u.kI.required]],email:[null,[u.kI.required]],role:this.fb.group({roleId:[null,[u.kI.required]]})}),this.fetchRoles(),this.form.patchValue(this.activatedRoute.snapshot.data.user)}save(){this.isLoading=!0;const t=Object.assign(this.form.value,{roleId:1});this.http.post(`${this.baseUrl}/api/usersApi/update`,t).subscribe({next:t=>{this.notificationsService.success("Success","User saved")},error:t=>{console.log(t),this.notificationsService.error("Error",t)},complete:()=>this.isLoading=!1})}back(){this.router.navigate(["users"])}fetchRoles(){this.isLoading=!0,this.http.post(`${this.baseUrl}/api/rolesApi/getAll`,{excludeAnonymous:!0}).subscribe({next:t=>{this.rolesList=t.items},error:t=>{console.log(t)},complete:()=>this.isLoading=!1})}}return t.\u0275fac=function(n){return new(n||t)(p.Y36(g.C),p.Y36(h.zb),p.Y36(m.F0),p.Y36(m.gz),p.Y36(u.qu),p.Y36(d.eN))},t.\u0275cmp=p.Xpm({type:t,selectors:[["app-user-edit"]],decls:9,vars:1,consts:[["nz-form","","nzLayout","vertical",3,"formGroup"],["nzDirection","vertical"],["nzTitle","General",4,"nzSpaceItem"],["nzDirection","horizontal"],["nz-button","","nzType","primary",3,"click",4,"nzSpaceItem"],["nz-button","","nzType","secondary",3,"click",4,"nzSpaceItem"],["nzTitle","General"],["nzRequired",""],["nzErrorTip","Please input your first name!"],["type","text","nz-input","","formControlName","username","placeholder","UserName"],["type","text","nz-input","","formControlName","firstName","placeholder","First Name"],["nzErrorTip","Please input your last name!"],["type","text","nz-input","","formControlName","lastName","placeholder","Last Name"],["nzErrorTip","Please input your Email!"],["type","text","nz-input","","formControlName","email","placeholder","Email"],["formGroupName","role"],["formControlName","roleId"],[4,"ngFor","ngForOf"],["nzDisabled","","nzCustomContent","",4,"ngIf"],[3,"nzValue","nzLabel",4,"ngIf"],[3,"nzValue","nzLabel"],["nzDisabled","","nzCustomContent",""],["nz-icon","","nzType","loading",1,"loading-icon"],["nz-button","","nzType","primary",3,"click"],["nz-button","","nzType","secondary",3,"click"]],template:function(t,n){1&t&&(p.TgZ(0,"nz-layout"),p.TgZ(1,"nz-content"),p.TgZ(2,"form",0),p.TgZ(3,"nz-space",1),p.YNc(4,O,28,2,"nz-card",2),p.qZA(),p.qZA(),p.qZA(),p.TgZ(5,"nz-footer"),p.TgZ(6,"nz-space",3),p.YNc(7,L,2,0,"button",4),p.YNc(8,P,2,0,"button",5),p.qZA(),p.qZA(),p.qZA()),2&t&&(p.xp6(2),p.Q6J("formGroup",n.form))},directives:[a.hw,a.OK,u._Y,u.JL,r.Lr,u.sg,c.NU,c.$1,a.nX,i.bd,f.SK,r.Nx,f.t3,r.iK,r.Fd,u.Fj,u.JJ,u.u,u.x0,s.Vq,o.sg,o.O5,s.Ip],styles:["[_nghost-%COMP%]{display:contents}[_nghost-%COMP%]   nz-layout[_ngcontent-%COMP%]{height:100%}[_nghost-%COMP%]   nz-layout[_ngcontent-%COMP%]   nz-content[_ngcontent-%COMP%]{padding:.5rem;overflow:auto}[_nghost-%COMP%]   nz-layout[_ngcontent-%COMP%]   nz-content[_ngcontent-%COMP%]   nz-space[_ngcontent-%COMP%]{width:100%}[_nghost-%COMP%]   nz-layout[_ngcontent-%COMP%]   nz-footer[_ngcontent-%COMP%]{text-align:end;padding:.5rem}[_nghost-%COMP%]   nz-layout[_ngcontent-%COMP%]     .ant-card-body, [_nghost-%COMP%]   nz-layout[_ngcontent-%COMP%]     .ant-card-head, [_nghost-%COMP%]   nz-layout[_ngcontent-%COMP%]     .ant-card-head-title{padding:.5rem}"]}),t})();var M=e(9597);function v(t,n){if(1&t){const t=p.EpF();p.TgZ(0,"div"),p.TgZ(1,"button",3),p.NdJ("click",function(){return p.CHM(t),p.oxw().addUser()}),p._uU(2,"Add User"),p.qZA(),p.qZA()}}function I(t,n){if(1&t){const t=p.EpF();p.TgZ(0,"tr"),p.TgZ(1,"td"),p._uU(2),p.qZA(),p.TgZ(3,"td"),p._uU(4),p.qZA(),p.TgZ(5,"td"),p._uU(6),p.ALo(7,"date"),p.qZA(),p.TgZ(8,"td"),p._uU(9),p.ALo(10,"date"),p.qZA(),p.TgZ(11,"td"),p.TgZ(12,"button",10),p.NdJ("click",function(){const n=p.CHM(t).$implicit;return p.oxw(2).edit(n)}),p._UZ(13,"i",11),p.qZA(),p.qZA(),p.qZA()}if(2&t){const t=n.$implicit;p.xp6(2),p.hij(" ",t.username," "),p.xp6(2),p.hij(" ",null==t.role?null:t.role.title," "),p.xp6(2),p.hij(" ",p.lcZ(7,4,t.lastLoginDate)," "),p.xp6(3),p.hij(" ",p.lcZ(10,6,null==t.auditData?null:t.auditData.createDate)," ")}}function k(t,n){if(1&t&&(p.TgZ(0,"nz-table",4,5),p.TgZ(2,"thead"),p.TgZ(3,"tr"),p.TgZ(4,"th"),p._uU(5,"Name"),p.qZA(),p.TgZ(6,"th",6),p._uU(7,"Role"),p.qZA(),p.TgZ(8,"th",7),p._uU(9,"Last Logged In"),p.qZA(),p.TgZ(10,"th",7),p._uU(11,"Created"),p.qZA(),p._UZ(12,"th",8),p.qZA(),p.qZA(),p.TgZ(13,"tbody"),p.YNc(14,I,14,8,"tr",9),p.qZA(),p.qZA()),2&t){const t=p.MAs(1),n=p.oxw();p.Q6J("nzFrontPagination",!1)("nzShowPagination",!1)("nzLoading",n.isLoading)("nzData",n.items),p.xp6(14),p.Q6J("ngForOf",t.data)}}const Y=[{path:"",component:(()=>{class t{constructor(t,n,e,o){this.baseUrl=t,this.router=n,this.activatedRoute=e,this.http=o,this.isLoading=!1,this.items=[],this.page=1,this.quantity=100}ngOnInit(){this.reload()}reload(){this.isLoading=!0,this.http.post(`${this.baseUrl}/api/usersApi/getAll`,{pageSize:this.quantity,pageNumber:this.page}).pipe((0,M.x)(()=>this.isLoading=!1)).subscribe(t=>{this.items=t.items,this.total=t.totalItems})}addUser(){this.router.navigate(["create"],{relativeTo:this.activatedRoute})}edit(t){this.router.navigate(["user",t.userId],{relativeTo:this.activatedRoute})}remove(t){}}return t.\u0275fac=function(n){return new(n||t)(p.Y36(g.C),p.Y36(m.F0),p.Y36(m.gz),p.Y36(d.eN))},t.\u0275cmp=p.Xpm({type:t,selectors:[["app-users-list"]],decls:3,vars:0,consts:[["nzDirection","vertical"],[4,"nzSpaceItem"],["nzTableLayout","fixed",3,"nzFrontPagination","nzShowPagination","nzLoading","nzData",4,"nzSpaceItem"],["nz-button","",3,"click"],["nzTableLayout","fixed",3,"nzFrontPagination","nzShowPagination","nzLoading","nzData"],["table",""],["nzWidth","200px"],["nzWidth","150px"],["nzWidth","70px"],[4,"ngFor","ngForOf"],["nz-button","","nzType","default",3,"click"],["nz-icon","","nzType","edit"]],template:function(t,n){1&t&&(p.TgZ(0,"nz-space",0),p.YNc(1,v,3,0,"div",1),p.YNc(2,k,15,5,"nz-table",2),p.qZA())},directives:[c.NU,c.$1,l.N8,l.Om,l.$Z,l.Uo,l._C,l.p0,o.sg],pipes:[o.uU],styles:["[_nghost-%COMP%]{display:block;height:100%;padding:.5rem}[_nghost-%COMP%] > .card[_ngcontent-%COMP%]{flex:1}[_nghost-%COMP%] > .card[_ngcontent-%COMP%]   .card-body[_ngcontent-%COMP%]{padding:0}"]}),t})(),data:{breadcrumb:"User List"}},{path:"create",component:C,data:{breadcrumb:"User Create"}},{path:"user/:id",component:x,data:{breadcrumb:"User Edit"},resolve:{user:z}}];let w=(()=>{class t{}return t.\u0275fac=function(n){return new(n||t)},t.\u0275mod=p.oAB({type:t}),t.\u0275inj=p.cJS({imports:[[m.Bz.forChild(Y)],m.Bz]}),t})(),F=(()=>{class t{}return t.\u0275fac=function(n){return new(n||t)},t.\u0275mod=p.oAB({type:t}),t.\u0275inj=p.cJS({providers:[z],imports:[[o.ez,w,u.UX,s.LV,a.wm,r.U5,i.vh,c.zf,l.HQ]]}),t})()}}]);