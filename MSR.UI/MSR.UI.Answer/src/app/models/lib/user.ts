import { Base } from './base';

export class User extends Base<User.IUser> implements User.IUser {
	accessFailedCount: number;
	createdBy: number;
	createdOn: string;
	customerId: number;
	email: string;
	firstName: string;
	id: number;
	isActive: boolean;
	isAnswerUser: boolean;
	lastName: string;
	lastUpdatedBy: number;
	lastUpdatedOn: string;
	locationId: number;
	lockoutEnabled: boolean;
	lockoutEndDateUtc: string;
	phone: string;
	securityStamp: string;
	supervisorId: number;
	timeZoneId: number;
	title: string;
	token: string;
	userName: string;
	userRoleId: string;
	constructor(dataUser?: User.IUser) {
		super();
		if (dataUser !== null) {
			Object.assign(this, dataUser);
		}
	}

	getFullName() {
		return `${this.lastName ? this.lastName : ''}`;
	}
}

export namespace User {
	export interface IUser {
		accessFailedCount: number;
		createdBy: number;
		createdOn: string;
		customerId: number;
		email: string;
		firstName: string;
		id: number;
		isActive: boolean;
		isAnswerUser: boolean;
		lastName: string;
		lastUpdatedBy: number;
		lastUpdatedOn: string;
		locationId: number;
		lockoutEnabled: boolean;
		lockoutEndDateUtc: string;
		phone: string;
		securityStamp: string;
		supervisorId: number;
		timeZoneId: number;
		title: string;
		token: string;
		userName: string;
		userRoleId: string;

		getFullName(): string;
	}

	export const PATH = '/Users';

	export enum RolUser {

	}
}
