import { AuthProvider, UserRole } from '@enums';
import { FC, LazyExoticComponent } from 'react';

export type ViewComponentProps = LazyExoticComponent<FC>;
export type RouteComponent = ViewComponentProps | FC;
export type HttpMethod = 'GET' | 'POST' | 'PUT' | 'DELETE' | 'OPTION';

export interface Author {
  id: number;
  firstName: string;
  lastName: string;
}

export interface OwnedLocation {
  address: string;
  city: string;
  zip: string;
  state: string;
  country: string;
}

export interface CreateUser {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  location: OwnedLocation;
  role: UserRole;
}

export interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  location: OwnedLocation;
  role: UserRole;
  provider: AuthProvider | null;
  createdAt: string;
}

export interface BasicLoginRequest {
  email: string;
  password: string;
}

export interface LoginForm extends BasicLoginRequest {
  remember: boolean;
}

export interface AuthResponse {
  token: string;
  user: User;
}

export interface OIDCLoginRequest {
  code: string;
  state: string;
}
