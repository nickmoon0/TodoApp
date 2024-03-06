'use client';
import React, { createContext, useContext, useState } from 'react';

const MessagesContext = createContext();

export const useMessagesContext = () => useContext(MessagesContext);

export const MessagesProvider = ({ children }) => {
  const [errorMessage, setErrorMessage] = useState('');

  return (
    <MessagesContext.Provider value={{
      errorMessage,
      setErrorMessage
    }}>
      {children}
    </MessagesContext.Provider>
  )
}