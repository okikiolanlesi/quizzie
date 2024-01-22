"use client";
import { useRouter } from "next/navigation";
import Navbar from "../../components/Navbar";

export default function About() {
  const router = useRouter();
  return (
    <section className="w-screen min-h-screen pt-10 text-white bg-blue">
      <Navbar />
      <div className="flex flex-1 justify-start items-center flex-col gap-6">
        <h5 className="font-bold text-4xl"> About The Quizzard App</h5>
        <p className="text-2xl uppercase font-light">
          One stop to put your knowledge to test
        </p>
        <p className="w-3/5 text-center text-2xl">
          The Quizzard App is a web application that allows users (admin) to
          create multiple choice questions and share them with others (users) to
          answer. The application is built with React, TypeScript, and Tailwind
          CSS on the frontend and C#, ASP .Net, and PostgreSQL on the backend.
        </p>
        <div className="flex xs:flex-col md:flex-row justify-center items-center gap-6">
          <img
            className="md:w-1/4 xs:w-2/4 shadow-lg hover:shadow-2xl"
            src="https://www.shutterstock.com/shutterstock/photos/2052894734/display_1500/stock-vector-quiz-and-question-marks-trivia-night-quiz-symbol-neon-sign-night-online-game-with-questions-2052894734.jpg"
            alt="Img1"
          />
          <img
            className="md:w-1/4 xs:w-2/4 shadow-lg hover:shadow-2xl"
            src="https://www.shutterstock.com/shutterstock/photos/1723459042/display_1500/stock-vector-quiz-neon-sign-vector-ready-for-a-quiz-neon-inscription-design-template-modern-trend-design-1723459042.jpg"
            alt="Img2"
          />
          <img
            className="md:w-1/4 xs:w-2/4 shadow-lg hover:shadow-2xl"
            src="https://www.shutterstock.com/shutterstock/photos/1135277165/display_1500/stock-vector-quiz-time-announcement-poster-neon-signboard-vector-pub-quiz-vintage-styled-neon-glowing-letters-1135277165.jpg"
            alt="Img3"
          />
        </div>
        <button
          onClick={() => router.push("/signup")}
          className="border-purple-500 border-2 p-2 rounded-md mb-10"
        >
          Get Started
        </button>
      </div>
    </section>
  );
}
