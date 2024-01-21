"use client";
import React, { useEffect } from "react";
import useAuth from "@/hooks/useAuth";
import { useSearchParams } from "next/navigation";
import Loader from "@/components/Loader";

function VerifyEmail() {
  const { verifyEmailMutation } = useAuth();

  const searchParams = useSearchParams();

  const userId = searchParams.get("userId");
  const token = searchParams.get("token");

  // useEffect to trigger the email verification when userId and token are present
  useEffect(() => {
    if (userId && token) {
      // Mutate the email verification
      const mutation = verifyEmailMutation.mutate({
        userId,
        token,
      });
    }
  }, [userId, token, verifyEmailMutation.mutate]);

  const RenderOutput = () => {
    if (!userId || !token || verifyEmailMutation.isError) {
      return <h3>Unable to verify your email</h3>;
    }

    if (verifyEmailMutation.isPending) {
      return (
        <div className=" min-h-52 flex flex-col justify-center items-center">
          <Loader size="lg" />
          <p>Trying to verify email...</p>
        </div>
      );
    }

    if (verifyEmailMutation.isSuccess) {
      return <div>Email Verified Successfully</div>;
    }
  };

  return (
    <div className="bg-blue min-h-[100vh] overflow-y-scroll scrollbar-thin">
      <div className="pad-section bg-white rounded-l-2xl  flex justify-center overflow-y-scroll items-center min-h-[100vh]">
        <div className="w-full">
          <h1 className="text-4xl font-bold my-4">Verify Email</h1>
          <RenderOutput />
        </div>
      </div>
    </div>
  );
}

export default VerifyEmail;
