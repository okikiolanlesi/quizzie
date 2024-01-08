"use client";
import useAuth from "@/hooks/useAuth";
import { notFound, redirect } from "next/navigation";
import React, { ReactNode, useEffect } from "react";

const Protected = ({
  admin = false,
  children,
}: {
  admin?: boolean;
  children: ReactNode;
}) => {
  const { token, user } = useAuth();

  useEffect(() => {
    if (!token) {
      redirect("/login");
    }

    if (admin) {
      if (user?.role !== "Admin") {
        return notFound();
      }
    } else {
      if (user?.role !== "User") {
        return notFound();
      }
    }
  }, []);

  return <div>{children}</div>;
};

export default Protected;
