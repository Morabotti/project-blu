import { query } from '@utils/clientUtils';

import {
  AuthResponse,
  BasicLoginRequest,
  CreateUser,
  User
} from '@types';

export const getMe = () => query<User>(`/api/auth/me`);
export const loginBasic = (login: BasicLoginRequest) => query<AuthResponse>(`/api/auth/login`, 'POST', login);

export const getSetup = () => query<boolean>(`/api/user/setup`);
export const createSetup = (user: CreateUser) => query<User>(`/api/user/setup`, 'POST', user);
