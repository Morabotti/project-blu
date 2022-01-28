import { ChakraTheme, extendTheme } from '@chakra-ui/react';

const partialTheme: Partial<ChakraTheme> = {
  colors: {
    brand: {
      900: '#1a365d',
      800: '#153e75',
      700: '#2a69ac'
    }
  },
  config: {
    initialColorMode: 'light',
    useSystemColorMode: true
  }
};

const theme = extendTheme(partialTheme);

export default theme;
