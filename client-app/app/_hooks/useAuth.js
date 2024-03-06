import api from "@/lib/api";
import { deleteToken } from "@/lib/getToken";
import useItems from "./useItems";
import { useRouter } from "next/navigation";

const useAuth = () => {
  const router = useRouter();
  const { triggerErrorAlert } = useItems();
  
  const handleLogout = async () => {
    try {
      await api.get('/auth/logout', { withCredentials: true });
      deleteToken(); // Deletes access token

      router.push('/login');
    } catch (error) {
      triggerErrorAlert('Failed to logout user');
    }
  };

  return {
    handleLogout
  }
};

export default useAuth;