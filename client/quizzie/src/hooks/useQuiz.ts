"use client";
import QuizService from "@/apiServices/quizService";
import { useAuthState } from "@/store/authStore";
import axiosResponseMessage from "@/utils/axiosResonseMessage";
import { useQuery } from "@tanstack/react-query";
import toast from "react-hot-toast";

interface GetQuizzesParams {
  searchTerm?: string;
  pageNumber?: number;
  pageSize?: number;
  category?: string;
}

const useQuiz = () => {
  const getQuizzes = (params: GetQuizzesParams) => {
    return useQuery({
      queryKey: ["quizzes", params],
      queryFn: async () => {
        try {
          const res = await QuizService.getAll(params);
          console.log(res.data);
          return res?.data;
        } catch (e) {
          console.log(e);
        }
      },
    });
  };

  return { getQuizzes };
};

export default useQuiz;
