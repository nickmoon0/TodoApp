'use client';

import { ItemsProvider } from "@/contexts/ItemsContext";
import { ElementRefsProvider } from "@/contexts/ElementRefsContext";
import { MessagesProvider } from "@/contexts/MessagesContext";

export function Providers({ children }) {
  return (
    <MessagesProvider>
      <ElementRefsProvider>
        <ItemsProvider>
          {children}
        </ItemsProvider>
      </ElementRefsProvider>
    </MessagesProvider>
  );
}