import { useOpenIdConnectLayer } from '@hooks';

export const OpenIdCallbackView: React.FC = () => {
  const { loading } = useOpenIdConnectLayer();

  if (loading) {
    return (
      <div>
        Loading auth
      </div>
    );
  }

  return (
    <div>
      Loading tokens
    </div>
  );
};
