import React, { useState } from 'react'

const EditableField = ({ text, placeHolderText, onTextChange }) => {
  const [isEditing, setIsEditing] = useState(false);
  const [value, setValue] = useState(text);

  const handleBlur = () => {
    setIsEditing(false);

    // Only call onTextChange if a change has actually been made
    if (value !== text) {
      onTextChange(value);
    }
  };

  const handleKeyDown = (event) => {
    if (event.key === 'Enter') {
      handleBlur();
      event.target.blur();
    }
  };

  return (
    <div onClick={() => setIsEditing(true)}>
      {isEditing ? (
        <input
          type="text"
          value={value}
          onChange={(e) => setValue(e.target.value)}
          onBlur={handleBlur}
          onKeyDown={handleKeyDown}
          autoFocus
          className="input input-sm input-bordered"
        />
      ) : (
        <span>{text || <span className="text-slate-700">{placeHolderText}</span>}</span>
      )}
    </div>
  )
}

export default EditableField