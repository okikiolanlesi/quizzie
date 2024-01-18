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

type QuizDetailResponse = {
  result: QuizDetail;
  ongoingSession: null | {
    id: string;
    startTime: string;
    endTime: string;
    isCompleted: boolean;
    totalQuestions: number | null;
    score: number | null;
    createdAt: string;
    updatedAt: string;
    quiz: null;
    quizId: string;
    user: null;
    userId: string;
    userAnswers: any[];
  };
};

type StartQuizResponse = {
  message: string;
  result: {
    id: string;
    startTime: string;
    endTime: string;
    isCompleted: boolean;
    totalQuestions: number | null;
    score: number | null;
    createdAt: string;
    updatedAt: string;
    quiz: {
      id: string;
      title: string;
      description: string;
      instructions: string;
      duration: number;
      createdAt: string;
      updatedAt: string;
      user: null;
      userId: string;
      category: null;
      categoryId: string;
      questions: any[]; // You might want to replace 'any[]' with the actual type of your questions array
      isActive: boolean;
    };
    quizId: string;
    user: {
      id: string;
      firstName: string;
      lastName: string;
      email: string;
      role: string;
      createdAt: string;
      updatedAt: string;
    };
    userId: string;
    userAnswers: any[]; // You might want to replace 'any[]' with the actual type of your userAnswers array
  };
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
  ): Promise<AxiosResponse<QuizDetailResponse>> => {
    return await axiosConfig.get(`quiz/${quizId}`);
  };

  static startQuiz = async (
    quizId: string
  ): Promise<AxiosResponse<StartQuizResponse>> => {
    return await axiosConfig.post(`quiz/start/${quizId}`);
  };
}

export default QuizService;
