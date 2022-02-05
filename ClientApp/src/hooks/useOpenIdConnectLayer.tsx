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

// URL: http://localhost:5000/auth/oidc/cb/microsoft/#code=0.ATkAAiUBragIDU-jixn1BOGuNk5NmlZKlJtKvWO3yn11oKI5AAI.AQABAAIAAAD--DLA3VO7QrddgJg7WevrX-EBG95eLOaz6Aku4fxOrbNkd6VqVIRc2X1n7jwqoZ-t2DQ-wAW4EgPbTp0Nv5eeFIcOwhxksD80snPLuwtti7UiGSktKYinVi26KxyW4m9KOeh1WE3_FPk_o08j0rn_urc6qNfzSljoqDXmlDWe_c_173Hw7ERTwfCuXnTFOfW6tai_tBxJlawcL5RwGun9nVDfxd7lMzcHjJuJkOnb4tZ1lwYliDPWdgP-il4WoJ4q1nDkt0gpS91fejgF4kBOXDCj5RkIvMGXsIWL9_3NXEjE7fChBQHoNCwbFjT-nnHju1uU1SHioVQyRzDvXtJUGdhkUT0_trmJE8P4PHUUNZSS_DflGSbPgR5EQkbx-YDSIwyrkYqdHGFcWa-vo0-Z8KE3zK34QlV-O8dduPbR1OT-rfDFbILaKt9-bM4IhEv0iVryytRoKfNeOHqkna3kAx54nDWtgx2M4H7cd9Q2hmJkNK-U5hD5BaKBZuV1X04brjvi-SpiMvQl8OY2QnMU8k0WW1TgawdUc7x5-0nPZk4Rsco6ekbwynWvIESk52Sj1v2wLQlgW_2Bzld1uOHtsIZ56ctzGihXU9z_3zhDyq9D9MlhNeLID4ZRu8NSOsZbIEe2RAtyTmbALeQzR0nk1IsbktxeJ7zJu7ePZXmmNit1ez8ntO9WhF02KEEYvb5nPWocZklibwvCnBjC3GTmZRU8gw8_eByg3m5iPsn_oY8A6o89C-it3h92omg0Sk4V4cIRwAPXPOOTvfxCAQarJ83kLr0eRpsD3DI8uvdsMowDlnMFd0dROITFDrN_am-NKdOK2l-NViMfzbK5f3gY6f4kOyQvwhhVnKskx1HuYKiP05pZ2rdi2YNoFZK3d47ddErTvDjz4iKyE95lcMXLYSLfJ6qIoCekhSMIFY3TOU0jtaBseHWXdqyyLJj-KzGkjEL6JuTphgiVkUOt9LQk3z3s54HwArdIxKjgHJdKZapZ5Wo3bOQ8RsfA37YiklXQfn_tZ1dh1G0LwRXN2EsL-9dBzmbeg_uE5vy28bYHv8-wdT-U2lWxVP8hYv_Mf7GOg8NQSC57z-Jc7BVn0KrkBQqR1n_P8b_7S7tSIj-X8ncxbUFnw-JDBXCKVpMgxld3OZVpNh9ymifcjHtvtWreZryWFnI6KyflR5M3HeTK9SAA&state=bf509a0e-054c-4ea8-b8f2-5ec9a7884c9f-4c64ac61-cfe6-4c76-a750-32706e9d829e&session_state=947a7282-0a31-4700-bb4d-4ae018592145

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
        console.log(token, user);
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
