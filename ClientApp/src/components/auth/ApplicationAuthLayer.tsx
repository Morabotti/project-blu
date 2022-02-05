import { memo } from 'react';
import { Navigate } from 'react-router-dom';
import { useAuthLayer } from '@hooks';

interface Props {
  children?: React.ReactNode;
}

export const ApplicationAuthLayer: React.FC<Props> = memo(({ children }: Props) => {
  const { loading, auth, queries } = useAuthLayer();

  if (loading && auth === null) {
    return (
      <p>Loading</p>
    );
  }

  if (!loading && auth === null) {
    return (
      <Navigate replace to={queries !== '' ? `/login?${queries}` : '/login'} />
    );
  }

  if (auth === null) {
    return (
      <p>Error</p>
    );
  }

  return (
    <>
      {children}
    </>
  );
});
