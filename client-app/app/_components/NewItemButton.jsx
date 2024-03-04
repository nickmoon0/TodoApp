'use client';
import React from 'react'
import useElementRefs from '@/hooks/useElementRefs';

const NewItemButton = () => {
  const { createItemModalRef } = useElementRefs();

  return (
    <button 
      className="btn btn-active btn-primary w-full"
      onClick={() => createItemModalRef.current.showModal()}>Create Item</button>
  )
}

export default NewItemButton