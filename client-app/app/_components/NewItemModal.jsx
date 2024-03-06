'use client';
import React from 'react'
import NewItemForm from './forms/NewItemForm'
import useElementRefs from '@/hooks/useElementRefs';

const NewItemModal = () => {
  const { createItemModalRef } = useElementRefs();

  return (
    <div>
      <dialog ref={createItemModalRef} className="modal">
        <div className="modal-box">
          <h3 className="font-bold text-lg pb-4">Create Todo Item</h3>
          <NewItemForm />
        </div>
      </dialog>
    </div>
  )
}

export default NewItemModal