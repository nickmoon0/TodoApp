'use client';
import React from 'react'

const NewItemButton = () => {
  return (
    <button 
      className="btn btn-active btn-primary w-full"
      onClick={() => document.getElementById('create_item_modal').showModal()}>Create Item</button>
  )
}

export default NewItemButton