'use client';
import { useRouter } from 'next/navigation';
import { useEffect } from 'react';
import { getToken } from '@/lib/getToken';

const RootPage = () => {  
  const router = useRouter();

  useEffect(() => {
    const token = getToken();

    if (token) {
      router.replace('/home');
    } else {
      router.replace('/login');
    }
  }, [router]);

  return (
    <div>
      {/* TODO: Create loading screen */}
      <p>Loading...</p>
    </div>
  );
}

export default RootPage;
