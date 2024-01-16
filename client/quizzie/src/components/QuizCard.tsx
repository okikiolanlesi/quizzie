import React from "react";
import Link from "next/link";

interface Quiz {
  quizName: string;
  quizPicture: string;
  quizId: string;
}

function QuizCard(props: Quiz) {
  return (
    <div key={props.quizId}>
      <Link className="cursor-pointer" href={`/quiz-details/${props.quizId}`}>
        <div className="bg-white flex justify-between w-full border p-2 rounded-lg">
          <div className="flex flex-col w-full pb-4">
            <div className=" h-48 w-full overflow-hidden">
              <img
                className="w-full h-full hover:scale-150 transition-all ease-in-out duration-300 object-cover"
                src={props.quizPicture}
                alt="Quiz Picture"
              />
            </div>

            <p className="text-gray-600">{props.quizName}</p>
          </div>
        </div>
      </Link>
    </div>
  );
}

export default QuizCard;
