import { Injectable } from '@angular/core';
import axios from 'axios';

@Injectable({
    providedIn: 'root',
})
export class IpService {
    constructor() { }

    async getIpAnotherAddress(): Promise<string> {
        try {
            const response = await axios.get('https://api.ipify.org?format=json');
            return response.data.ip;
        } catch (error) {
            console.error('Error getting IP address:', error.message);
            return ''; // Return an empty string in case of error
        }
    }
}
