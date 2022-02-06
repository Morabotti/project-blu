import { OuterLoader } from '@components/common';
import { useOpenIdConnectLayer } from '@hooks';

export const OpenIdCallbackView: React.FC = () => {
  const { loading } = useOpenIdConnectLayer();

  if (loading) {
    return (
      <OuterLoader text='Authenticating...' />
    );
  }

  return (
    <OuterLoader text='Loading tokens...' />
  );
};
