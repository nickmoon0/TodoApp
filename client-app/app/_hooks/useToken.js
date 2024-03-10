export const useToken = () => {
  const tokenName = process.env.NEXT_PUBLIC_ACCESS_TOKEN;

  const getToken = () => localStorage.getItem(tokenName);
  const setToken = (token) => localStorage.setItem(tokenName, token);
  const deleteToken = () => localStorage.removeItem(tokenName);

  return {
    getToken, setToken, deleteToken
  };
};
