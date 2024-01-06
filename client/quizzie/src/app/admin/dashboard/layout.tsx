import Protected from "@/components/Protected";
import React from "react";

export default function AdminLayout({
  children, // will be a page or nested layout
}: {
  children: React.ReactNode;
}) {
  return (
    <Protected admin>
      <section className=" ">{children}</section>
    </Protected>
  );
}
