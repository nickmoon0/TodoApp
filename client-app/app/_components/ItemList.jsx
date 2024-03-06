'use client';
import ErrorAlert from '@/components/errors/ErrorAlert/ErrorAlert';
import EditableField from '@/components/EditableField';
import useItems from '@/hooks/useItems';
import DeleteItemButton from './DeleteItemButton';

const ItemList = () => {
  const { 
    items,
    showError,
    errorMessage,
    handleFieldUpdate,
    handleDeleteItem,
    handleCheckboxChange,
    handleSelectAllChange,
  } = useItems();

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
              <th>Delete</th>
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
                    onTextChange={(newValue) => handleFieldUpdate(item.itemId, 'name', newValue)}
                  />
                </td>
                <td>
                  <EditableField
                    text={item.description}
                    placeHolderText='No Description'
                    onTextChange={(newValue) => handleFieldUpdate(item.itemId, 'description', newValue)}
                  />
                </td>
                <td>
                  <DeleteItemButton onClickHandler={() => handleDeleteItem(index)}/>
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