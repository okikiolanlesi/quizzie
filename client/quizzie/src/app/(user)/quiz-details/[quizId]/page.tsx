"use client";
import Loader from "@/components/Loader";
import { Button } from "@/components/ui/button";
import useQuiz from "@/hooks/useQuiz";
export default function Instruction({
  params,
}: {
  params: { quizId: string };
}) {
  const { getAQuiz } = useQuiz();

  const { isLoading, data } = getAQuiz(params.quizId);

  if (isLoading) {
    return (
      <div className=" min-h-52 flex justify-center items-center">
        <Loader size="lg" />
      </div>
    );
  }

  return (
    <section className="w-full mt-2 ">
      <div className="w-full flex justify-center items-center max-h-28 overflow-hidden gap-6">
        <img
          className="w-full h-full object-cover"
          src="https://img.freepik.com/free-photo/stock-market-exchange-economics-investment-graph_53876-167143.jpg?size=626&ext=jpg&ga=GA1.2.212456353.1696925456&semt=ais"
          alt=""
        />
      </div>
      <div className="flex flex-col space-y-6 justify-center items-center border-b-2 py-5 w-full  ">
        <h5 className="font-bold text-center text-4xl">{data?.title}</h5>
        <p className="text-center font-light">{data?.description}</p>
      </div>
      <div>
        <h5 className="text-xl font-medium text-center mt-6">{}</h5>
        <p className="text-center font-light">
          <span className="font-bold">Total Time:</span> {data?.duration}{" "}
          Minutes
        </p>
        <p className="text-center font-light">
          <span className="font-bold">Total Questions:</span>{" "}
          {data?.questions.length}
        </p>
        <h5 className="font-bold text-xl text-center mt-6">INSTRUCTION</h5>
        <p className="my-6">{data?.instructions}</p>
        <div className="flex justify-center items-center">
          <Button>Start Quiz</Button>
        </div>
      </div>
    </section>
  );
}
