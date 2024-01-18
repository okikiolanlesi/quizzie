import React from "react";
import Countdown, { CountdownRenderProps } from "react-countdown";
import { Progress } from "./ui/progress";

interface IQuizTimerProps {
  endTime: Date;
  startTime: Date;
  onTimeEnd: () => void;
}
const Completionist = () => <span>Time is up!</span>;

const renderer = (endTime: Date, startTime: Date) => {
  const totalDuration = endTime.getTime() - startTime.getTime();
  return ({
    total: totalTimeLeft,
    hours,
    minutes,
    seconds,
    completed,
  }: CountdownRenderProps) => {
    if (completed) {
      // Render a completed state
      return <Completionist />;
    } else {
      // Render a countdown
      return (
        <span className=" min-w-52 w-full">
          <div>Duration: {totalDuration / (1000 * 60)} Minutes</div>
          <Progress
            value={(totalTimeLeft / totalDuration) * 100}
            className="w-full"
          />
          {hours}:{minutes}:{seconds}
        </span>
      );
    }
  };
};

const QuizTimer = ({ endTime, startTime, onTimeEnd }: IQuizTimerProps) => {
  return (
    <Countdown
      date={endTime}
      renderer={renderer(endTime, startTime)}
      onComplete={onTimeEnd}
    />
  );
};

export default QuizTimer;
