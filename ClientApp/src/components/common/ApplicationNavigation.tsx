import { Suspense } from 'react';
import { Outlet } from 'react-router-dom';
import { AppNavBar } from '@components/common';
import { Box, Center, Flex } from '@chakra-ui/react';

interface Props {
  children?: React.ReactNode;
}

export const ApplicationNavigation: React.FC<Props> = () => {
  return (
    <Flex minH='100vh' flexDir='column' height='full'>
      <AppNavBar />
      <Flex mt='64px' pos='relative' height='100%' flexDir='column' grow={1} w='100%'>
        <Box
          as='nav'
          w={250}
          h='100%'
          position='fixed'
          left={0}
          top='64px'
          bg='white'
          borderRight={1}
          borderStyle='solid'
          borderColor='gray.200'
        >
          <Center>
            Navigation
          </Center>
        </Box>
        <Box as='main' ml={250} h='100%'>
          <Suspense fallback={<div />}>
            <Outlet />
          </Suspense>
        </Box>
      </Flex>
    </Flex>
  );
};
