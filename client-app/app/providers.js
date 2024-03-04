'use client';

import { ItemsProvider } from "@/contexts/ItemsContext";

export function Providers({ children }) {
  return (
    <ItemsProvider>
      {children}
    </ItemsProvider>
  )
}