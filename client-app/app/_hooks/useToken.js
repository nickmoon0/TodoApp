import { useTokenContext } from '@/contexts/TokenContext';

export const useToken = () => {
  const { token, setToken } = useTokenContext();

  const deleteToken = () => setToken('');

  return {
    token, setToken, deleteToken
  }
};