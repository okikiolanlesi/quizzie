"use client";
import Protected from "@/components/Protected";
import DashboardLayout from "@/components/layout/DashboardLayout";
import React from "react";

const DashboardProtectedLayout = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  return (
    <Protected>
      <DashboardLayout>{children}</DashboardLayout>
    </Protected>
  );
};

export default DashboardProtectedLayout;
