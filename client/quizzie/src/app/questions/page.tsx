"use client";

import { use, useEffect, useState } from "react";
import { Progress } from "@/components/progressBar";

export default function QuizQuestion() {
  const [selectedOption, setSelectedOption] = useState<number | null>(null);
  const [progress, setProgress] = useState(10);
  useEffect(() => {
    const timer = setTimeout(() => setProgress(70), 500);
    return () => clearTimeout(timer);
  }, []);

  const question = "1. 'Cyberspace' was coined by";
  const options = [
    "Andrew Tannenboum",
    "William Gibson",
    "Andrew Tannenboum",
    "Andrew Tannenboum",
  ];
  const numbers = Array.from({ length: 10 }, (_, i) => i + 1);

  return (
    <>
      <div className="px-4 py-10 border-b-[5px] border-[#A934F1]">
        <div className="flex justify-between items-center font-bold text-md">
          <h1>CYBERSECURITY</h1>
          <div className="flex items-center gap-4">
            <p className="">00:00</p>
            {/* <Progress className='bg-red-500 w-[100%]' value={progress} /> */}
            <div className="px-4 py-2 rounded-md bg-[#A934F1]">
              THIS IS A QUIZ APP
            </div>
            <p className="">30:00</p>
          </div>
        </div>
      </div>
      <div className="font-bold">
        <h2 className="my-10 mx-3 text-2xl"> 1. 'Cyberspace' was coined by?</h2>
        <div className="flex flex-col bg-white w-full p-2 space-y-5">
          {/* <div className="border-2 border-purple-500 bg-[#A934F1] w-8 h-6 mr-1"></div> */}
          <div className="flex space-x-3 p-2 items-center text-black text-x1 border-2 border-purple-500">
            <div className="h-5 w-5 border-2 border-purple-500"></div>
            <div>Andrew Tannenboum</div>
          </div>
          <div className="flex space-x-3 p-2 items-center text-black text-x1 border-2 border-purple-500">
            <div className="h-5 w-5 border-2 border-purple-500"></div>
            <div>Eka Cyber</div>
          </div>
          <div className="flex space-x-3 p-2 items-center text-black text-x1 border-2 border-purple-500">
            <div className="h-5 w-5 border-2 border-purple-500"></div>
            <div>Cyber BOJ</div>
          </div>
          <div className="flex space-x-3 p-2 items-center text-black text-x1 border-2 border-purple-500">
            <div className="h-5 w-5 border-2 border-purple-500"></div>
            <div>Hay CoderCoder</div>
          </div>
        </div>
      </div>
      <div>
        <div
          style={{
            position: "fixed",
            bottom: 10,
            right: 20,
            backgroundColor: "#A934F1",
            color: "white",
            padding: "10px",
            borderRadius: "5px",
          }}
        >
          Next
        </div>
      </div>
      <div>
        <div
          style={{
            position: "fixed",
            bottom: 10,
            left: 20,
            backgroundColor: "#A934F1",
            color: "white",
            padding: "10px",
            borderRadius: "5px",
          }}
        >
          Previous
        </div>
      </div>
      <div
        className="font-bold my-10 justify-center"
        style={{ display: "flex", flexWrap: "wrap", gap: "10px" }}
      >
        {numbers.map((number) => (
          <div
            key={number}
            style={{
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
              width: "50px",
              height: "50px",
              border: "1px solid black",
              borderRadius: "5px",
            }}
          >
            {number}
          </div>
        ))}
      </div>
    </>
  );
}

// <h1 className="text-2xl font-bold mb-4">{question}</h1>
// {options.map((option, index) => (
//   <button
//     key={index}
//     className={`block w-full py-2 px-4 rounded-lg my-2 text-left ${selectedOption === index ? 'bg-purple-500 text-white' : 'bg-white text-black'}`}
//     onClick={() => setSelectedOption(index)}
//   >
//     {option}
//   </button>
// ))}
