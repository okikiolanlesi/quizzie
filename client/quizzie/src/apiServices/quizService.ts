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

type Option = {
  id: string;
  optionText: string;
};

type Question = {
  id: string;
  questionText: string;
  createdAt: string;
  updatedAt: string;
  options: Option[];
};

type QuizDetail = {
  id: string;
  title: string;
  description: string;
  instructions: string;
  duration: number;
  createdAt: string;
  updatedAt: string;
  user: User;
  userId: string;
  category: Category;
  categoryId: string;
  questions: Question[];
  isActive: boolean;
};

class QuizService {
  static getAll = async (queryParams: {
    searchTerm?: string;
    pageNumber?: number;
    pageSize?: number;
    category?: string;
  }): Promise<AxiosResponse<GetAllQuizResponseDto>> => {
    return await axiosConfig.get("quiz", { params: queryParams });
  };

  static getOne = async (
    quizId: string
  ): Promise<AxiosResponse<QuizDetail>> => {
    return await axiosConfig.get(`quiz/${quizId}`);
  };
}

export default QuizService;
