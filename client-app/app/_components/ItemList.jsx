'use client';
import React, { useEffect, useState } from 'react';
import ErrorAlert from '@/components/errors/ErrorAlert';
import EditableField from '@/components/EditableField';
import useItems from '../_hooks/useItems';

const ItemList = () => {
  const { items, showError, errorMessage, handleItemUpdate, handleCheckboxChange, handleSelectAllChange } = useItems();

  return (
    <>
      {showError && <ErrorAlert message={errorMessage} />}
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