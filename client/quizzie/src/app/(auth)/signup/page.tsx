"use client";
import { useFormik } from "formik";
import React, { useEffect, useState } from "react";
import FormError from "@/components/FormError";
import { object, ref, string } from "yup";
import TextInput from "@/components/TextInput";
import Link from "next/link";
import useAuth from "@/hooks/useAuth";
import Loader from "@/components/Loader";
import ConfirmModal from "@/components/ConfirmModal";
import { useRouter } from "next/navigation";

function SignUp() {
  const [showSuccessModal, setShowSuccessModal] = useState<boolean>(false);
  const router = useRouter();
  const { signUpMutation } = useAuth();
  const validationSchema = object({
    firstName: string().required("Please enter first Name"),
    lastName: string().required("Please enter last Name"),
    email: string().email("Invalid email address").required("Required"),
    password: string()
      .min(6, "Password must be at least 6 characters")
      .required("Please enter a password"),
    confirmPassword: string()
      .oneOf([ref("password"), undefined], "Passwords must match")
      .required("Confirm Password is required"),
  });

  const formik = useFormik({
    initialValues: {
      firstName: "",
      lastName: "",
      email: "",
      password: "",
      confirmPassword: "",
    },
    validationSchema,
    onSubmit: (values) => {
      signUpMutation.mutate(values);
    },
  });

  useEffect(() => {
    if (signUpMutation.isSuccess) {
      setShowSuccessModal(true);
    }
  }, [signUpMutation.isSuccess]);

  return (
    <div className="bg-blue min-h-[100vh] overflow-y-scroll scrollbar-thin">
      <ConfirmModal
        title={"You've registered succesfully"}
        paragraph={
          "An email has been sent to you, please check and click the verification link to verify your email. After verifying your email, you can proceed to login"
        }
        onCancel={() => setShowSuccessModal(false)}
        onContinue={() => router.push("/dashboard")}
        isOpen={showSuccessModal}
      />
      <div className="pad-section bg-white rounded-l-2xl  flex justify-center overflow-y-scroll items-center min-h-[100vh]">
        <div className="w-full">
          <h1 className="text-4xl font-bold my-4">Create Account</h1>

          <form
            className="flex flex-col space-y-3"
            onSubmit={formik.handleSubmit}
          >
            <div>
              <label htmlFor="firstName">First Name</label>
              <TextInput
                id="firstName"
                name="firstName"
                type="text"
                onChange={formik.handleChange}
                placeholder="First name"
                onBlur={formik.handleBlur}
                value={formik.values.firstName}
                // className=""
              />
              {formik.touched.firstName && formik.errors.firstName ? (
                <FormError error={formik.errors.firstName} />
              ) : null}
            </div>
            <div>
              <label htmlFor="lastName">Last Name</label>
              <TextInput
                id="lastName"
                name="lastName"
                type="text"
                placeholder="Last name"
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                value={formik.values.lastName}
              />
              {formik.touched.lastName && formik.errors.lastName ? (
                <FormError error={formik.errors.lastName} />
              ) : null}
            </div>
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

            <div>
              <label htmlFor="confirmPassword">Confirm Password</label>
              <TextInput
                id="confirmPassword"
                name="confirmPassword"
                type="password"
                placeholder="Confirm Password"
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                value={formik.values.confirmPassword}
              />
              {formik.touched.confirmPassword &&
              formik.errors.confirmPassword ? (
                <FormError error={formik.errors.confirmPassword} />
              ) : null}
            </div>
            <button
              disabled={!formik.isValid || signUpMutation.isPending}
              className={` text-white font-bold py-2 px-3 rounded-full ${
                formik.isValid ? "bg-blue" : "bg-disabled"
              } ${signUpMutation.isPending ? " bg-disabled" : null}`}
            >
              {signUpMutation.isPending ? <Loader size="xs" /> : "Submit"}
            </button>
          </form>
          <div className="flex flex-col md:flex-row space-y-3 md:space-y-0 mt-3 justify-between items-center">
            <p className="">
              Already a member?{" "}
              <Link
                href="/login"
                className="text-deepBlue font-bold cursor-pointer"
              >
                {" "}
                Login
              </Link>
            </p>{" "}
            <Link
              href="/forgot-password"
              className=" text-deepBlue font-semibold cursor-pointer"
            >
              Forgot password?
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
}

export default SignUp;
