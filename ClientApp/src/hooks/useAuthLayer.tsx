import { useEffect, useCallback, useMemo } from 'react';
import { AuthResponse } from '@types';
import { useAuth } from '@hooks';
import { useLocation } from 'react-router-dom';
import { LocalStorageKey, QueryParams } from '@enums';
import { getMe } from '@client';

interface AuthContext {
  loading: boolean;
  auth: null | AuthResponse;
  queries: string;
}

export const useAuthLayer = (): AuthContext => {
  const { loading, auth, stopLoading, setAuth, revokeAuth } = useAuth();
  const { pathname, search } = useLocation();

  const queries = useMemo(() => {
    const params = new URLSearchParams(search).toString();
    const newParams = new URLSearchParams();

    if (pathname !== '/') {
      newParams.set(QueryParams.Redirect, pathname);
    }

    if (params !== '') {
      newParams.set(QueryParams.Params, params);
    }

    return newParams.toString();
  }, [search, pathname]);

  const getStatus = useCallback(() => {
    const token = localStorage.getItem(LocalStorageKey.Token);

    if (auth === null && loading) {
      if (token) {
        getMe()
          .then(response => setAuth(token, response))
          .catch(() => revokeAuth());
      }
      else {
        stopLoading();
      }
    }
  }, [loading, auth, stopLoading, revokeAuth, setAuth]);

  useEffect(() => {
    getStatus();
  }, [getStatus]);

  return {
    loading,
    auth,
    queries
  };
};
