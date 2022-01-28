import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { ApplicationProviders } from '@components/common';
import { MainView } from '@components/main';

import '../index.less';

const Application = () => (
  <ApplicationProviders>
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<MainView />} />
      </Routes>
    </BrowserRouter>
  </ApplicationProviders>
);

export default Application;
