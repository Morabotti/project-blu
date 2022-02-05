import { useState, createContext, ReactNode, useContext, useCallback } from 'react';
import { LocalStorageKey } from '@enums';
import { AuthResponse, User } from '@types';
import { useNavigate } from 'react-router';
import { useQueryClient } from 'react-query';

interface AuthContext {
  loading: boolean;
  auth: null | AuthResponse;
  setAuth: (token: string, user: User, idToken?: string | null) => void;
  revokeAuth: (queries?: string) => void;
  stopLoading: () => void;
}

interface Props {
  children: ReactNode;
}

export const __AuthContext = createContext<AuthContext>({
  loading: true,
  auth: null,
  setAuth: () => {},
  revokeAuth: () => {},
  stopLoading: () => {}
});

export const AuthProvider = ({ children }: Props): JSX.Element => {
  const queryClient = useQueryClient();
  const [loading, setLoading] = useState(true);
  const [ auth, setStateAuth ] = useState<null | AuthResponse>(null);
  const navigate = useNavigate();

  const setAuth = useCallback((token: string, user: User) => {
    localStorage.setItem(LocalStorageKey.Token, token);

    setStateAuth({ token, user });
    setLoading(false);
  }, [setStateAuth, setLoading]);

  const revokeAuth = useCallback((queries?: string) => {
    queryClient.clear();
    localStorage.removeItem(LocalStorageKey.Token);
    setStateAuth(null);
    setLoading(false);
    navigate(queries ? `/login?${queries}` : '/login');
  }, [setStateAuth, navigate, setLoading, queryClient]);

  const stopLoading = useCallback(() => {
    setLoading(false);
  }, [setLoading]);

  return (
    <__AuthContext.Provider
      value={{
        loading,
        auth,
        setAuth,
        revokeAuth,
        stopLoading
      }}
    >
      {children}
    </__AuthContext.Provider>
  );
};

export const useAuth = (): AuthContext => useContext(__AuthContext);
