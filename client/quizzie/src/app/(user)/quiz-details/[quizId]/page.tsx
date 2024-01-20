"use client";
import Loader from "@/components/Loader";
import { Button } from "@/components/ui/button";
import useQuiz from "@/hooks/useQuiz";
import Image from "next/image";
import { useRouter } from "next/navigation";
import Markdown from "react-markdown";
import gfm from "remark-gfm";
export default function Instruction({
  params,
}: {
  params: { quizId: string };
}) {
  const router = useRouter();
  const { GetAQuiz, StartQuiz } = useQuiz();

  const { isLoading, data, isError } = GetAQuiz(params.quizId);

  if (isLoading) {
    return (
      <div className=" min-h-52 flex justify-center items-center">
        <Loader size="lg" />
      </div>
    );
  }

  if (isError) {
    return <div>Something went wrong</div>;
  }
  return (
    <section className="w-full mt-2 ">
      <div className="w-full flex justify-center items-center max-h-28 overflow-hidden gap-6">
        <Image
          className="w-full h-full object-cover self-center"
          src="https://th.bing.com/th/id/R.fdc244c2603d3db1bc44b215bf1f9835?rik=6YecZxhgVB2uAQ&pid=ImgRaw&r=0"
          alt=""
          width={100}
          height={100}
          unoptimized
        />
      </div>
      <div className="flex flex-col space-y-6 justify-center items-center border-b-2 py-5 w-full  ">
        <h5 className="font-bold text-center text-4xl">{data?.result.title}</h5>
        <p className="text-center font-light">{data?.result.description}</p>
      </div>
      <div>
        <h5 className="text-xl font-medium text-center mt-6">{}</h5>
        <p className="text-center font-light">
          <span className="font-bold">Total Time:</span> {data?.result.duration}{" "}
          Minutes
        </p>
        <p className="text-center font-light">
          <span className="font-bold">Total Questions:</span>{" "}
          {data?.result.questions.length}
        </p>
        <h5 className="font-bold text-xl text-center mt-6">INSTRUCTION</h5>
        <p className="my-6">
          <Markdown
            remarkPlugins={[gfm]}
            className={"markdown"}
            children={data?.result.instructions!}
          />
        </p>
        <div className="flex justify-center items-center ">
          <Button
            onClick={() => {
              if (data?.ongoingSession) {
                router.push(`/questions/${data?.ongoingSession.id}`);
              } else {
                StartQuiz.mutate(data?.result.id!);
              }
            }}
            className="bg-blue hover:bg-white hover:text-deepBlue hover:border-2 hover:border-deepBlue"
          >
            {!data?.ongoingSession ? "Start" : "Resume"} Quiz
          </Button>
        </div>
      </div>
    </section>
  );
}
