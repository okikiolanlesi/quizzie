import React from "react";

export default function AuthLayout({
  children, // will be a page or nested layout
}: {
  children: React.ReactNode;
}) {
  return (
    <section className="flex ">
      <div className="w-full  min-h-[100vh] hidden sm:block bg-blue w-[80%]">
        <div className="  flex h-full min-h-[100vh] justify-center items-center ">
          <div className="text-white p-5 max-w-2xl">
            <h2 className="text-5xl font-bold">
              Unleash Your Inner Wizard of Wisdom
            </h2>
            <p>
              Embark on a Journey of Knowledge Exploration with Our Extensive
              Collection of Interactive Quizzes.
            </p>
          </div>
        </div>
      </div>
      <div className="w-full h-full">{children}</div>
    </section>
  );
}
