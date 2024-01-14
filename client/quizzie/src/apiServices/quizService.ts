import axiosConfig from "@/config/axios";
import { AxiosResponse } from "axios";

interface Category {
  id: string;
  title: string;
  description: string;
  createdAt: string;
  updatedAt: string;
}

interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  passwordHash: string;
  role: string;
  createdAt: string;
  updatedAt: string;
}

interface Quiz {
  id: string;
  title: string;
  description: string;
  instructions: string;
  duration: number;
  createdAt: string;
  updatedAt: string;
  isActive: boolean;
  category: Category;
  categoryId: string;
  user: User;
  userId: string;
}

export interface GetAllQuizResponseDto {
  results: Quiz[];
  totalCount: number;
  page: number;
  pageSize: number;
}

class QuizService {
  static getAll = async (queryParams: {
    searchTerm?: string;
    pageNumber?: number;
    pageSize?: number;
    category?: string;
  }): Promise<AxiosResponse<GetAllQuizResponseDto>> => {
    return await axiosConfig.get("quiz", { params: queryParams });
  };
}

export default QuizService;
