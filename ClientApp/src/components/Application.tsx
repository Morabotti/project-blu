import { Route, Routes } from 'react-router-dom';
import { ApplicationProviders, ApplicationNavigation } from '@components/common';
import { MainView } from '@components/main';
import { ApplicationAuthLayer, LoginView, OpenIdCallbackView } from '@components/auth';
import { Suspense } from 'react';

import '../index.less';

const Application = () => (
  <ApplicationProviders>
    <Suspense fallback={<div>Upper suspense load</div>}>
      <Routes>
        <Route path='/' element={
          <ApplicationAuthLayer>
            <ApplicationNavigation />
          </ApplicationAuthLayer>
        }>
          <Route index element={<div>home</div>} />
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
