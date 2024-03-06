import api from '@/lib/api';
import { useEffect } from 'react';
import { useItemsContext } from '@/contexts/ItemsContext';
import { useElementRefsContext } from '@/contexts/ElementRefsContext';
import { useMessagesContext } from '@/contexts/MessagesContext';

const useItems = () => {
  const { 
    items,
    setItems,
    showError, 
    setShowError 
  } = useItemsContext();
  const { errorMessage, setErrorMessage } = useMessagesContext();
  const { newItemFormRef, createItemModalRef } = useElementRefsContext();

  const triggerErrorAlert = (errorMsg) => {
    setErrorMessage(errorMsg);
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
      triggerErrorAlert('Failed to load items');
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
      triggerErrorAlert('Failed to update item.');
      setItems([...items]); // Rollback changes on screen
    }
  };

  const handleDeleteItem = async (index) => {
    const deletedItem = items[index];
    const newItemList = items.filter(x => x.itemId !== deletedItem.itemId);
    
    try {
      await api.delete(`/item/delete/${deletedItem.itemId}`);
      setItems(newItemList);
    } catch (error) {
      triggerErrorAlert('Failed to delete item');
    }
  }

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
      
      createItemModalRef.current.close(); // Close the create account popup
      newItemFormRef.current.reset(); // Clear form fields
      
      loadItems();
    } catch (error) {
      triggerErrorAlert('Failed to create new item');
    }
  };

  return { 
    items, 
    showError,
    errorMessage,
    loadItems,
    handleFieldUpdate, 
    handleCheckboxChange,
    handleDeleteItem,
    handleSelectAllChange,
    handleCreateItem,
    triggerErrorAlert
  };
};

export default useItems;