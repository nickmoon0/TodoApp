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
    <div className="flex items-center justify-center h-screen">
      <span className="loading loading-spinner loading-lg"></span>
    </div>
  );
}

export default RootPage;
