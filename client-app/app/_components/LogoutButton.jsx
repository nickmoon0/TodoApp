'use client';
import React from 'react'
import useAuth from '@/hooks/useAuth';

const LogoutButton = () => {
  const { handleLogout } = useAuth();

  return (
    <button onClick={handleLogout} className="btn btn-neutral">Logout</button>
  );
}

export default LogoutButton