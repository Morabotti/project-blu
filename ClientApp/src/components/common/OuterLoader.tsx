import { Flex, Spinner, Text } from '@chakra-ui/react';

interface Props {
  text?: string;
}

export const OuterLoader: React.FC<Props> = ({
  text = 'Loading...'
}: Props) => {
  return (
    <Flex minH='100vh' align='center' justify='center' direction='column'>
      <Spinner
        thickness='4px'
        speed='0.65s'
        emptyColor='gray.300'
        color='blue.500'
        size='xl'
        mb={4}
      />
      <Text fontSize='md' color='gray.600' align='center'>
        {text}
      </Text>
    </Flex>
  );
};
