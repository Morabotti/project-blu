import { query } from '@utils/clientUtils';

import {
  AuthResponse,
  BasicLoginRequest,
  CreateUser,
  OIDCLoginRequest,
  User
} from '@types';

export const getMe = () => query<User>(`/api/auth/me`);
export const getOIDCProviders = () => query<Record<string, boolean>>(`/api/auth/oidc`);
export const loginBasic = (login: BasicLoginRequest) => query<AuthResponse>(`/api/auth/login`, 'POST', login);
export const loginOpenIdConnect = (login: OIDCLoginRequest, provider: string) => query<AuthResponse>(`/api/auth/oidc/${provider}`, 'POST', login);

export const getSetup = () => query<boolean>(`/api/user/setup`);
export const createSetup = (user: CreateUser) => query<User>(`/api/user/setup`, 'POST', user);
