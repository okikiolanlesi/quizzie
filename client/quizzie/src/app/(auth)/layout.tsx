import Link from "next/link";
import React from "react";

export default function AuthLayout({
  children, // will be a page or nested layout
}: {
  children: React.ReactNode;
}) {
  return (
    <section className="flex ">
      <div className="min-h-[100vh] hidden sm:block bg-blue w-[80%]">
        <div className=" h-full min-h-[100vh] p-5 ">
          <Link href={"/"} className="text-3xl font-bold text-white">
            {" "}
            Quizzie
          </Link>
          <div className="  flex h-full justify-center items-center ">
            <div className="text-white max-w-2xl">
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
      </div>
      <div className="w-full h-full">{children}</div>
    </section>
  );
}
