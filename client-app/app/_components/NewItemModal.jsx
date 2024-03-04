import React from 'react'
import NewItemForm from './forms/NewItemForm'

const NewItemModal = () => {
  return (
    <div>
      <dialog id="create_item_modal" className="modal">
        <div className="modal-box">
          <h3 className="font-bold text-lg pb-4">Create Todo Item</h3>
          <NewItemForm />
        </div>
      </dialog>
    </div>
  )
}

export default NewItemModal