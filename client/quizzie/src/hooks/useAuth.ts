"use client";
import AuthService, {
  LoginRequestDto,
  RegisterRequestDto,
  ResetPasswordRequestDto,
} from "@/apiServices/authService";
import { useAuthState } from "@/store/authStore";
import axiosResponseMessage from "@/utils/axiosResonseMessage";
import { useMutation } from "@tanstack/react-query";
import { useRouter } from "next/navigation";
import toast from "react-hot-toast";

const useAuth = () => {
  const { setUser, setToken, user, token } = useAuthState((state) => state);
  const router = useRouter();

  const signUpMutation = useMutation({
    mutationFn: async (user: RegisterRequestDto) => {
      const res = await AuthService.register(user);
      return res?.data;
    },
    onError: (error: any) => {
      toast.error(axiosResponseMessage(error));
    },
    onSuccess: (data) => {
      const { message } = data;
      toast.success(message);
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
      const { message, user, token } = data;
      toast.success(message);
      setUser(user);
      setToken(token);

      if (user.role === "Admin") {
        router.push("/admin/dashboard");
      } else {
        router.push("/dashboard");
      }
    },
  });

  const verifyEmailMutation = useMutation({
    mutationFn: async (requestParams: { token: string; userId: string }) => {
      const res = await AuthService.verifyEmail(requestParams);
      return res?.data;
    },
    onError: (error: any) => {
      toast.error(axiosResponseMessage(error));
    },
    onSuccess: (data) => {
      const { message, user, token } = data;
      toast.success(message);
      setUser(user);
      setToken(token);

      if (user.role === "Admin") {
        router.push("/admin/dashboard");
      } else {
        router.push("/dashboard");
      }
    },
  });

  const forgotPasswordMutation = useMutation({
    mutationFn: async ({ email }: { email: string }) => {
      const res = await AuthService.forgotPassword(email);
      return res?.data;
    },
    onError: (error: any) => {
      toast.error(axiosResponseMessage(error));
    },
    onSuccess: (data) => {
      const { message } = data;
      toast.success(message);
      toast.success("Please check your email for the next steps");
    },
  });

  const resetPasswordMutation = useMutation({
    mutationFn: async (requestBody: ResetPasswordRequestDto) => {
      const res = await AuthService.resetPassword(requestBody);
      return res?.data;
    },
    onError: (error: any) => {
      toast.error(axiosResponseMessage(error));
    },
    onSuccess: (data) => {
      const { message } = data;
      toast.success(message);
      router.push("/login");
    },
  });

  return {
    signUpMutation,
    loginMutation,
    verifyEmailMutation,
    user,
    token,
    forgotPasswordMutation,
    resetPasswordMutation,
  };
};

export default useAuth;
