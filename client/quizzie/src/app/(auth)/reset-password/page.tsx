"use client";
import { useFormik } from "formik";
import React from "react";
import FormError from "@/components/FormError";
import { object, ref, string } from "yup";
import TextInput from "@/components/TextInput";
import Link from "next/link";
import useAuth from "@/hooks/useAuth";
import { Button } from "@/components/ui/button";
import Loader from "@/components/Loader";
import { useSearchParams } from "next/navigation";

function ForgotPassword() {
  const searchParams = useSearchParams();

  const token = searchParams.get("token");

  const { resetPasswordMutation } = useAuth();

  const validationSchema = object({
    token: string(),
    newPassword: string().required("Required"),
  });

  const formik = useFormik({
    initialValues: {
      token: token?.toString()!,
      newPassword: "",
    },
    validationSchema,
    onSubmit: (values) => {
      resetPasswordMutation.mutate(values);
    },
  });

  return (
    <div className="bg-blue min-h-[100vh] overflow-y-scroll scrollbar-thin">
      <div className="pad-section bg-white rounded-l-2xl  flex justify-center overflow-y-scroll items-center min-h-[100vh]">
        <div className="w-full">
          <h1 className="text-4xl font-bold my-4">Reset Password</h1>

          <form
            className="flex flex-col space-y-3"
            onSubmit={formik.handleSubmit}
          >
            <div>
              <label htmlFor="email">New Password</label>
              <TextInput
                id="newPassword"
                name="newPassword"
                type="password"
                placeholder="New Password"
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                value={formik.values.newPassword}
              />
              {formik.touched.newPassword && formik.errors.newPassword ? (
                <FormError error={formik.errors.newPassword} />
              ) : null}
            </div>

            <Button
              disabled={!formik.isValid || resetPasswordMutation.isPending}
              className={` text-white font-bold py-2 px-3 rounded-full ${
                formik.isValid ? "bg-blue" : "bg-disabled"
              } ${resetPasswordMutation.isPending ? " bg-disabled" : null}`}
            >
              {resetPasswordMutation.isPending ? (
                <Loader size="xs" />
              ) : (
                "Submit"
              )}
            </Button>
          </form>
          <div className="flex flex-col md:flex-row space-y-3 md:space-y-0 mt-3 justify-between items-center">
            <p className="">
              Already have an account?{" "}
              <Link
                href="/signup"
                className="text-deepBlue font-bold cursor-pointer"
              >
                {" "}
                Login
              </Link>
            </p>{" "}
            <Link
              href="#"
              className=" text-deepBlue font-semibold cursor-pointer"
            ></Link>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ForgotPassword;
