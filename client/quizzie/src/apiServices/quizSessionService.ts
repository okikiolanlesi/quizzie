import axiosConfig from "@/config/axios";
import { AxiosResponse } from "axios";

type QuizResultsResponse = {
  results: {
    results: QuizSessionDto[];
    totalCount: number;
    page: number;
    pageSize: number;
  };
};

export interface QuizSessionDto {
  id: string;
  startTime: string;
  endTime: string;
  isCompleted: boolean;
  totalQuestions: null | number;
  score: null | number;
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
    isActive: boolean;
    category: null; // Change the type if category can have properties
    categoryId: string;
    user: null; // Change the type if user can have properties
    userId: string;
    questions: null; // Change the type if questions can have properties
  };
  quizId: string;
  user: {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    passwordHash: string;
    role: string;
    createdAt: string;
    updatedAt: string;
  };
  userId: string;
  userAnswers: null; // Change the type if userAnswers can have properties
}

type UserQuizOption = {
  id: string;
  optionText: string;
};

type QuizQuestion = {
  id: string;
  questionText: string;
  createdAt: string;
  updatedAt: string;
  options: UserQuizOption[];
};

type Quiz = {
  id: string;
  title: string;
  description: string;
  instructions: string;
  duration: number;
  createdAt: string;
  updatedAt: string;
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
  category: {
    id: string;
    title: string;
    description: string;
    createdAt: string;
    updatedAt: string;
  };
  categoryId: string;
  questions: QuizQuestion[];
  isActive: boolean;
};

export type UserAnswer = {
  id: string;
  createdAt: string;
  lastUpdatedAt: string;
  option: UserQuizOption;
  optionId: string;
  question: QuizQuestion;
  questionId: string;
};

export type QuizResult = {
  id: string;
  startTime: string;
  endTime: string;
  isCompleted: boolean;
  totalQuestions: number | null;
  score: number | null;
  createdAt: string;
  updatedAt: string;
  quiz: Quiz;
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
  userAnswers: UserAnswer[];
};

type GetAQuizSessionResponse = {
  results: QuizResult;
};
type AnswerQuestionResponse = {
  result: QuizResult;
};

type SubmitQuizSessionResponse = {
  result: QuizResult;
};

class QuizSessionService {
  static getMyQuizSessions = async (queryParams: {
    searchTerm?: string;
    pageNumber?: number;
    pageSize?: number;
    status?: string;
  }): Promise<AxiosResponse<QuizResultsResponse>> => {
    return await axiosConfig.get("quiz-session", { params: queryParams });
  };

  static getOne = async (
    quizSessionId: string
  ): Promise<AxiosResponse<GetAQuizSessionResponse>> => {
    return await axiosConfig.get(`quiz-session/${quizSessionId}`);
  };

  static answerQuestion = async (
    quizSessionId: string,
    requestBody: { questionId: string; optionId: string }
  ): Promise<AxiosResponse<AnswerQuestionResponse>> => {
    return await axiosConfig.post(
      `quiz-session/answer/${quizSessionId}`,
      requestBody
    );
  };

  static submit = async (
    quizSessionId: string
  ): Promise<AxiosResponse<SubmitQuizSessionResponse>> => {
    return await axiosConfig.post(`quiz-session/submit/${quizSessionId}`);
  };
}

export default QuizSessionService;
