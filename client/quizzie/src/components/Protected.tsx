import useAuth from "@/hooks/useAuth";
import React, { ReactNode } from "react";

const Protected = (children: ReactNode) => {
  const { token, user } = useAuth();
  return <div>Protected</div>;
};

export default Protected;
