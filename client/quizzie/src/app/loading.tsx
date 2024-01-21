import Loader from "@/components/Loader";
import React from "react";

const Loading = () => {
  return (
    <div className=" min-h-52 flex justify-center items-center">
      <Loader size="lg" />
    </div>
  );
};

export default Loading;
