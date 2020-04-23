import { Component, OnInit, HostBinding } from '@angular/core';
import { ResetpasswordService } from './resetpassword.service';
import { ActivatedRoute } from '@angular/router';
import { AppConfig } from '../../app.config';

@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.scss']
})
export class ResetpasswordComponent implements OnInit {
  @HostBinding('class') classes = 'auth-page app';

  token: string = '';
  password: string = '';
  confirmPassword: string = '';
  constructor(public resetPasswordService: ResetpasswordService, private route: ActivatedRoute, appConfig: AppConfig) { }
  ngOnInit(): void {
    this.token = this.route.snapshot.paramMap.get('token');
  }

  public async updatePassword() {
    const { token, password } = this;

    if (token.length !== 0 && password.length !== 0) {
      await this.resetPasswordService.resetPassword({ token, password });
    }
  }

  public login() {
    this.resetPasswordService.logoutUser();
  }
}
