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

export interface LoginRequestDto {
  email: string;
  password: string;
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
}

export default AuthService;
