import { roles } from "./roles";

export interface User {
    userName?: string;
    roleName?: string;
    token?: string;
    firstName?: string;
    lastName?: string;
    surName?: string;
    password?: string;
    roleId?: number;
    roles?: roles
    isActive?: boolean
}