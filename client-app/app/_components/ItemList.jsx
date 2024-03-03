'use client';
import React, { useEffect, useState } from 'react';
import api from '@/lib/api';
import ErrorAlert from '@/components/errors/ErrorAlert';
import EditableField from '@/components/EditableField';

const ItemList = () => {
  const [items, setItems] = useState([]);
  const [showError, setShowError] = useState(false);

  useEffect(() => {
    // Retrieve all items
    const fetchData = async () => {
      try {
        const response = await api.get('/item/all');
        setItems(response.data.items);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();
  }, []);

  const triggerErrorAlert = () => {
    setShowError(true);
    setTimeout(() => {
      setShowError(false);
    }, 3000); // Show the alert for 3 seconds
  };

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
      triggerErrorAlert('Failed to update item');
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

  return (
    <>
      {showError && <ErrorAlert message='Failed to update item' />}
      <div className='overflow-x-auto'>
        <table className='table table-md'>
          <thead>
            <tr>
              <th>
                <label>
                  <input type='checkbox' className='checkbox' onChange={handleSelectAllChange} />
                </label>
              </th>
              <th>Name</th>
              <th>Description</th>
            </tr>
          </thead>
          <tbody>
            {items.map((item, index) => (
              <tr key={index}>
                <td>
                  <input
                    type='checkbox'
                    className='checkbox'
                    checked={item.completed}
                    onChange={() => handleCheckboxChange(index)} />
                </td>
                <td>
                  <EditableField
                    text={item.name}
                    placeHolderText='No Name'
                    onTextChange={(newValue) => handleItemUpdate(item.itemId, 'name', newValue)}
                  />
                </td>
                <td>
                  <EditableField
                    text={item.description}
                    placeHolderText='No Description'
                    onTextChange={(newValue) => handleItemUpdate(item.itemId, 'description', newValue)}
                  />
                </td>

              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </>
  )
}

export default ItemList