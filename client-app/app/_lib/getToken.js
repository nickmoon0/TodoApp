export const getToken = () => {
  return localStorage.getItem(process.env.NEXT_PUBLIC_ACCESS_TOKEN);
};

export const setToken = (token) => {
  localStorage.setItem(process.env.NEXT_PUBLIC_ACCESS_TOKEN, token);
}

export const deleteToken = () => {
  localStorage.removeItem(process.env.NEXT_PUBLIC_ACCESS_TOKEN);
}