export const handleAuth = (request, nextResponse) => {
  const { cookies } = request;
  const { pathname } = request.nextUrl;

  const authPaths = ['/login', '/register'];

  // Check that user has a refresh token
  const refreshToken = cookies.get('RefreshToken');
  if (!refreshToken && !authPaths.includes(pathname)) {
    return nextResponse.redirect(new URL('/login', request.url));
  }

  // Redirect to home page if user has tokens and is going to login/register
  if (refreshToken && authPaths.includes(pathname)) {
    return nextResponse.redirect(new URL('/home', request.url));
  }
}