import { Avatar, Box, Button, Flex, Heading, Stack } from '@chakra-ui/react';
import { useAuth } from '@hooks';
import { staticFilePath } from '@utils/staticFileUtil';

interface Props {
  children?: React.ReactNode;
}

export const AppNavBar: React.FC<Props> = () => {
  const { revokeAuth, auth } = useAuth();

  return (
    <Box>
      <Flex
        bg='white'
        color='grey.600'
        minH='64px'
        py={{ base: 2 }}
        px={{ base: 6 }}
        borderBottom={1}
        borderStyle='solid'
        borderColor='gray.200'
        align='center'
      >
        <Flex flex={{ base: 1 }} justify='start'>
          <Heading
            textAlign='left'
            color='gray.800'
            size='xl'
          >
            LOGO
          </Heading>
          <Flex display={{ base: 'none', md: 'flex' }} ml={10} />
        </Flex>
        <Stack
          flex={{ base: 1, md: 0 }}
          justify='flex-end'
          direction='row'
          align='center'
          spacing={6}
        >
          <Button
            display={{ base: 'none', md: 'inline-flex' }}
            fontSize='sm'
            fontWeight={600}
            colorScheme='facebook'
            onClick={() => revokeAuth()}
          >
            Logout
          </Button>
          <Avatar
            bg='facebook.500'
            size='md'
            src={staticFilePath(auth?.user.image?.source)}
          />
        </Stack>
      </Flex>
    </Box>
  );
};
