'use client';

import { ItemsProvider } from "@/contexts/ItemsContext";
import { ElementRefsProvider } from "@/contexts/ElementRefsContext";
import { MessagesProvider } from "@/contexts/MessagesContext";
import { TokenProvider } from "@/contexts/TokenContext";
import { ApiProvider } from "@/contexts/ApiContext";

export function Providers({ children }) {
  return (
    <TokenProvider>
      <ApiProvider>
        <MessagesProvider>
          <ElementRefsProvider>
            <ItemsProvider>
              {children}
            </ItemsProvider>
          </ElementRefsProvider>
        </MessagesProvider>
      </ApiProvider>
    </TokenProvider>
  );
}