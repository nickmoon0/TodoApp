'use client';

import { ItemsProvider } from "@/contexts/ItemsContext";
import { ElementRefsProvider } from "@/contexts/ElementRefsContext";

export function Providers({ children }) {
  return (
    <ElementRefsProvider>
      <ItemsProvider>
        {children}
      </ItemsProvider>
    </ElementRefsProvider>
  )
}