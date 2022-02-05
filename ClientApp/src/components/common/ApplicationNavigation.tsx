import { Suspense } from 'react';
import { Outlet } from 'react-router-dom';

interface Props {
  children?: React.ReactNode;
}

export const ApplicationNavigation: React.FC<Props> = () => {
  return (
    <div>
      <div>navigation</div>
      <div>
        <Suspense fallback={<div>Inner suspense load</div>}>
          <Outlet />
        </Suspense>
      </div>
    </div>
  );
};
