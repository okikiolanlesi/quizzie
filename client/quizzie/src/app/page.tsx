import Navbar from "@/components/Navbar";
import TwoGrid from "@/components/TwoGrid";
import Image from "next/image";
import Link from "next/link";

export default function Home() {
  return (
    <main className="bg-blue min-h-[100vh] w-full py-8">
      <Navbar />
      <div className="pad-section maxWidthSection">
        <TwoGrid
          firstBlock={
            <div className="text-white">
              <h1 className="text-4xl font-bold">
                Welcome to the magical realm of knowledge exploration!
              </h1>
              <p className="mt-4">
                Embark on a Journey of Knowledge Exploration with Our Extensive
                Collection of Interactive Quizzes.
              </p>
              <Link href={"/signup"}>
                <button className="bg-purple rounded-full py-2 px-4 mt-5">
                  Get started
                </button>
              </Link>
            </div>
          }
          secondBlock={
            <div className="w-full flex justify-center min-h-full h-full  items-center">
              <Image
                alt={"Product image"}
                className="w-full max-w-96 object-cover"
                src={"/images/hero-image-1.svg"}
                width={100}
                height={100}
              />
            </div>
          }
        />
      </div>
    </main>
  );
}
