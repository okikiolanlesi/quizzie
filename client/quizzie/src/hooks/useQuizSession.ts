"use client";
import QuizSessionService from "@/apiServices/quizSessionService";
import axiosResponseMessage from "@/utils/axiosResonseMessage";
import {
  InvalidateQueryFilters,
  QueryKey,
  useMutation,
  useQuery,
  useQueryClient,
} from "@tanstack/react-query";
import { useRouter } from "next/navigation";
import toast from "react-hot-toast";

interface GetQuizSessionParams {
  searchTerm?: string;
  pageNumber?: number;
  pageSize?: number;
  status?: "ongoing" | "completed";
}

const useQuizSession = () => {
  const queryClient = useQueryClient();
  const router = useRouter();

  const GetQuizSessions = (params: GetQuizSessionParams) => {
    return useQuery({
      queryKey: ["quizzes", params],
      queryFn: async () => {
        try {
          const res = await QuizSessionService.getMyQuizSessions(params);
          return res?.data;
        } catch (e) {
          console.log(e);
        }
      },
    });
  };

  const GetQuizSession = (quizSessionId: string) => {
    return useQuery({
      queryKey: ["getAQuizSession", quizSessionId],
      queryFn: async () => {
        try {
          const res = await QuizSessionService.getOne(quizSessionId);
          return res?.data.results;
        } catch (e) {
          console.log(e);
          toast.error("Error loadin quiz session");
        }
      },
    });
  };

  const AnswerQuestionMutation = useMutation({
    mutationFn: async (props: {
      quizSessionId: string;
      questionId: string;
      optionId: string;
    }) => {
      const res = await QuizSessionService.answerQuestion(props.quizSessionId, {
        questionId: props.questionId,
        optionId: props.optionId,
      });
      return res?.data;
    },
    onError: (error: any) => {
      toast.error(axiosResponseMessage(error));
    },
    onSuccess: (data) => {
      const queryKey: InvalidateQueryFilters = {
        queryKey: ["getAQuizSession", data.result.id],
      };

      queryClient.invalidateQueries(queryKey);
    },
  });

  const SubmitQuizSession = useMutation({
    mutationFn: async (quizSessionId: string) => {
      const res = await QuizSessionService.submit(quizSessionId);
      return res?.data;
    },
    onError: (error: any) => {
      toast.error(axiosResponseMessage(error));
    },
    onSuccess: (data) => {
      toast.success("Quiz submitted successfully");
    },
  });

  return {
    GetQuizSession,
    AnswerQuestionMutation,
    SubmitQuizSession,
    GetQuizSessions,
  };
};

export default useQuizSession;
