'use client';
import React, { createContext, useContext, useState } from 'react';

const TokenContext = createContext();

export const useTokenContext = () => useContext(TokenContext);

export const TokenProvider = ({ children }) => {
  const [token, setToken] = useState('');

  return (
    <TokenContext.Provider value={{
      token,
      setToken
    }}>
      {children}
    </TokenContext.Provider>
  );
};