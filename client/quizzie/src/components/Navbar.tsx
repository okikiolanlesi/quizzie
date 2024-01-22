import Link from "next/link";
import React from "react";

function Navbar() {
  return (
    <div className=" maxWidthSection text-white sticky py-4 top-0 bg-blue">
      <div className="flex mb-10 justify-between items-center pad-section maxWidthSection">
        <div>
          <Link href="/">
            <p className="text-2xl font-bold">Quizzard</p>
          </Link>
        </div>
        <div className="hidden sm:block">
          <div className=" flex space-x-4">
            <Link href="/">
              <p>Home</p>
            </Link>

            <Link href="/about">
              <p>About</p>
            </Link>

            <Link href="#">
              <p>Contact us</p>
            </Link>
          </div>
        </div>

        <div className="space-x-4">
          <Link href={"/login"}>
            <button className="border-1 border rounded-full py-2 px-4">
              Login
            </button>
          </Link>{" "}
          <Link href={"/signup"}>
            <button className="border-1 bg-white text-deepBlue border rounded-full py-2 px-4">
              Signup
            </button>
          </Link>
        </div>
      </div>
    </div>
  );
}

export default Navbar;
