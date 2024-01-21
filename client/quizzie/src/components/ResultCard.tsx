import React from "react";
import Link from "next/link";
import { Button } from "./ui/button";
import { QuizSessionDto } from "@/apiServices/quizSessionService";
import { useRouter } from "next/navigation";

interface IQuizCard {
  quizSession: QuizSessionDto;
  backgroundGradient: string;
}

function QuizCard({ quizSession, backgroundGradient }: IQuizCard) {
  const color = `${backgroundGradient}`;
  const router = useRouter();
  return (
    <div key={quizSession.id}>
      <div className="bg-white flex justify-between w-full border p-2 rounded-lg hover:shadow-xl hover:bg-slate-100 transition duration-300">
        <div className="flex flex-col w-full pb-4">
          <div
            style={{
              background: color,
              // background: "black",
            }}
            className=" rounded-lg h-48 flex justify-center items-center w-full overflow-hidden"
          >
            <h2 className="text-white text-xl text-center font-bold">
              {quizSession.quiz.title}
            </h2>
            {/* <img
                className="w-full h-full hover:scale-150 transition-all ease-in-out duration-300 object-cover"
                src={quiz.quizPicture}
                alt="Quiz Picture"
              /> */}
          </div>

          <p className="font-bold my-3 text-lg">{quizSession.quiz.title}</p>
          {quizSession.isCompleted ? (
            <>
              <p className="text-gray-600">
                <span className="font-bold ">Score: </span>
                {quizSession.score}/{quizSession.totalQuestions}
                {" ("}
                {quizSession.totalQuestions === 0
                  ? "100%)"
                  : (
                      (quizSession.score! / quizSession.totalQuestions!) *
                      100
                    ).toFixed(2) + "%)"}
              </p>
            </>
          ) : (
            <>
              <Button
                onClick={() => {
                  router.push(`/questions/${quizSession.id}`);
                }}
              >
                Resume Quiz
              </Button>
            </>
          )}
        </div>
      </div>
    </div>
  );
}

export default QuizCard;
