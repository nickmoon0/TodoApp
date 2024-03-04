'use client';
import React, { createContext, useContext, useRef } from 'react';

const ElementRefsContext = createContext();

export const useElementRefsContext = () => useContext(ElementRefsContext);

export const ElementRefsProvider = ({ children }) => {
  const newItemFormRef = useRef(null);
  const createItemModalRef = useRef(null);

  return (
    <ElementRefsContext.Provider value={{
      newItemFormRef,
      createItemModalRef
    }}>
      {children}
    </ElementRefsContext.Provider>
  )
};