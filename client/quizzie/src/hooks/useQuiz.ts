"use client";
import QuizService from "@/apiServices/quizService";
import { useAuthState } from "@/store/authStore";
import axiosResponseMessage from "@/utils/axiosResonseMessage";
import { useMutation, useQuery } from "@tanstack/react-query";
import { useRouter } from "next/navigation";
import toast from "react-hot-toast";
import { string } from "yup";

interface GetQuizzesParams {
  searchTerm?: string;
  pageNumber?: number;
  pageSize?: number;
  category?: string;
}

const useQuiz = () => {
  const router = useRouter();
  const GetQuizzes = (params: GetQuizzesParams) => {
    return useQuery({
      queryKey: ["quizzes", params],
      queryFn: async () => {
        try {
          const res = await QuizService.getAll(params);
          return res?.data;
        } catch (e) {
          console.log(e);
        }
      },
    });
  };

  const GetAQuiz = (quizId: string) => {
    return useQuery({
      queryKey: ["getAQuiz", quizId],
      queryFn: async () => {
        try {
          const res = await QuizService.getOne(quizId);
          return res?.data;
        } catch (e) {
          console.log(e);
          toast.error("Error fetching quiz details");
        }
      },
    });
  };

  const StartQuiz = useMutation({
    mutationFn: async (quizId: string) => {
      const res = await QuizService.startQuiz(quizId);
      return res?.data;
    },
    onError: (error: any) => {
      toast.error(axiosResponseMessage(error));
    },
    onSuccess: (data) => {
      toast.success("Quiz started, taking you to the hall");
      router.push(`/questions/${data.result.id}`);
    },
  });

  return { GetQuizzes, GetAQuiz, StartQuiz };
};

export default useQuiz;
