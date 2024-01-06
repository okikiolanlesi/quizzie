import Protected from "@/components/Protected";
import DashboardLayout from "@/components/layout/DashboardLayout";
import React from "react";

export default function AdminLayout({
  children, // will be a page or nested layout
}: {
  children: React.ReactNode;
}) {
  return (
    <Protected admin>
      <DashboardLayout>{children}</DashboardLayout>
    </Protected>
  );
}
