import { Component, OnInit, ElementRef } from "@angular/core";
import { Globals } from "../../../models/lib/globals";
import {
  EnumMenuItem,
  RoleService,
  Role,
  CreateRoleRequest,
  AuditActionResultOfRole,
  UserRoleModel,
  UpdateRoleRequest,
  RolesUsersView,
  ReportModel,
  UserService,
  AuditActionResultOfICollectionOfUserModel,
  UserModel,
  AuditActionResultOfICollectionOfRolesUsersView,
} from "../../../services/api.client.generated";
import { take } from "rxjs/operators";
import { environment as env } from "../../../../environments/environment";
import { responseHandler } from "../../../utils/responseHandler";
import { ViewSaved } from "../../../models/lib/ViewSaved";
import { ColumnsSaved } from "../../../models/lib/ColumnsSaved";
import { CommonGrid } from "../../../models/lib/CommonGrid";
import { ToastrService } from "ngx-toastr";
import { Observable } from "rxjs";
import { AllowedActions } from "../../../../app/models/lib/AllowedActions";
import {
  copyObj,
  pushIfNotExists,
  emptyArray,
} from "../../../../app/models/lib/Utils";
import { GridSaved } from "../../../../app/models/lib/GridSaved";
import { EnumColumnType } from "../../../../app/models/enums/EnumColumnType";
import * as moment from "moment";
import * as _ from "lodash";
import { callFunctionWithFilters } from "../../../models/lib/Utils";
import { LazyLoadEvent } from "primeng/api";

declare let jQuery: any;

@Component({
  selector: "app-role",
  templateUrl: "./role.component.html",
  styleUrls: ["./role.component.scss"],
})
export class RoleComponent implements OnInit {
  userPrivileges: AllowedActions;
  menuItems = EnumMenuItem;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  roles: any[];
  allRoles: any[] = [];
  display: boolean = false;
  currentRole: Role;
  data: Array<Role>;
  availableRoles: Array<Role>;
  isCertificationRole: any[];
  rolesUsers: Array<RolesUsersView> = new Array<RolesUsersView>();
  roleUsers: Array<RolesUsersView> = new Array<RolesUsersView>();
  showGrid: boolean = false;
  users: UserModel[];
  userOptions: RolesUsersView[] = [];
  totalRecords: number = 0;
  roleUsersPopupGrid: GridSaved;
  roleUsersPopupModel: ReportModel;
  canGetRoles: boolean = false;

  constructor(
    public globals: Globals,
    public cg: CommonGrid,
    private toastr: ToastrService,
    private elem: ElementRef,
    private roleService: RoleService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.currentRole = this.getCurrentRole(undefined);

    this.gridStorageId =
      "rolesGrid" + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: "id", label: "Id", visible: true }),
      new ColumnsSaved({ id: "name", label: "Name", visible: true }),
      new ColumnsSaved({
        id: "isCertificationRole",
        label: "Is Certification Role",
        visible: true,
      }),
      new ColumnsSaved({
        id: "parentRoles",
        label: "Parent Roles",
        visible: true,
      }),
      new ColumnsSaved({
        id: "assignedUsers",
        label: "Assigned Users",
        visible: true,
      }),
      new ColumnsSaved({
        id: "createdOn",
        label: "Created On",
        visible: false,
      }),
      new ColumnsSaved({
        id: "createdByName",
        label: "Created By",
        visible: false,
      }),
      new ColumnsSaved({
        id: "lastUpdatedOn",
        label: "Updated On",
        visible: false,
      }),
      new ColumnsSaved({
        id: "lastUpdatedByName",
        label: "Updated By",
        visible: false,
      }),
    ];

    this.isCertificationRole = [
      { label: "Yes", value: true },
      { label: "No", value: false },
    ];
    this.userPrivileges = this.globals.getEnumPrivileges(EnumMenuItem.Roles);

    this.roleUsersPopupGrid = new GridSaved({
      columnsSaved: [
        new ColumnsSaved({
          id: "fullName",
          label: "Name",
          type: EnumColumnType.String,
          visible: true,
          styles: { width: "23rem" },
        }),
        new ColumnsSaved({
          id: "certificationFromDate",
          label: "Issue Date",
          visible: true,
          type: EnumColumnType.InputDateTime,
          styles: { width: "10rem" },
        }),
        new ColumnsSaved({
          id: "certificationToDate",
          label: "Expiration Date",
          visible: true,
          type: EnumColumnType.InputDateTime,
          styles: { width: "10rem" },
        }),
      ],
      gridClass: "formTbl",
      showMyViewsFeature: false,
      paginator: false,
      storageId: "roleUsersPopupGrid",
      version: "1.0.0",
    });

    this.roleUsersPopupModel = new ReportModel({
      name: "",
    });

    this.getAllUsers();
    this.getRolesUsers();
    this.data = [];
  }

  getRoles(event: LazyLoadEvent) {
    this.globals.showLoader(true);
    setTimeout(() => {
      this.roleService.roleGet(
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        env.apiVersion
      );
      callFunctionWithFilters(
        this.roleService,
        this.roleService.roleGet,
        event,
        this.globals.functionDic
      )
        .pipe(take(1))
        .subscribe(
          responseHandler((response) => {
            this.setAssignedUsers(response.object);
            this.showGrid = true;
            this.totalRecords = response.totalNumberOfRecords;
          })
        );
    }, 100);
  }

  setAssignedUsers(data) {
    if (this.canGetRoles) {
      this.data = data.map((x) => {
        x.assignedUsers = this.getRoleUsersByRoleId(x.id);
        return x;
      });
    } else {
      setTimeout(() => {
        this.setAssignedUsers(data);
      }, 100);
    }
  }

  getAllUsers() {
    this.userService
      .userGet(
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        env.apiVersion
      )
      .pipe(take(1))
      .subscribe(
        responseHandler(
          (response: AuditActionResultOfICollectionOfUserModel) => {
            this.users = response.object;
          }
        )
      );
  }

  getRoleUsersOptions(roleUsers: RolesUsersView[], roleId: number) {
    const roleUsersOptions = _.cloneDeep(roleUsers);
    this.users.forEach((user: UserModel) => {
      const foundUser = roleUsersOptions.find((x) => x.user.id === user.id);
      if (foundUser === undefined) {
        roleUsersOptions.push(
          new RolesUsersView({
            id: 0,
            roleId: roleId,
            user: user,
            userId: user.id,
          })
        );
      }
    });

    return this.setFullNameToParentObjAndSortIt(roleUsersOptions);
  }

  setFullNameToParentObjAndSortIt(rolesUsersViewArray) {
    rolesUsersViewArray.map((x) => {
      x.fullName = x.user.fullName;
      x.firstName = x.user.firstName;
      x.lastName = x.user.lastName;
      return x;
    });

    const ret = _.orderBy(
      rolesUsersViewArray,
      [
        (user) => user.firstName.toLowerCase(),
        (user) => user.lastName.toLowerCase(),
      ],
      ["asc", "asc"]
    );
    return ret;
  }

  getRolesUsers() {
    this.roleService
      .rolesUsers(null, env.apiVersion)
      .pipe(take(1))
      .subscribe(
        responseHandler((response) => {
          const roles = this.setFullNameToParentObjAndSortIt(response.object);
          this.rolesUsers.length = 0;
          roles.map((role) => {
            this.rolesUsers.push(role);
          });

          this.canGetRoles = true;
        })
      );
  }

  updateRoleUsers(roleId: number, roleUsers: any) {
    const filteredRoles = this.rolesUsers.filter((x) => x.roleId !== roleId);
    filteredRoles.push(...this.setFullNameToParentObjAndSortIt(roleUsers));

    this.rolesUsers.length = 0;
    filteredRoles.map((role) => {
      this.rolesUsers.push(role);
    });
    // this.rolesUsers = filteredRoles;
  }

  getRoleUsersByRoleId(roleId) {
    const assignedUsers = [];
    if (roleId === undefined) {
      return assignedUsers;
    }
    this.rolesUsers.forEach((x) => {
      if (x.roleId === roleId) {
        pushIfNotExists(x, assignedUsers, "userId");
      }
    });
    return assignedUsers;
  }

  showDialog(roleView: Role) {
    this.currentRole = this.getCurrentRole(roleView);
    this.availableRoles = this.data.filter(
      (elem) => elem.id !== this.currentRole.id
    );
    this.roleUsers = this.getRoleUsersByRoleId(this.currentRole.id);
    this.userOptions = this.getRoleUsersOptions(
      this.roleUsers,
      this.currentRole.id
    );

    this.display = true;
  }

  submitRole() {
    const ctrl = this;
    jQuery(".parsleyjs").parsley().validate();
    if (jQuery(".parsleyjs").parsley().isValid()) {
      this.globals.showLoader(true);
      let method: Observable<AuditActionResultOfRole> = null;

      let data = {
        id: this.currentRole.id,
        name: this.currentRole.name,
        isCertificationRole: this.currentRole.isCertificationRole,
        parentRoleIds: this.currentRole.parentRoles.map((role) => role.id),
        userRoles: [],
      };

      this.roleUsers.forEach((x: any) => {
        let userRoleModel = new UserRoleModel(x);
        userRoleModel.userRoleId = x.id;
        if (
          data.isCertificationRole &&
          userRoleModel.certificationFromDate !== undefined
        ) {
          userRoleModel.certificationFromDate = moment(
            userRoleModel.certificationFromDate,
            "MM/DD/YYYY"
          ).toDate();
        }
        if (
          data.isCertificationRole &&
          userRoleModel.certificationToDate !== undefined
        ) {
          userRoleModel.certificationToDate = moment(
            userRoleModel.certificationToDate,
            "MM/DD/YYYY"
          ).toDate();
        }
        data.userRoles.push(userRoleModel);
      });

      if (this.currentRole.id !== undefined) {
        let postRoleData = new UpdateRoleRequest(data);
        method = this.roleService.rolePatch(env.apiVersion, postRoleData);
      } else {
        let postRoleData = new CreateRoleRequest(data);
        method = this.roleService.rolePost(env.apiVersion, postRoleData);
      }

      method.pipe(take(1)).subscribe(
        responseHandler(
          (resp: any) => {
            if (!resp.hasErrors) {
              resp.object.parentRoles = this.currentRole.parentRoles;
              this.globals.showLoader(true);
              this.roleService
                .rolesUsers(resp.object.id, env.apiVersion)
                .pipe(take(1))
                .subscribe(
                  responseHandler(
                    (
                      userRolesResponse: AuditActionResultOfICollectionOfRolesUsersView
                    ) => {
                      resp.object.assignedUsers =
                        this.setFullNameToParentObjAndSortIt(
                          userRolesResponse.object
                        );
                      this.updateRoleUsers(
                        resp.object.id,
                        resp.object.assignedUsers
                      );
                      if (ctrl.currentRole.id === undefined) {
                        ctrl.data.push(resp.object);
                        this.data = this.data.slice(0);
                      } else {
                        const index = ctrl.data.findIndex(
                          (x) => x.id === ctrl.currentRole.id
                        );
                        ctrl.data.splice(index, 1);
                        ctrl.data.splice(index, 0, resp.object);
                        ctrl.data = ctrl.data.slice(0);
                      }
                      ctrl.clseDialog();
                    }
                  )
                );
            }
          },
          () => {}
        )
      );
    }
  }

  getCurrentRole(role) {
    if (role === undefined) {
      let ret = new Role();
      ret.parentRoles = [];
      return ret;
    } else {
      return copyObj(role);
    }
  }

  removeRow(role: Role) {
    this.globals.showLoader(true);
    this.roleService
      .roleDelete(role.id, env.apiVersion)
      .pipe(take(1))
      .subscribe(
        responseHandler(
          (resp) => {
            if (!resp.hasErrors) {
              const index = this.data.findIndex((x) => x.id === role.id);
              this.data.splice(index, 1);
              this.data = this.data.slice(0);
              this.clseDialog();
            }
          },
          () => {}
        )
      );
  }

  clseDialog() {
    this.display = false;
    jQuery(".parsleyjs").parsley().reset();
  }
}
