import { Suspense } from 'react';
import { Outlet } from 'react-router-dom';
import { AppNavBar } from '@components/common';
import { Flex } from '@chakra-ui/react';

interface Props {
  children?: React.ReactNode;
}

export const ApplicationNavigation: React.FC<Props> = () => {
  return (
    <Flex minH='100vh' flexDir='column' height='full'>
      <AppNavBar />
      <Flex as='main' height='full' flexDir='column' grow={1}>
        <Suspense fallback={<div />}>
          <Outlet />
        </Suspense>
      </Flex>
    </Flex>
  );
};
