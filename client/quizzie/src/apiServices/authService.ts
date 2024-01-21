import axiosConfig from "@/config/axios";
import { AxiosResponse } from "axios";

export interface RegisterRequestDto {
  email: string;
  password: string;
  confirmPassword: string;
  firstName: string;
  lastName: string;
}

export interface RegisterResponseDto {
  message: string;
}

export interface LoginRequestDto {
  email: string;
  password: string;
}

export interface VerifyEmailRequestDto {
  token: string;
  userId: string;
}
export interface ResetPasswordRequestDto {
  token: string;
  newPassword: string;
}

export interface LoginResponseDto {
  message: string;
  user: {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    role: string;
    createdAt: Date;
    updatedAt: Date;
  };
  token: string;
}
type VerifyEmailResponseDto = LoginResponseDto;
type ForgotPasswordResponseDto = { message: string };
type ResettPasswordResponseDto = { message: string };

class AuthService {
  static register = async (
    requestBody: RegisterRequestDto
  ): Promise<AxiosResponse<RegisterResponseDto>> => {
    return await axiosConfig.post("auth/register", requestBody);
  };

  static login = async (
    requestBody: LoginRequestDto
  ): Promise<AxiosResponse<LoginResponseDto>> => {
    return await axiosConfig.post("auth/login", requestBody);
  };

  static verifyEmail = async (
    requestParams: VerifyEmailRequestDto
  ): Promise<AxiosResponse<VerifyEmailResponseDto>> => {
    return await axiosConfig.get("auth/verify-email", {
      params: requestParams,
    });
  };

  static forgotPassword = async (
    email: string
  ): Promise<AxiosResponse<ForgotPasswordResponseDto>> => {
    return await axiosConfig.post("auth/forgot-password", {
      email,
    });
  };
  static resetPassword = async (
    requestBody: ResetPasswordRequestDto
  ): Promise<AxiosResponse<ResettPasswordResponseDto>> => {
    return await axiosConfig.post("auth/reset-password", requestBody);
  };
}

export default AuthService;
