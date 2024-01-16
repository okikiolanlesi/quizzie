"use client";
import AuthService, {
  LoginRequestDto,
  RegisterRequestDto,
} from "@/apiServices/authService";
import CategoryService from "@/apiServices/categoryService";
import { useQuery } from "@tanstack/react-query";

const useCategories = (page = 1) => {
  const GetCategories = () => {
    return useQuery({
      queryKey: ["categories"],
      queryFn: async () => {
        const res = await CategoryService.getAll();
        return res?.data;
      },
    });
  };

  return { GetCategories };
};

export default useCategories;
