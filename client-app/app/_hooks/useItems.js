import api from '@/lib/api';
import { useState, useEffect } from 'react';
import { useItemsContext } from '@/contexts/ItemsContext';

const useItems = () => {
  const { 
    items,
    setItems,
    showError, 
    setShowError 
  } = useItemsContext();
  
  const [errorMessage, setErrorMessage] = useState('');

  const triggerErrorAlert = () => {
    setShowError(true);
    setTimeout(() => {
      setShowError(false);
    }, 3000); // Show the alert for 3 seconds
  };

  const loadItems = async () => {
    try {
      const response = await api.get('/item/all');
      setItems(response.data.items);
    } catch (error) {
      setErrorMessage('Failed to load items');
      triggerErrorAlert();
    }
  };

  useEffect(() => { 
    loadItems();
  }, []);

  const handleFieldUpdate = async (itemId, field, newValue) => {
    const updatedItems = items.map((item) => {
      if (item.itemId === itemId) {
        // Return a copy of edited item with the new field value
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
      setItems([...items]); // Rollback changes on screen
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
      setItems([...items]); // Rollback changes on screen
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

  const handleCreateItem = async (event) => {
    event.preventDefault();

    const formData = new FormData(event.currentTarget);
    const item = {
      name: formData.get('itemName'),
      description: formData.get('itemDescription'),
      completed: !!formData.get('itemCompleted')
    }

    try {
      await api.post('/item/create', item);
      document.getElementById('create_item_modal').close();
      loadItems();
    } catch (error) {
      setErrorMessage('Failed to create new item');
      triggerErrorAlert();
    }
  };

  return { 
    items, 
    showError, 
    errorMessage,
    loadItems,
    handleFieldUpdate, 
    handleCheckboxChange, 
    handleSelectAllChange,
    handleCreateItem
  };
};

export default useItems;