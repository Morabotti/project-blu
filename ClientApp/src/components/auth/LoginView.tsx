import { Flex, Stack, Heading, Box, FormControl, FormLabel, Input, Checkbox, Button, Link, Text, FormErrorMessage, Center } from '@chakra-ui/react';
import { FcGoogle } from 'react-icons/fc';
import { FaMicrosoft } from 'react-icons/fa';
import { useLogin } from '@hooks';
import { LoginForm } from '@types';
import { useForm } from 'react-hook-form';
import styled from '@emotion/styled';

const ButtonLink = styled(Link)`
  text-decoration: none !important;
`;

const getProviderOptions = (provider: string): [string?, string?, string?, JSX.Element?] => {
  switch (provider) {
    case 'google': return ['outline', undefined, 'Sign in with Google', <FcGoogle />];
    case 'microsoft': return ['solid', 'blue', 'Sign in with Microsoft', <FaMicrosoft />];
    default: return [undefined, undefined, undefined, undefined];
  }
};

export const LoginView: React.FC = () => {
  const { loading, onSubmit, providers } = useLogin();

  const { register, handleSubmit, formState: { errors } } = useForm<LoginForm>({
    defaultValues: {
      email: '',
      password: '',
      remember: true
    }
  });

  const activeProviders = Object.entries(providers.data ?? {}).filter(e => e[1]);

  return (
    <Flex
      minH='100vh'
      align='center'
      justify='center'
      bg='gray.100'
    >
      <Stack spacing={8} mx='auto' maxW='lg' py={12} px={6} width='full'>
        <Stack align='center'>
          <Heading fontSize='4xl'>Project-Blu</Heading>
          <Text fontSize='lg' color='gray.600'>
            Self hosted & managed <Link color='blue.400'>project tracking software</Link>
          </Text>
        </Stack>
        <Box
          rounded='lg'
          bg='white'
          boxShadow='lg'
          p={8}
        >
          <form onSubmit={handleSubmit(onSubmit)}>
            <Stack spacing={4}>
              <FormControl id='form-email' isInvalid={!!errors.email}>
                <FormLabel>Email address</FormLabel>
                <Input {...register('email')} type='email' autoComplete='username' />
                <FormErrorMessage>
                  {errors.email && errors.email.message}
                </FormErrorMessage>
              </FormControl>
              <FormControl id='form-password' isInvalid={!!errors.password}>
                <FormLabel>Password</FormLabel>
                <Input
                  {...register('password')}
                  type='password'
                  autoComplete='current-password'
                />
                <FormErrorMessage>
                  {errors.password && errors.password.message}
                </FormErrorMessage>
              </FormControl>
              <Stack spacing={10}>
                <Stack
                  direction={{ base: 'column', sm: 'row' }}
                  align='start'
                  justify='space-between'
                >
                  <Checkbox {...register('remember')}>Remember me</Checkbox>
                  <Link color='blue.400'>Forgot password?</Link>
                </Stack>
                <Button
                  bg='blue.400'
                  color='white'
                  type='submit'
                  isLoading={loading}
                  loadingText='Authenticating'
                  _hover={{
                    bg: 'blue.500'
                  }}
                >
                  Sign in
                </Button>
              </Stack>
            </Stack>
          </form>
          {activeProviders.length !== 0 && (
            <>
              <Box my={6}>
                <Text fontSize='md' color='gray.600' align='center'>
                  Or use external provider
                </Text>
              </Box>
              <Stack spacing={4}>
                {activeProviders.map(provider => {
                  const [variant, schema, text, icon] = getProviderOptions(provider[0]);
                  return (
                    <ButtonLink href={`/api/auth/oidc/${provider[0]}`} key={provider[0]}>
                      <Button
                        w='full'
                        maxW='md'
                        variant={variant}
                        colorScheme={schema}
                        leftIcon={icon}>
                        <Center>
                          <Text>{text}</Text>
                        </Center>
                      </Button>
                    </ButtonLink>
                  );
                })}
              </Stack>
            </>
          )}
        </Box>
      </Stack>
    </Flex>
  );
};
