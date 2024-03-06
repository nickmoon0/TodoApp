'use client';
import React, { createContext, useContext, useState } from 'react';

const ItemsContext = createContext();

export const useItemsContext = () => useContext(ItemsContext);

export const ItemsProvider = ({ children }) => {
  const [items, setItems] = useState([]);
  const [showError, setShowError] = useState(false);

  return (
    <ItemsContext.Provider value={{ 
      items, 
      setItems, 
      showError,
      setShowError 
    }}>
      {children}
    </ItemsContext.Provider>
  );
};
