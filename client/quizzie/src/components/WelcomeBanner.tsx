"use client";
import React from "react";

const WelcomeBanner = ({ title, desc }: { title: string; desc: string }) => {
  return (
    <div
      style={{
        borderRadius: "20px",
        background: "linear-gradient(180deg, #C66AFF 8.41%, #072847 100%)",
        boxShadow: "0px 4px 4px 0px rgba(0, 0, 0, 0.25)",
      }}
      className="py-5 sm:py-10 px-8  flex justify-between"
    >
      <div className="flex flex-col space-y-4">
        <h1 className=" font-bold text-2xl text-white">{title}</h1>
        <p className="text-white max-w-[400px]">{desc}</p>
      </div>
    </div>
  );
};

export default WelcomeBanner;
