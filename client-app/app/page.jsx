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
    <div>
      {/* TODO: Create loading screen */}
      <p>Loading...</p>
    </div>
  );
}
