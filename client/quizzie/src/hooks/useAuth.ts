import AuthService, {
  LoginRequestDto,
  RegisterRequestDto,
} from "@/apiServices/authService";
import { useAuthState } from "@/store/authStore";
import axiosResponseMessage from "@/utils/axiosResonseMessage";
import { useMutation } from "@tanstack/react-query";
import toast from "react-hot-toast";

const useAuth = () => {
  const { setUser, setToken, user, token } = useAuthState((state) => state);

  const signUpMutation = useMutation({
    mutationFn: async (user: RegisterRequestDto) => {
      const res = await AuthService.register(user);
      return res?.data;
    },
    onError: (error: any) => {
      toast.error(axiosResponseMessage(error));
    },
    onSuccess: (data) => {
      toast.success(data.message);
      setUser(data.user);
      setToken(data.token);
    },
  });

  const loginMutation = useMutation({
    mutationFn: async (user: LoginRequestDto) => {
      const res = await AuthService.login(user);
      return res?.data;
    },
    onError: (error: any) => {
      toast.error(axiosResponseMessage(error));
    },
    onSuccess: (data) => {
      toast.success(data.message);
      setUser(data.user);
      setToken(data.token);
    },
  });

  return { signUpMutation, loginMutation, user, token };
};

export default useAuth;
