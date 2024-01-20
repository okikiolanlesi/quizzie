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

function Login() {
  const { loginMutation } = useAuth();

  const validationSchema = object({
    email: string().email("Invalid email address").required("Required"),
    password: string().required("Please enter a password"),
  });

  const formik = useFormik({
    initialValues: {
      email: "",
      password: "",
    },
    validationSchema,
    onSubmit: (values) => {
      loginMutation.mutate(values);
    },
  });

  return (
    <div className="bg-blue min-h-[100vh] overflow-y-scroll scrollbar-thin">
      <div className="pad-section bg-white rounded-l-2xl  flex justify-center overflow-y-scroll items-center min-h-[100vh]">
        <div className="w-full">
          <h1 className="text-4xl font-bold my-4">Login</h1>

          <form
            className="flex flex-col space-y-3"
            onSubmit={formik.handleSubmit}
          >
            <div>
              <label htmlFor="email">Email Address</label>
              <TextInput
                id="email"
                name="email"
                type="email"
                placeholder="Email"
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                value={formik.values.email}
              />
              {formik.touched.email && formik.errors.email ? (
                <FormError error={formik.errors.email} />
              ) : null}
            </div>

            <div>
              <label htmlFor="password">Password</label>
              <TextInput
                id="password"
                name="password"
                type="password"
                placeholder="Password"
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                value={formik.values.password}
              />
              {formik.touched.password && formik.errors.password ? (
                <FormError error={formik.errors.password} />
              ) : null}
            </div>
            <Button
              disabled={!formik.isValid || loginMutation.isPending}
              className={` text-white font-bold py-2 px-3 rounded-full ${
                formik.isValid ? "bg-blue" : "bg-disabled"
              } ${loginMutation.isPending ? " bg-disabled" : null}`}
            >
              {loginMutation.isPending ? <Loader size="xs" /> : "Submit"}
            </Button>
          </form>
          <p className="mt-3">
            Don&apos;t have an account?{" "}
            <Link
              href="/signup"
              className="text-primary font-bold cursor-pointer"
            >
              {" "}
              Sign up
            </Link>
          </p>
        </div>
      </div>
    </div>
  );
}

export default Login;
