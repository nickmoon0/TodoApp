'use client';
import { useRouter } from 'next/navigation';
import { useEffect } from 'react';

const RootPage = () => {  
  const router = useRouter();

  useEffect(() => {
    const token = localStorage.getItem(process.env.NEXT_PUBLIC_ACCESS_TOKEN);

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
