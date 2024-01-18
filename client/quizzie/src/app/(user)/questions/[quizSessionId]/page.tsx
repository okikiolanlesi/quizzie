"use client";

import { useEffect, useState } from "react";
import useQuizSession from "@/hooks/useQuizSession";
import { Button } from "@/components/ui/button";
import { UserAnswer } from "@/apiServices/quizSessionService";
import Loader from "@/components/Loader";
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group";
import { Label } from "@/components/ui/label";
import { useRouter } from "next/navigation";
import toast from "react-hot-toast";
import QuizTimer from "@/components/QuizTimer";
import ConfirmModal from "@/components/ConfirmModal";

interface IAnswerMap {
  [key: string]: UserAnswer;
}

const getCurrentQuestionIndex = (quizSessionId: string) => {
  return parseInt(
    localStorage.getItem(quizSessionId + "CurrentQuestionId") || "0"
  );
};

export default function QuizQuestion({
  params,
}: {
  params: { quizSessionId: string };
}) {
  const router = useRouter();
  const [answers, setAnswers] = useState<IAnswerMap>({});
  const [showSubmitModal, setShowSubmitModal] = useState<boolean>(false);

  const [selectedQuestionIndex, setSelectedQuestionIndex] = useState<number>(
    getCurrentQuestionIndex(params.quizSessionId)
  );

  const { GetQuizSession, AnswerQuestionMutation, SubmitQuizSession } =
    useQuizSession();

  const getQuzSessionQuery = GetQuizSession(params.quizSessionId);

  useEffect(() => {
    const userAnswersQuestionMap: IAnswerMap = {};

    if (getQuzSessionQuery?.data) {
      for (const answer of getQuzSessionQuery?.data?.userAnswers) {
        userAnswersQuestionMap[answer.questionId] = answer;
      }
      if (
        new Date(getQuzSessionQuery.data?.endTime!) <= new Date() ||
        getQuzSessionQuery.data.isCompleted
      ) {
        toast.error("Invalid quiz session");
        router.push("/dashboard");
      }
    }

    setAnswers(userAnswersQuestionMap);
  }, [getQuzSessionQuery.data]);

  useEffect(() => {
    if (SubmitQuizSession.isSuccess) {
      alert(
        SubmitQuizSession.data.result.score +
          "/" +
          SubmitQuizSession.data.result.totalQuestions
      );
    }
  }, [SubmitQuizSession.isSuccess]);

  if (getQuzSessionQuery.isLoading || SubmitQuizSession.isPending) {
    return (
      <div className=" min-h-52 flex justify-center items-center">
        <Loader size="lg" />
      </div>
    );
  }

  if (getQuzSessionQuery.isError) {
    return <div>Something went wrong</div>;
  }

  return (
    <>
      <ConfirmModal
        title="Are you sure?"
        paragraph="Please press continue to submit or cancel to return to your quiz"
        onContinue={() => {
          SubmitQuizSession.mutate(params.quizSessionId);
          setShowSubmitModal(false);
        }}
        onCancel={setShowSubmitModal}
        isOpen={showSubmitModal}
      />
      <div className="px-4 pb-2 border-b-[5px] border-[#A934F1]">
        <div className="flex flex-col sm:flex-row space-y-5 sm:space-y-0 justify-between sm:items-center font-bold text-md">
          <h1>{getQuzSessionQuery.data?.quiz.title}</h1>
          <div className="flex items-center gap-4">
            <QuizTimer
              endTime={new Date(getQuzSessionQuery.data?.endTime!)}
              startTime={new Date(getQuzSessionQuery.data?.startTime!)}
              onTimeEnd={() => {
                SubmitQuizSession.mutate(params.quizSessionId);
              }}
            />
          </div>
        </div>
      </div>

      <div className="font-bold">
        <div className="flex justify-end">
          <Button
            onClick={() => setShowSubmitModal(true)}
            className="bg-purple mt-3 hover:bg-white border hover:text-purple-500 hover:border-slate-950"
          >
            Submit
          </Button>
        </div>
        <h2 className="mt-2 mb-5 mx-3 text-2xl">
          {
            getQuzSessionQuery.data?.quiz.questions[selectedQuestionIndex]
              ?.questionText
          }
        </h2>
        <RadioGroup
          className="space-y-3"
          value={
            answers[
              getQuzSessionQuery.data?.quiz.questions[selectedQuestionIndex]
                ?.id!
            ]?.optionId
          }
          onValueChange={(val) => {
            AnswerQuestionMutation.mutate({
              questionId:
                getQuzSessionQuery.data?.quiz.questions[selectedQuestionIndex]
                  ?.id!,
              optionId: val,
              quizSessionId: params.quizSessionId,
            });
          }}
        >
          {getQuzSessionQuery.data?.quiz.questions[
            selectedQuestionIndex
          ]?.options?.map((option) => (
            <div
              key={option.id}
              className="flex items-center space-x-2 border-2 border-purple p-2"
            >
              <RadioGroupItem
                className="rounded-none focus-visible:rounded-none"
                value={option.id}
                id={option.id}
              />
              <Label htmlFor={option.id}>{option.optionText}</Label>
            </div>
          ))}
        </RadioGroup>
      </div>

      <div className="mt-8 flex justify-between">
        <div>
          <Button
            onClick={() => {
              const prevIndex = selectedQuestionIndex - 1;
              if (prevIndex >= 0) {
                localStorage.setItem(
                  params.quizSessionId + "CurrentQuestionId",
                  prevIndex.toString()
                );

                setSelectedQuestionIndex(prevIndex);
              }
            }}
            className="bg-purple hover:bg-white border hover:text-purple-500 hover:border-slate-950"
            disabled={!(selectedQuestionIndex - 1 >= 0)}
          >
            Previous
          </Button>
        </div>
        <div>
          <Button
            onClick={() => {
              const nextIndex = selectedQuestionIndex + 1;
              if (
                nextIndex <
                (getQuzSessionQuery.data?.quiz.questions.length || 0)
              ) {
                localStorage.setItem(
                  params.quizSessionId + "CurrentQuestionId",
                  nextIndex.toString()
                );

                setSelectedQuestionIndex(nextIndex);
              }
            }}
            disabled={
              !(
                selectedQuestionIndex + 1 <
                (getQuzSessionQuery.data?.quiz.questions.length || 0)
              )
            }
            className="bg-purple hover:bg-white border hover:text-purple-500 hover:border-slate-950"
          >
            Next
          </Button>
        </div>
      </div>
      <div
        className="font-bold my-10 justify-center"
        style={{ display: "flex", flexWrap: "wrap", gap: "10px" }}
      >
        {getQuzSessionQuery.data?.quiz.questions.map((question, index) => (
          <div
            key={question.id}
            style={{
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
              width: "50px",
              height: "50px",
              border:
                index === selectedQuestionIndex
                  ? "2px solid blue"
                  : "1px solid black",
              scale: index !== selectedQuestionIndex ? "100%" : "120%",
              borderRadius: "5px",
              backgroundColor:
                index === selectedQuestionIndex
                  ? "#1B223F"
                  : answers[question.id]
                  ? "#A934F1"
                  : "transparent",
              color: index !== selectedQuestionIndex ? "black" : "#fff",
            }}
            onClick={() => {
              localStorage.setItem(
                params.quizSessionId + "CurrentQuestionId",
                index.toString()
              );

              setSelectedQuestionIndex(index);
            }}
          >
            {index + 1}
          </div>
        ))}
      </div>
    </>
  );
}
