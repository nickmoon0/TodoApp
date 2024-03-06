import { NextResponse } from 'next/server';
import middlewarePipeline from '@/middleware/middlewarePipeline';

export const middleware = (request) => {
  // Pass request through each middleware module
  for (var i = 0; i < middlewarePipeline.length; i++) {
    const result = middlewarePipeline[i](request, NextResponse);
    
    // Return early if module has result
    if (result) {
      return result;
    }
  }

  // Allow requests to pass through
  return NextResponse.next();
}

export const config = {
  matcher: [
    // Allows all api calls and static files to pass through middleware
    '/((?!api|_next/static|_next/image|favicon.ico).*)'
  ]
};