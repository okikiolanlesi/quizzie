import axiosConfig from "@/config/axios";
import { AxiosResponse } from "axios";

interface Category {
  id: string;
  title: string;
  description: string;
  createdAt: string;
  updatedAt: string;
  quizzes: null; // Adjust this type if quizzes can have a different structure
}

class CategoryService {
  static getAll = async (): Promise<AxiosResponse<Category[]>> => {
    return await axiosConfig.get("category");
  };
}

export default CategoryService;
