'use client';
import api from '@/utils/api';
import { useRouter } from 'next/navigation';
import React, { useState } from 'react'
import ErrorMessage from '../ErrorMessage';

const RegisterForm = () => {
  const router = useRouter();
  const [errorMessage, setErrorMessage] = useState('');
  const [username, setUsername] = useState(''); // Only used to check if field has value
  const [password, setPassword] = useState(''); // Only used to check if field has value

  const handleSubmit = async (event) => {
    event.preventDefault();
    setErrorMessage('');

    const formData = new FormData(event.currentTarget);
    const username = formData.get('username');
    const password = formData.get('password');

    try {
      const response = await api.post("/user/create", { username, password });

      const token = response.data.accessToken;
      localStorage.setItem(process.env.NEXT_PUBLIC_ACCESS_TOKEN, token);
      
      router.push('/home');
    } catch (error) {
      setErrorMessage('Failed to create account.');
    }
  }

  return (
    <form onSubmit={handleSubmit} className="flex flex-col space-y-2 px-4 pb-4">
      {errorMessage && <ErrorMessage message={errorMessage}/>}
      <input type="text" name="username" placeholder="Username" onChange={(event) => setUsername(event.target.value)} className="input input-bordered input-primary w-full max-w-xs" />
      <input type="password" name="password" placeholder="Password" onChange={(event) => setPassword(event.target.value)} className="input input-bordered input-primary w-full max-w-xs" />
      <button type="submit" disabled={!username || !password} className="btn btn-primary w-full max-w-xs">Create Account</button>
    </form>
  )
}

export default RegisterForm