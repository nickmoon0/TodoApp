'use client';

import { ItemsProvider } from "@/contexts/ItemsContext";
import { ElementRefsProvider } from "@/contexts/ElementRefsContext";
import { MessagesProvider } from "@/contexts/MessagesContext";
import { ApiProvider } from "@/contexts/ApiContext";

export function Providers({ children }) {
  return (
    <ApiProvider>
      <MessagesProvider>
        <ElementRefsProvider>
          <ItemsProvider>
            {children}
          </ItemsProvider>
        </ElementRefsProvider>
      </MessagesProvider>
    </ApiProvider>
  );
}