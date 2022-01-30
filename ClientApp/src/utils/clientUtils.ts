import { LocalStorageKey } from '@enums';
import { HttpMethod } from '@types';

export const checkResponse = (response: Response): Response => {
  if (!response.ok) {
    throw new Error(`${response.status.toString()}: ${response.statusText}`);
  }
  return response;
};

export const query = <T>(url: string, method?: HttpMethod, body?: unknown): Promise<T> => fetch(
  url,
  {
    method: method ?? 'GET',
    body: body ? JSON.stringify(body) : undefined,
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem(LocalStorageKey.Token)}`
    }
  }
)
  .then(res => checkResponse(res))
  .then(res => res.json());
