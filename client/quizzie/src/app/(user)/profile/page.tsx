"use client";
import React from "react";
import { Button } from "@/components/ui/button";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import useAuth from "@/hooks/useAuth";
import TextInput from "@/components/TextInput";
import FormError from "@/components/FormError";
import { object, string } from "yup";
import { useFormik } from "formik";

const UserProfile = () => {
  const { user } = useAuth();

  const { editProfileMutation, changePasswordMutation } = useAuth();

  const editProfileValidationSchema = object({
    firstName: string().required("Please enter first name"),
    lastName: string().required("Please enter last name"),
  });

  const editProfileFormik = useFormik({
    initialValues: {
      firstName: user?.firstName!,
      lastName: user?.lastName!,
    },
    validationSchema: editProfileValidationSchema,
    onSubmit: (values: { firstName: string; lastName: string }) => {
      editProfileMutation.mutate({ ...values, userId: user?.id! });
    },
  });

  const changePasswordValidationSchema = object({
    oldPassword: string().required("Please enter Old Password"),
    newPassword: string().required("Please enter New Password"),
  });

  const changePasswordFormik = useFormik({
    initialValues: {
      oldPassword: "",
      newPassword: "",
    },
    validationSchema: changePasswordValidationSchema,
    onSubmit: (values) => {
      changePasswordMutation.mutate(values);
    },
  });
  return (
    <div>
      <div className="ml-auto py-2 px-3">
        <div className="flex grid-cols-2 border-spacing-5 ml-auto">
          <div className="border border-deepBlue w-full rounded-lg justify-center py-5 px-3">
            <Avatar className="h-20 w-20">
              <AvatarImage
                src="https://th.bing.com/th?id=OIP.buxKr5Wnd1kTlg1Zm0fuyAHaHa&w=250&h=250&c=8&rs=1&qlt=90&o=6&dpr=1.4&pid=3.1&rm=2"
                alt="User avatar"
              />
              {/* <AvatarFallback initials="FL" /> */}
            </Avatar>
            <div className="py-2 ">
              <span className="font-bold">First Name: </span>
              {user?.firstName}
            </div>
            <div className="py-2 ">
              <span className="font-bold">Last Name: </span>
              {user?.lastName}
            </div>
            <div className="py-2 ">
              <span className="font-bold">Email Address: </span>
              {user?.email}
            </div>

            {/* <Button className="bg-blue font-bold flex items-center h-10 w-full border p-2 rounded-lg">
                Edit Profile
              </Button> */}
            <div className="flex gap-4 items-center">
              <Dialog>
                <DialogTrigger asChild>
                  <Button className="bg-blue font-bold">Edit Profile</Button>
                </DialogTrigger>
                <DialogContent className="sm:max-w-[425px]">
                  <DialogHeader>
                    <DialogTitle className="font-bold">
                      Edit profile
                    </DialogTitle>
                    <DialogDescription>
                      Make changes to your profile here. Click save when
                      you&apos;re done.
                    </DialogDescription>
                  </DialogHeader>
                  <form
                    className="flex flex-col space-y-3"
                    onSubmit={editProfileFormik.handleSubmit}
                  >
                    <div>
                      <label htmlFor="email">First Name</label>
                      <TextInput
                        id="firstName"
                        name="firstName"
                        type="firstName"
                        placeholder="First Name"
                        onChange={editProfileFormik.handleChange}
                        onBlur={editProfileFormik.handleBlur}
                        value={editProfileFormik.values.firstName}
                      />
                      {editProfileFormik.touched.firstName &&
                      editProfileFormik.errors.firstName ? (
                        <FormError error={editProfileFormik.errors.firstName} />
                      ) : null}
                    </div>
                    <div>
                      <label htmlFor="email">Last Name</label>
                      <TextInput
                        id="lastName"
                        name="lastName"
                        type="lastName"
                        placeholder="Last Name"
                        onChange={editProfileFormik.handleChange}
                        onBlur={editProfileFormik.handleBlur}
                        value={editProfileFormik.values.lastName}
                      />
                      {editProfileFormik.touched.lastName &&
                      editProfileFormik.errors.lastName ? (
                        <FormError error={editProfileFormik.errors.lastName} />
                      ) : null}
                    </div>
                    <Button className="" type="submit">
                      Save changes
                    </Button>
                  </form>
                  <DialogFooter></DialogFooter>
                </DialogContent>
              </Dialog>
              <Dialog>
                <DialogTrigger asChild>
                  <Button className="bg-blue font-bold">Change Password</Button>
                </DialogTrigger>
                <DialogContent className="sm:max-w-[425px]">
                  <DialogHeader>
                    <DialogTitle className="font-bold">
                      Change Password
                    </DialogTitle>
                    <DialogDescription>
                      Make changes to your password here. Click save when
                      you&apos;re done.
                    </DialogDescription>
                  </DialogHeader>
                  <form
                    className="flex flex-col space-y-3"
                    onSubmit={changePasswordFormik.handleSubmit}
                  >
                    <div>
                      <label htmlFor="email">Old Password</label>
                      <TextInput
                        id="oldPassword"
                        name="oldPassword"
                        type="password"
                        placeholder="Old Password"
                        onChange={changePasswordFormik.handleChange}
                        onBlur={changePasswordFormik.handleBlur}
                        value={changePasswordFormik.values.oldPassword}
                      />
                      {changePasswordFormik.touched.oldPassword &&
                      changePasswordFormik.errors.oldPassword ? (
                        <FormError
                          error={changePasswordFormik.errors.oldPassword}
                        />
                      ) : null}
                    </div>
                    <div>
                      <label htmlFor="email">New Password</label>
                      <TextInput
                        id="newPassword"
                        name="newPassword"
                        type="password"
                        placeholder="New Password"
                        onChange={changePasswordFormik.handleChange}
                        onBlur={changePasswordFormik.handleBlur}
                        value={changePasswordFormik.values.newPassword}
                      />
                      {changePasswordFormik.touched.newPassword &&
                      changePasswordFormik.errors.newPassword ? (
                        <FormError
                          error={changePasswordFormik.errors.newPassword}
                        />
                      ) : null}
                    </div>
                    <Button className="" type="submit">
                      Save changes
                    </Button>
                  </form>
                  <DialogFooter></DialogFooter>
                </DialogContent>
              </Dialog>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default UserProfile;
