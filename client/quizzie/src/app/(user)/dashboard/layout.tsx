import Protected from "@/components/Protected";
import React from "react";

const DashboardProtectedLayout = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  return <Protected>{children}</Protected>;
};

export default DashboardProtectedLayout;
