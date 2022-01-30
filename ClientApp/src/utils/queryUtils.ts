/* eslint-disable @typescript-eslint/no-explicit-any */
import { QueryParams } from '@enums';

const getQueryParam = (search: string, key: string): string | null => {
  const params = new URLSearchParams(search);
  const param = params.get(key);
  return param;
};

export const getQueryParamsLength = (params: URLSearchParams): number => {
  let numberOfKeys = 0;
  for (const k of params.keys()) {
    if (k) {
      numberOfKeys++;
    }
  }
  return numberOfKeys;
};

export const getQueryNumberParam = (search: string, key: string): number | undefined => {
  const param = getQueryParam(search, key);

  if (param && !isNaN(Number(param))) {
    return parseInt(param);
  }

  return undefined;
};

export const getQueryStringParam = (search: string, key: string): string | undefined => {
  const param = getQueryParam(search, key);

  return param || undefined;
};

export const setQueryParam = (
  pathname: string,
  search: string,
  key: QueryParams,
  value: string | number | undefined,
  defaultValue: string | number
): string => {
  const params = new URLSearchParams(search);
  const keyParam = params.get(key);

  if (!value || defaultValue === value) {
    params.delete(key);
  }
  else if ((keyParam || defaultValue) !== value) {
    params.set(key, value.toString());
  }

  const numberOfKeys = getQueryParamsLength(params);

  const url = numberOfKeys !== 0
    ? `${pathname}?${params}`
    : pathname;

  return url;
};

export const searchParams = (objects: any[]): string => {
  const query = new URLSearchParams();

  for (const object of objects) {
    if (!object) {
      continue;
    }

    for (const key in object) {
      if (object[key] !== null
        && object[key] !== undefined
        && object[key] !== ''
        && String(object[key]).trim() !== ''
      ) {
        if (Array.isArray(object[key])) {
          for (const arrayValue of object[key]) {
            query.append(key, encodeURIComponent(arrayValue));
          }
        }
        else {
          query.set(key, String(object[key]));
        }
      }
    }
  }

  return query.toString();
};

export function stateParams<T> (object: T, search: string): string {
  const query = new URLSearchParams(search);

  for (const key in object) {
    if (object[key] !== null
      && object[key] !== undefined
      && (object as any)[key] !== ''
      && (object as any)[key] !== 0
      && String(object[key]) !== ''
    ) {
      query.set(key, encodeURIComponent(String(object[key])));
    }
    else if (query.get(key) !== null) {
      query.delete(key);
    }
  }

  return query.toString();
}
