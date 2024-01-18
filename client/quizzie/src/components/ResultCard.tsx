import React from "react";
import Link from "next/link";
import { QuizSessionGetAll } from "@/apiServices/quizSessionService";

interface ResultCardProps {
  quizSession: QuizSessionGetAll;
}

function ResultCard({ quizSession }: ResultCardProps) {
  return (
    <div key={quizSession.id}>
      <Link className="cursor-pointer" href={`/quiz-details/`}>
        <div className="bg-white flex justify-between w-full border p-2 rounded-lg">
          <div className="flex flex-col w-full pb-4">
            <div className=" h-48 w-full overflow-hidden">
              {/* <img
                className="w-full h-full hover:scale-150 transition-all ease-in-out duration-300 object-cover"
                src={props.quizPicture}
                alt="Quiz Picture"
              /> */}
            </div>

            <p className="text-gray-600">
              Score: {quizSession.score}/{quizSession.totalQuestions}
              {" ("}
              {quizSession.totalQuestions === 0
                ? "100%)"
                : (
                    (quizSession.score! / quizSession.totalQuestions!) *
                    100
                  ).toFixed(2) + "%)"}
            </p>
            {/* <p className="text-gray-600">{quizSession}</p> */}
          </div>
        </div>
      </Link>
    </div>
  );
}

export default ResultCard;
