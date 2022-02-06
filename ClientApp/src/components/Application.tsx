import { Route, Routes } from 'react-router-dom';
import { ApplicationProviders, ApplicationNavigation, OuterLoader } from '@components/common';
import { MainView } from '@components/main';
import { ApplicationAuthLayer, LoginView, OpenIdCallbackView } from '@components/auth';
import { Suspense } from 'react';

const Application = () => (
  <ApplicationProviders>
    <Suspense fallback={<OuterLoader text='Loading assets...' />}>
      <Routes>
        <Route path='/' element={
          <ApplicationAuthLayer>
            <ApplicationNavigation />
          </ApplicationAuthLayer>
        }>
          <Route index element={<div style={{ height: '100%' }}>home</div>} />
          <Route path='issues' element={<div>issues</div>} />
        </Route>
        <Route path='/login' element={<LoginView />} />
        <Route path='/main' element={<MainView />} />
        <Route path='/auth/oidc/cb/:provider' element={<OpenIdCallbackView />} />
        <Route path='*' element={<div>not found</div>} />
      </Routes>
    </Suspense>
  </ApplicationProviders>
);

export default Application;
