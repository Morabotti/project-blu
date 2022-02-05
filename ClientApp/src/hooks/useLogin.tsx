import { useEffect, useCallback, useState } from 'react';
import { useAuth } from '@hooks';
import { Client, LocalStorageKey, QueryParams } from '@enums';
import { LoginForm } from '@types';
import { useNavigate, useLocation } from 'react-router';
import { getOIDCProviders, loginBasic } from '@client';
import { useQuery, UseQueryResult } from 'react-query';

interface LoginContext {
  error: boolean;
  loading: boolean;
  onSubmit: (data: LoginForm) => void;
  providers: UseQueryResult<Record<string, boolean>>;
}

export const useLogin = (): LoginContext => {
  const { search } = useLocation();
  const navigate = useNavigate();
  const { setAuth, auth } = useAuth();
  const [fetchState, setFetchState] = useState<[loading: boolean, error: boolean]>([false, false]);
  const providers = useQuery([Client.GetOIDCProviders], getOIDCProviders);

  const onSubmit = useCallback((data: LoginForm) => {
    if (data.email === '' || data.password === '') {
      return;
    }

    setFetchState([true, false]);
    loginBasic(data)
      .then(({ user, token }) => {
        localStorage.setItem(LocalStorageKey.Token, token);

        setFetchState([false, false]);
        setAuth(token, user);
      })
      .catch(() => setFetchState([false, true]));
  }, [
    setFetchState,
    setAuth
  ]);

  useEffect(() => {
    if (auth !== null) {
      if (search !== '') {
        const params = new URLSearchParams(search);
        const redirect = params.get(QueryParams.Redirect);
        const queries = params.get(QueryParams.Params);

        const path = redirect || '/';
        const queryParmas = queries
          ? `?${new URLSearchParams(queries).toString()}`
          : '';

        navigate(`${path}${queryParmas}`);
      }
      else {
        navigate('/');
      }
    }
  }, [auth, navigate, search]);

  return {
    loading: fetchState[0],
    error: fetchState[1],
    providers,
    onSubmit
  };
};
