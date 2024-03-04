import { useElementRefsContext } from '../_contexts/ElementRefsContext';

const useElementRefs = () => {
  const { 
    newItemFormRef,
    createItemModalRef
  } = useElementRefsContext();

  return {
    newItemFormRef,
    createItemModalRef
  }
}

export default useElementRefs;