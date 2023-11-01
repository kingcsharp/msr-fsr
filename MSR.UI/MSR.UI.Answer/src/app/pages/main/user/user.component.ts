import {
  Component,
  OnInit,
  ViewEncapsulation,
  ElementRef,
} from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { Globals } from "../../../models/lib/globals";
import {
  UserService,
  UserModel,
  IAuditActionResultOfUserModel,
  LocationService,
  UpdateUserRequest,
  RoleService,
  Role,
  EnumMenuItem,
  CustomerService,
  CustomerModel,
} from "../../../services/api.client.generated";
import { take } from "rxjs/operators";
import { environment as env } from "../../../../environments/environment";
import { responseHandler } from "../../../utils/responseHandler";
import { Observable } from "rxjs";
import { ViewSaved } from "../../../models/lib/ViewSaved";
import { ColumnsSaved } from "../../../models/lib/ColumnsSaved";
import { CommonGrid } from "../../../models/lib/CommonGrid";
import { copyObj } from "../../../models/lib/Utils";
import { AllowedActions } from "../../../models/lib/AllowedActions";
import { callFunctionWithFilters } from "../../../models/lib/Utils";
import { LazyLoadEvent } from "primeng/api";

declare let jQuery: any;

@Component({
  selector: "user",
  templateUrl: "./user.template.html",
  styleUrls: ["./user.style.scss"],
  encapsulation: ViewEncapsulation.None,
  preserveWhitespaces: true,
})
export class UserComponent implements OnInit {
  userPrivileges: AllowedActions;
  menuItems = EnumMenuItem;
  data: any;
  display: boolean = false;
  currUser: any;
  phoneValue = "";
  statuses: any[];
  roles: any[];
  allRoles: any[] = [];
  allUsers: any[];
  userTypes: any[];
  showSaveView: boolean = false;
  savedViewsOptions: any;
  viewsSaved: Array<ViewSaved>;
  viewToSave: ViewSaved;
  controllerName: string;
  gridVersion: string; // IF YOU ADD COLUMNS OR EDIT DATA TYPE YOU NEED TO UPGRADE THIS VERSION
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  selectedColumns: any;
  columnPicker: any;
  columnDropdown: boolean = false;
  gridOptionsRotate: boolean = false;
  locations: any[] = [];
  getLocationsFlag: boolean = false;
  customers: Array<CustomerModel>;
  totalRecords: number = 0;

  constructor(
    public userService: UserService,
    public cg: CommonGrid,
    private toastr: ToastrService,
    private customerService: CustomerService,
    public globals: Globals,
    private elem: ElementRef,
    public locationService: LocationService,
    public roleService: RoleService
  ) {}

  ngOnInit(): void {
    this.data = [];
    this.currUser = new UserModel();
    this.gridVersion = "1.0.2";
    this.gridStorageId =
      "userGrid" + this.elem.nativeElement.tagName.toLowerCase();
    // SET DEFAULT VIEW COLS
    this.gridSettings = [
      new ColumnsSaved({ id: "id", label: "Id", visible: true }),
      new ColumnsSaved({ id: "isActive", label: "Active", visible: true }),
      new ColumnsSaved({
        id: "isAnswerUser",
        label: "User Type",
        visible: true,
      }),
      new ColumnsSaved({ id: "firstName", label: "First Name", visible: true }),
      new ColumnsSaved({ id: "lastName", label: "Last Name", visible: true }),
      new ColumnsSaved({ id: "userName", label: "Username", visible: true }),
      new ColumnsSaved({ id: "email", label: "Email", visible: true }),
      new ColumnsSaved({
        id: "createdOn",
        label: "Created On",
        visible: false,
      }),
      new ColumnsSaved({ id: "roles", label: "Roles", visible: false }),
      new ColumnsSaved({
        id: "locationName",
        label: "Location",
        visible: false,
      }),
      new ColumnsSaved({
        id: "supervisorName",
        label: "Supervisor",
        visible: false,
      }),
    ];

    this.roles = [];
    this.allUsers = [];

    this.statuses = [
      { label: "Active", value: true },
      { label: "InActive", value: false },
    ];
    this.userTypes = [
      { label: "Portal User", value: false },
      { label: "Answer User", value: true },
    ];

    this.userPrivileges = this.globals.getEnumPrivileges(this.menuItems.Users);
    this.getLocations();
    this.getRoles();
    this.getCustomers();
  }

  getCustomers() {
    this.customerService
      .customerGet(
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        true,
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
        responseHandler((response) => {
          this.customers = response.object;
        })
      );
  }

  getRoles() {
    this.roleService
      .roleGet(
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
        responseHandler((response) => {
          this.allRoles = response.object;
        })
      );
  }

  getLocations() {
    if (this.getLocationsFlag) {
      return this.locations;
    }
    this.locationService
      .locationGet(
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
        null,
        null,
        null,
        env.apiVersion
      )
      .pipe(take(1))
      .subscribe(
        responseHandler((response) => {
          response.object.map((x) => {
            if (x.parentId === null) {
              this.locations.push({ label: x.name, value: x.id });
            }
          });
          this.getLocationsFlag = true;
        })
      );
  }

  async getUsers(event: LazyLoadEvent) {
    this.globals.showLoader(true);
    setTimeout(() => {
      callFunctionWithFilters(
        this.userService,
        this.userService.userGet,
        event,
        this.globals.functionDic
      )
        .pipe(take(1))
        .subscribe(
          responseHandler((response) => {
            this.totalRecords = response.totalNumberOfRecords;
            this.data = response.object;
          })
        );
    }, 10);

    if (this.allUsers.length === 0) {
      setTimeout(() => {
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
            responseHandler((response) => {
              this.updateUsersData(response.object);
            })
          );
      }, 10);
    }
  }

  updateUsersData(usersData) {
    usersData.forEach((x) => {
      const userIndex = this.allUsers.findIndex((z) => z.value === x.id);
      if (userIndex < 0) {
        this.allUsers.push({
          label: x.firstName + " " + x.lastName,
          value: x.id,
        });
      }
    });
    this.allUsers.sort((a, b) => (a.label > b.label ? 1 : -1));
  }

  unmask(event) {
    return event.replace(/\D+/g, "");
  }

  showDialog(user: UserModel) {
    this.display = true;
    this.currUser = this.getUser(user);
  }

  closeDialog() {
    this.display = false;
    jQuery(".parsleyjs").parsley().reset();
  }

  changeUserStatus(user: UserModel) {
    this.userService
      .userDelete(user.id, env.apiVersion)
      .pipe(take(1))
      .subscribe(
        responseHandler(
          () => {
            this.toastr.success(
              `User has been successfully ${
                user.isActive ? "activated" : "deactivated"
              }!`
            );
          },
          () => {
            user.isActive = !user.isActive;
          }
        )
      );
  }

  changeUserType(user: UpdateUserRequest) {
    this.userService
      .userPatch(env.apiVersion, user)
      .pipe(take(1))
      .subscribe(
        responseHandler(
          (resp) => {
            if (!resp.hasErrors) {
              if (user.isAnswerUser) {
                this.toastr.success("User type changed to Is Answer User!");
              } else {
                this.toastr.success("User type changed to Portal User!");
              }
            }
          },
          () => {
            user.isAnswerUser = !user.isAnswerUser;
          }
        )
      );
  }

  onUserSubmit() {
    jQuery(".parsleyjs").parsley().validate();
    if (jQuery(".parsleyjs").parsley().isValid()) {
      let method: Observable<IAuditActionResultOfUserModel> = null;
      this.globals.showLoader(true);
      if (this.currUser.customer !== undefined) {
        this.currUser.customerId = this.currUser.customer.id;
      }
      if (this.currUser.isAnswerUser) {
        this.currUser.customerId = null;
      }
      if (this.currUser.id === undefined) {
        method = this.userService.userPost(env.apiVersion, this.currUser);
      } else {
        let updateUserReq = new UpdateUserRequest();
        Object.assign(updateUserReq, this.currUser);
        if (this.currUser.timeZone !== undefined) {
          updateUserReq.timeZoneId = this.currUser.timeZone.id;
        }
        method = this.userService.userPatch(env.apiVersion, updateUserReq);
      }

      method.pipe(take(1)).subscribe(
        responseHandler((resp) => {
          if (!resp.hasErrors) {
            if (this.currUser.id !== undefined) {
              const index = this.data.findIndex((x) => x.id === resp.object.id);
              this.data.splice(index, 1);
            }

            this.data.push(new UserModel(resp.object));
            this.data = this.data.slice(0);
            this.updateUsersData(this.data);
            this.closeDialog();
          }
        })
      );
    }
  }

  getUser(user: UserModel) {
    if (user === undefined) {
      let ret = new UserModel();
      ret.isActive = true;
      ret.isAnswerUser = true;
      ret.firstName = "";
      ret.roles = [];
      return ret;
    } else {
      this.setCustomer(user);
      return copyObj(user);
    }
  }

  setCustomer(user: any) {
    if (user.customerId !== undefined) {
      const customerIndex = this.customers.findIndex(
        (z) => z.id === user.customerId
      );
      if (customerIndex !== -1) {
        user.customer = this.customers[customerIndex];
      }
    }
  }
}
