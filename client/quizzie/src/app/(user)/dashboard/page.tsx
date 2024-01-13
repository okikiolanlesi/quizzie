import QuizCard from "@/components/QuizCard";
import React from "react";

const UserDashboard = () => {
  return <div className="w-3/4 p-4">
    <QuizCard quizName="History" quizPicture="https://th.bing.com/th?id=OIP.Ryp5psw7KbiVsaoPEqXwiAHaJ4&w=216&h=288&c=8&rs=1&qlt=90&o=6&dpr=1.5&pid=3.1&rm=2"/>
    <QuizCard quizName="History" quizPicture="https://th.bing.com/th?id=OIP.Ryp5psw7KbiVsaoPEqXwiAHaJ4&w=216&h=288&c=8&rs=1&qlt=90&o=6&dpr=1.5&pid=3.1&rm=2"/>
    <QuizCard quizName="History" quizPicture="https://th.bing.com/th?id=OIP.Ryp5psw7KbiVsaoPEqXwiAHaJ4&w=216&h=288&c=8&rs=1&qlt=90&o=6&dpr=1.5&pid=3.1&rm=2"/>
  </div>;
};

export default UserDashboard;
