export const staticFilePath = (fileName?: string | null) => {
  if (!fileName) {
    return undefined;
  }

  return `/api/content/${fileName}`;
};
