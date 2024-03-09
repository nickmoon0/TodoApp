'use client';
import React, { useState } from 'react'
import ErrorMessage from '@/components/errors/ErrorMessage';
import useAuth from '@/hooks/useAuth';

const RegisterForm = () => {
  const [errorMessage, setErrorMessage] = useState('');
  const [username, setUsername] = useState(''); // Only used to check if field has value
  const [password, setPassword] = useState(''); // Only used to check if field has value

  const { handleRegister } = useAuth();

  const handleSubmit = async (event) => {
    event.preventDefault();
    setErrorMessage('');

    const formData = new FormData(event.currentTarget);
    const username = formData.get('username');
    const password = formData.get('password');

    await handleRegister({ username, password }, () => {
      setErrorMessage('Failed to create account.');
    });
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