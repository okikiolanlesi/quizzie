import React from "react";
import Link from "next/link";
import { Quiz } from "@/apiServices/quizService";

interface IQuizCard {
  quiz: Quiz;
  backgroundGradient: string;
}

function QuizCard({ quiz, backgroundGradient }: IQuizCard) {
  const color = `${backgroundGradient}`;
  return (
    <div key={quiz.id}>
      <Link className="cursor-pointer" href={`/quiz-details/${quiz.id}`}>
        <div className="bg-white flex justify-between w-full border p-2 rounded-lg hover:shadow-xl hover:bg-slate-100 transition duration-300">
          <div className="flex flex-col w-full pb-4">
            <div
              style={{
                background: color,
                // background: "black",
              }}
              className=" rounded-lg h-48 flex justify-center items-center w-full overflow-hidden"
            >
              <h2 className="text-white text-center font-bold">{quiz.title}</h2>
              {/* <img
                className="w-full h-full hover:scale-150 transition-all ease-in-out duration-300 object-cover"
                src={quiz.quizPicture}
                alt="Quiz Picture"
              /> */}
            </div>

            <p className="font-bold text-lg">{quiz.title}</p>
            <p className="text-sm text-gray-600 line-clamp-2 truncate">
              {quiz.description}{" "}
            </p>
            <p className="text-gray-600">Category: {quiz.category.title}</p>
            <p className="text-gray-600">Duration: {quiz.duration} Minutes </p>
          </div>
        </div>
      </Link>
    </div>
  );
}

export default QuizCard;
