import { ChakraTheme, extendTheme } from '@chakra-ui/react';

const partialTheme: Partial<ChakraTheme> = {
  config: {
    initialColorMode: 'light',
    useSystemColorMode: true
  },
  fonts: {
    body: 'Inter, system-ui, sans-serif',
    heading: 'Work Sans, system-ui, sans-serif'
  },
  styles: {
    global: {
      'html, body, #mount': {
        height: '100%',
        width: '100%',
        margin: 0,
        backgroundColor: 'gray.100'
      }
    }
  }
};

const theme = extendTheme(partialTheme);

export default theme;
