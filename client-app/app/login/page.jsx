import React from 'react'
import LoginForm from '@/components/forms/LoginForm'
import Link from 'next/link'

const LoginPage = () => {
  return (
    <div className="min-h-screen flex items-center justify-center">
      <div className="max-w-xs w-full mx-auto bg-base-100 text-base-content border border-gray-300 rounded-lg">
        <h1 className="text-xl text-center p-2">Login</h1>
        <LoginForm />
        <div className="text-center px-4 pb-4">
          <Link href="/register" className="link link-primary">Create Account</Link>
        </div>
      </div>
    </div>
  )
}

export default LoginPage