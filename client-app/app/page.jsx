'use client';
import api from '@/api';
import { useEffect, useState } from 'react';

export default function Home() {
  const [items, setItems] = useState([]);
  
  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await api.get('/item/all');
        console.log(response);
        setItems(response.data.items);
      } catch (error) {
        console.error('Error fetching items:', error);
      }
    };

    fetchData();
  }, []);

  return (
    <>
      <h1>Hello World!</h1>
      <ul>
        {items.map((item) => (
          <li key={item.itemId}>
            <p>Name: {item.name}</p>
            {item.description && <p>Description: {item.description}</p>}
            <p>Completed: {item.completed ? 'Yes' : 'No'}</p>
          </li>
        ))}
      </ul>
    </>
  );
}
