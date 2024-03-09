import { useApiContext } from '@/contexts/ApiContext';

export const useApi = () => {
  const api = useApiContext();

  return { api };
};