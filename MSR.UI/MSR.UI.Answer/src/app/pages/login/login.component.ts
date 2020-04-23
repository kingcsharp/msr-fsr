import { Component, HostBinding } from '@angular/core';
import { LoginService } from './login.service';
import { ActivatedRoute } from '@angular/router';
import { AppConfig } from '../../app.config';

@Component({
  selector: 'login',
  templateUrl: './login.template.html',
})
export class Login {
  @HostBinding('class') classes = 'auth-page app';

  email: string = '';
  password: string = '';
  username: string = '';
  forgotUsername: boolean = false;
  showLogin: boolean = true;
  forgotPassword: boolean = false;

  constructor(public loginService: LoginService, private route: ActivatedRoute, appConfig: AppConfig) {
    const config: any = appConfig.getConfig();
    // const creds = config.auth;
    // this.email = creds.email;
    // this.password = creds.password;
    if (this.loginService.isAuthenticated()) {
      this.loginService.receiveLogin();
    }

    this.route.queryParams.subscribe((params) => {
      if (params.token) {
        this.loginService.receiveToken(params.token);
      }
    });
  }

  public async forgotUserPassword() {
    if (this.username.length <= 0) {
      this.loginService.loginError('Please fill Username Field.');
    }

    var sentEmail = await this.loginService.forgotUserPassword(this.username);
    this.forgotPassword = !sentEmail;
    this.showLogin = sentEmail;
  }

  public async forgotUserName() {
    if (this.email.length <= 0) {
      this.loginService.loginError('Please fill Username Field.');
    }

    var sentEmail = await this.loginService.forgotUserName(this.email);
    this.forgotUsername = !sentEmail;
    this.showLogin = sentEmail;
  }

  public showLoginDiv() {
    this.forgotUsername = false;
    this.showLogin = true;
    this.forgotPassword = false;
  }

  public fogotUserName() {
    this.forgotUsername = true;
    this.showLogin = false;
    this.forgotPassword = false;
  }

  public fogotPassword() {
    this.forgotUsername = false;
    this.showLogin = false;
    this.forgotPassword = true;
  }

  public login() {
    const { email, password } = this;

    if (email.length !== 0 && password.length !== 0) {
      this.loginService.loginUser({ email, password });
    }
  }

  // public googleLogin() {
  //   this.loginService.loginUser({ social: 'google' });
  // }

  // public microsoftLogin() {
  //   this.loginService.loginUser({ social: 'microsoft' });
  // }
}
