import RegisterForm from '@/components/RegisterForm'
import React from 'react'
import Link from 'next/link'

const CreateAccount = () => {
  return (
    <div>
      <div className="min-h-screen flex items-center justify-center">
      <div className="max-w-xs w-full mx-auto bg-base-100 text-base-content border border-gray-300 rounded-lg">
        <h1 className="text-xl text-center p-2">Create Account</h1>
        <RegisterForm />
        <div className="text-center px-4 pb-4">
          <Link href="/login" className="link link-primary">Login</Link>
        </div>
      </div>
    </div>
    </div>
  )
}

export default CreateAccount