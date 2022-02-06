import { Button, Text, Center, Link } from '@chakra-ui/react';
import styled from '@emotion/styled';

const ButtonLink = styled(Link)`
  text-decoration: none !important;
`;

interface Props {
  provider: string;
  variant?: string;
  schema?: string;
  icon?: JSX.Element;
  text?: string;
}

export const SocialMediaButton: React.FC<Props> = ({
  provider,
  icon,
  schema,
  text,
  variant
}: Props) => {
  return (
    <ButtonLink href={`/api/auth/oidc/${provider}`}>
      <Button
        w='full'
        variant={variant}
        colorScheme={schema}
        leftIcon={icon}
      >
        <Center>
          <Text>{text}</Text>
        </Center>
      </Button>
    </ButtonLink>
  );
};
