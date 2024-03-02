const isValidToken = () => {
  const token = localStorage.getItem(process.env.NEXT_PUBLIC_ACCESS_TOKEN);
  return !!token;
}

export default isValidToken;