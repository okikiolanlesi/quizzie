import React from "react";
type size = "sm" | "xs" | "md" | "lg";
export default function Loader({ size = "sm" }: { size?: size }) {
  const renderSize =
    size == "sm" ? 24 : size == "xs" ? 16 : size == "md" ? 30 : 40;
  return (
    <div className="flex justify-center items-center">
      <div className="grid gap-2">
        <div className="flex items-center justify-center ">
          <div
            style={{
              width: renderSize,
              height: renderSize,
            }}
            className={` border-b-2 border-deepBlue rounded-full animate-spin`}
          ></div>
        </div>
      </div>
    </div>
  );
}
