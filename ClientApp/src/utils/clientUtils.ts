export const checkResponse = (response: Response): Response => {
  if (!response.ok) {
    throw new Error(`${response.status.toString()}: ${response.statusText}`);
  }
  return response;
};

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export const createSearchParams = (objects: any[]): string => {
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
        query.set(key, String(object[key]));
      }
    }
  }

  return query.toString();
};
