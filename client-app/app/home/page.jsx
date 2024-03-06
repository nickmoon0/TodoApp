import React from 'react'
import ItemList from '@/components/ItemList'
import styles from './styles.module.css';
import NewItemButton from '@/components/NewItemButton';
import NewItemModal from '@/components/NewItemModal';

const HomePage = () => {
  return (
    <>
      <NewItemModal />
      <div className={`grid grid-rows-2 ${styles['grid-rows-auto-height']} py-4`}>
        <div className='grid grid-cols-12'>
          <div className={`col-start-5 col-span-1 ${styles['vertically-center']}`}>
            <h1>Todo Items</h1>
          </div>
          <div className='col-start-8 col-span-1'>
            <NewItemButton />
          </div>
        </div>
        <div className='grid grid-cols-12 py-4'>
          <div className='col-start-5 col-span-4 border rounded-lg'>
            <ItemList />
          </div>
        </div>
      </div>
    </>
  )
}

export default HomePage