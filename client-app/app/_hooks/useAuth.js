import useItems from "./useItems";
import { useRouter } from "next/navigation";
import { useApi } from "./useApi";
import { useToken } from "./useToken";

const useAuth = () => {
  const router = useRouter();
  
  const { api } = useApi();
  const { setToken, deleteToken } = useToken();
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

  const handleLogin = async ({username, password}, errorCallback) => {
    try {
      const response = await api.post("/auth/login", { username, password }, { withCredentials: true });
      
      const token = response.data.accessToken;
      setToken(token);
      
      router.push('/home');
    } catch (error) {
      errorCallback(error);
    }
  };

  const handleRegister = async ({ username, password }, errorCallback) => {
    try {
      const response = await api.post("/user/create", { username, password }, { withCredentials:true });

      const token = response.data.accessToken;
      setToken(token);
      
      router.push('/home');
    } catch (error) {
      errorCallback(error);
    }
  };

  return {
    handleLogout,
    handleLogin,
    handleRegister
  }
};

export default useAuth;