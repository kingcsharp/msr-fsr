import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
//44398
//const hostApi = environment.production ? 'https://dev-api.answer.msr-fsr.com' : 'http://localhost';
const hostApi = environment.url;
const portApi = environment.production ? 443 : 443;

const baseURLApi = `${environment.url}${portApi ? `:${portApi}` : ``}/${environment.version}`;

@Injectable()
export class AppConfig {
  config = {
    remote: 'https://flatlogic-node-backend.herokuapp.com',
    isBackend: environment.backend,
    hostApi,
    portApi,
    baseURLApi,
    auth: {
      email: 'admin@flatlogic.com',
      password: 'password'
    },
    settings: {
      colors: {
        'white': '#fff',
        'black': '#000',
        'gray-light': '#999',
        'gray-lighter': '#eee',
        'gray': '#666',
        'gray-dark': '#343434',
        'gray-darker': '#222',
        'gray-semi-light': '#777',
        'gray-semi-lighter': '#ddd',
        'brand-primary': '#5d8fc2',
        'brand-success': '#64bd63',
        'brand-warning': '#f0b518',
        'brand-danger': '#dd5826',
        'brand-info': '#5dc4bf'
      },
    },
  };

  getConfig(): Object {
    return this.config;
  }
}

