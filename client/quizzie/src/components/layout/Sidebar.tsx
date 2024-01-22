"use client";
import React, { ReactNode, useEffect, useRef, useState } from "react";
import Link from "next/link";
import { redirect, usePathname } from "next/navigation";
import Image from "next/image";
import { useAuthState } from "@/store/authStore";
import { adminRoutes, userRoutes } from "@/config/dashboardRoutes";

interface SidebarProps {
  sidebarOpen: boolean;
  setSidebarOpen: (arg: boolean) => void;
}

const Sidebar = ({ sidebarOpen, setSidebarOpen }: SidebarProps) => {
  const { user, clearAuth } = useAuthState((state) => state);

  const pathname = usePathname();

  const trigger = useRef<any>(null);
  const sidebar = useRef<any>(null);

  let storedSidebarExpanded = "true";
  const [sidebarExpanded, setSidebarExpanded] = useState(
    storedSidebarExpanded === null ? false : storedSidebarExpanded === "true"
  );

  const [links, setLinks] = useState<
    {
      id: number;
      label: string;
      route: string;
      icon: ReactNode;
    }[]
  >([]);

  // close on click outside
  useEffect(() => {
    const clickHandler = ({ target }: MouseEvent) => {
      if (!sidebar.current || !trigger.current) return;
      if (
        !sidebarOpen ||
        sidebar.current.contains(target) ||
        trigger.current.contains(target)
      )
        return;
      setSidebarOpen(false);
    };
    document.addEventListener("click", clickHandler);
    return () => document.removeEventListener("click", clickHandler);
  });

  // close if the esc key is pressed
  useEffect(() => {
    const keyHandler = ({ key }: KeyboardEvent) => {
      if (!sidebarOpen || key !== "Escape") return;
      setSidebarOpen(false);
    };
    document.addEventListener("keydown", keyHandler);
    return () => document.removeEventListener("keydown", keyHandler);
  });

  useEffect(() => {
    localStorage.setItem("sidebar-expanded", sidebarExpanded.toString());
    if (sidebarExpanded) {
      document.querySelector("body")?.classList.add("sidebar-expanded");
    } else {
      document.querySelector("body")?.classList.remove("sidebar-expanded");
    }
  }, [sidebarExpanded]);

  useEffect(() => {
    setLinks(user?.role === "Admin" ? adminRoutes : userRoutes);
  }, [user]);

  return (
    <aside
      ref={sidebar}
      className={`absolute left-0 top-0 z-50 flex h-screen w-72.5 flex-col overflow-y-hidden bg-blue duration-300 ease-linear  lg:static lg:translate-x-0 ${
        sidebarOpen ? "translate-x-0" : "-translate-x-full"
      }`}
    >
      <div className="flex items-center justify-between gap-2 px-6 py-5.5 lg:py-6.5 mt-8">
        <Link href={user?.role === "Admin" ? "/admin/dashboard" : "/dashboard"}>
          <p className="text-2xl font-bold text-white">Quizzard</p>
        </Link>

        <button
          ref={trigger}
          onClick={() => setSidebarOpen(!sidebarOpen)}
          aria-controls="sidebar"
          aria-expanded={sidebarOpen}
          className="block lg:hidden"
        >
          <svg
            className="fill-current"
            width="20"
            height="18"
            viewBox="0 0 20 18"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M19 8.175H2.98748L9.36248 1.6875C9.69998 1.35 9.69998 0.825 9.36248 0.4875C9.02498 0.15 8.49998 0.15 8.16248 0.4875L0.399976 8.3625C0.0624756 8.7 0.0624756 9.225 0.399976 9.5625L8.16248 17.4375C8.31248 17.5875 8.53748 17.7 8.76248 17.7C8.98748 17.7 9.17498 17.625 9.36248 17.475C9.69998 17.1375 9.69998 16.6125 9.36248 16.275L3.02498 9.8625H19C19.45 9.8625 19.825 9.4875 19.825 9.0375C19.825 8.55 19.45 8.175 19 8.175Z"
              fill="white"
            />
          </svg>
        </button>
      </div>

      <div className="no-scrollbar flex flex-col overflow-y-auto duration-300 ease-linear  h-full">
        <nav className="mt-5 py-4 px-4 lg:mt-9 lg:px-6 flex flex-col justify-between h-full">
          <div>
            <h3 className="mb-4 ml-4 text-sm font-semibold text-white">MENU</h3>

            <ul className="mb-6 flex flex-col gap-1.5">
              {links.map((route: any) => {
                return (
                  <li key={route.id}>
                    <Link
                      href={route.route}
                      className={`group relative flex items-center gap-2.5 rounded-sm  py-2 px-4 font-medium text-white duration-300 ease-in-out hover:underline"
                      }`}
                    >
                      {route.icon}
                      <span
                        className={`${
                          pathname === route.route
                            ? "border-b-2 pb-2 border-white "
                            : null
                        }`}
                      >
                        {route.label}
                      </span>
                    </Link>
                  </li>
                );
              })}
            </ul>
          </div>

          <button
            onClick={() => {
              clearAuth();
              window.location.href = "/login";
            }}
            className="text-white text-left py-2 px-3 w-full bg-red-500 rounded-lg"
          >
            Logout
          </button>
        </nav>
      </div>
    </aside>
  );
};

export default Sidebar;
