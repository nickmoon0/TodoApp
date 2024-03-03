import api from '@/lib/api';
import { useState, useEffect } from 'react';

const useItems = () => {
  const [items, setItems] = useState([]);
  const [showError, setShowError] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');

  const triggerErrorAlert = () => {
    setShowError(true);
    setTimeout(() => {
      setShowError(false);
    }, 3000); // Show the alert for 3 seconds
  };

  useEffect(() => {
    const loadItems = async () => {
      try {
        const response = await api.get('/item/all');
        setItems(response.data.items);
      } catch (error) {
        setErrorMessage('Failed to retrieve items.');
        triggerErrorAlert();
      }
    }
    
    loadItems();
  }, []);

  const handleItemUpdate = async (itemId, field, newValue) => {
    const updatedItems = items.map((item) => {
      if (item.itemId === itemId) {
        return { ...item, [field]: newValue };
      }
      return item;
    });

    setItems(updatedItems);

    const updatedItem = updatedItems.find(x => x.itemId === itemId);
    try {
      await api.put(`/item/update/${itemId}`, updatedItem);
    } catch (error) {
      setErrorMessage('Failed to update item');
      triggerErrorAlert('Failed to update item.');
      setItems(items); // Rollback changes on screen
    }
  };

  const handleCheckboxChange = async (index) => {
    // Create a new array with updated items
    const updatedItems = items.map((item, idx) => {
      if (idx === index) {
        return { ...item, completed: !item.completed };
      }
      return item;
    });

    setItems(updatedItems);

    const updatedItem = updatedItems[index];
    try {
      await api.put(`/item/update/${updatedItem.itemId}`, updatedItem);
    } catch (error) {
      setErrorMessage('Failed to update item.');
      triggerErrorAlert();
      setItems(items); // Rollback changes on screen
    }
  };

  const handleSelectAllChange = async (event) => {
    const isChecked = event.target.checked;
    const updatedItems = items.map((item, idx) => {
      return { ...item, completed: isChecked };
    });

    setItems(updatedItems);
    // TODO: Post/put updated items back to server as a list. Need to add endpoint to backend
  }

  return { items, showError, errorMessage, handleItemUpdate, handleCheckboxChange, handleSelectAllChange };
};

export default useItems;