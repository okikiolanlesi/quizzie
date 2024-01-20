"use client";
import Link from "next/link";
import { IoClose } from "react-icons/io5";
import { MdLegendToggle } from "react-icons/md";

import Image from "next/image";
import Sidebar from "./Sidebar";
import { CgProfile } from "react-icons/cg";

const DashboardNavBar = (props: {
  sidebarOpen: string | boolean | undefined;
  setSidebarOpen: (arg0: boolean) => void;
}) => {
  return (
    <header className="sticky top-0 z-10 flex w-full bg-white border-b">
      <div className="flex flex-grow items-center justify-between px-4 py-4  md:px-6 2xl:px-11">
        <div className="flex items-center gap-2 sm:gap-4 lg:hidden">
          {/* <!-- Hamburger Toggle BTN --> */}
          <button
            aria-controls="sidebar"
            onClick={(e) => {
              e.stopPropagation();
              props.setSidebarOpen(!props.sidebarOpen);
            }}
            className=" block rounded-sm border border-stroke bg-white p-1.5 shadow-sm lg:hidden"
          >
            {props.sidebarOpen ? (
              <IoClose size={30} />
            ) : (
              <MdLegendToggle size={30} />
            )}
          </button>
          {/* <!-- Hamburger Toggle BTN --> */}

          <Link className="block flex-shrink-0 lg:hidden" href="/">
            {/* <Image
              width={32}
              height={32}
              src={"/images/logo/logo-icon.svg"}
              alt="Logo"
            /> */}
            <h1 className="font-bold text-2xl text-deepBlue">Quizzard</h1>
          </Link>
        </div>

        <div className="flex w-full justify-end items-center gap-3">
          <Link href={"/profile"}>
            <CgProfile size={30} />
          </Link>
        </div>
      </div>
    </header>
  );
};

export default DashboardNavBar;
