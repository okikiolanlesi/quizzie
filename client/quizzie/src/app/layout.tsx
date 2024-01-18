import { Metadata } from "next";
import "./globals.css";
import Providers from "@/utils/Provider";
import { Toaster } from "react-hot-toast";

export const metadata: Metadata = {
  title: "Quizzard",
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body>
        <Providers>{children}</Providers>
        <Toaster />
      </body>
    </html>
  );
}
