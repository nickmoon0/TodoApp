'use client';
import React from 'react'
import useItems from '@/hooks/useItems';

const NewItemForm = () => {
  const { handleCreateItem } = useItems();

  return (
    <>
      <form id="new-item-form" onSubmit={handleCreateItem} className='flex flex-col space-y-2 px-4 pb-4'>
        <input type="text" name="itemName" placeholder="Item Name"  className="input input-bordered input-primary w-full max-w-xs" />
        <input type="text" name="itemDescription" placeholder="Item Description"  className="input input-bordered input-primary w-full max-w-xs" />
        <div className='flex items-center space-x-2'>
          <input type="checkbox" id="itemCompleted" name="itemCompleted" className='checkbox' />
          <label htmlFor='itemCompleted'>Completed</label>
        </div>
      </form>
      <div className="modal-action flex justify-end space-x-2">
        <button type="submit" form="new-item-form" className="btn btn-primary">Submit</button>
        <form method="dialog">
          <button className="btn">Close</button>
        </form>
      </div>
    </>
  )
}

export default NewItemForm