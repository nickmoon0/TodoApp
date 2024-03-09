export const handleRootRoute = (request, nextResponse) => {
  const { pathname } = request.nextUrl;
  
    // Redirect to home page
    if (pathname == '/') {
      return nextResponse.redirect(new URL('/home', request.url));
    }
}