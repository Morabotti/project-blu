import { LocalStorageKey } from '@enums';
import { checkResponse } from '@utils/clientUtils';

export const getStatus = (): Promise<string> => fetch(
  `/api/status`,
  {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem(LocalStorageKey.Token)}`
    }
  }
)
  .then(res => checkResponse(res))
  .then(res => res.json());
