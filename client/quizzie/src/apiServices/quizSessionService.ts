import axiosConfig from "@/config/axios";
import { AxiosResponse } from "axios";

type QuizResultsResponse = {
  results: {
    results: QuizSessionGetAll[];
    totalCount: number;
    page: number;
    pageSize: number;
  };
};

export type QuizSessionGetAll = {
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
  userAnswers: null | any[];
};

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

type QuizResult = {
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
