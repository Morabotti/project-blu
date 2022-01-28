import { useState } from 'react';

interface BasicHookContext {
  data: string;
  setData: React.Dispatch<React.SetStateAction<string>>;
}

export const useBasicHook = (): BasicHookContext => {
  const [data, setData] = useState<string>('');

  return {
    data,
    setData
  };
};
