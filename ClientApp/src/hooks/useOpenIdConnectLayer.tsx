import { useEffect, useCallback, useState } from 'react';
import { useAuth } from '@hooks';
import { AuthResponse } from '@types';
import { useNavigate, useLocation, useParams } from 'react-router';
import { loginOpenIdConnect } from '@client';

const getToken = (search: string): [string | null, string | null] => {
  const hashFormat = `?${search.substring(1)}`;
  const params = new URLSearchParams(hashFormat);

  return [
    params.get('code'),
    params.get('state')
  ];
};

interface OpenIdConnectContext {
  loading: boolean;
  auth: AuthResponse | null;
}

export const useOpenIdConnectLayer = (): OpenIdConnectContext => {
  const navigate = useNavigate();
  const { provider } = useParams<{ provider?: string }>();
  const { auth, setAuth } = useAuth();
  const { search, hash } = useLocation();
  const [ loading, setLoading ] = useState<boolean>(false);

  const onLogin = useCallback((code: string, state: string) => {
    if (!provider) {
      return;
    }

    setLoading(true);
    loginOpenIdConnect({ code, state }, provider)
      .then(({ token, user }) => {
        setAuth(token, user);
        navigate('/', { replace: true });
      })
      .catch(() => {
        navigate('/login', { replace: true });
      });
  }, [ setAuth, navigate, provider]);

  useEffect(() => {
    const [code, state] = getToken(search || hash);

    if (code && state && !loading) {
      onLogin(code, state);
    }
  }, [loading, search, onLogin, hash]);

  useEffect(() => {
    const [code, state] = getToken(search || hash);

    if (!code || !state) {
      navigate('/login', { replace: true });
    }
  }, [navigate, search, hash]);

  return {
    auth,
    loading
  };
};
