/* eslint-disable react/jsx-no-undef */
import { Button } from "@/components/ui/button";
import * as React from "react";
// import { useForm } from "react-hook-form";
import IconEmail from "@/components/icon/IconEmail";
import IconInfo from "@/components/icon/IconInfo";
import IconPhone from "@/components/icon/IconPhone";
// import Image from 'next/image'
export default function ContactUs() {
  // const {control,handleSubmit,
  //   formState:{errors}} = useForm({
  //   defaultValues: {
  //     name: "",
  //     email: "",
  //     message: "",

  //   },
  // });
  return (
    <div className="xl:container mx-auto mb-32">
      <div
        className="flex justify-center bg-gradient-to-r from-purple-500 to-[#1B223F] radial-gradient-circle"
        style={{
          background:
            "radial-gradient(circle,rgba(0,0,0,1) 40% rgba(252,70,107,1) 100%)",
          height: "200px",
        }}
      >
        <h1 className=" font-bold text-5xl sm:text-7xl text-white uppercase pt-12">
          Contact us
        </h1>
      </div>
      <div className="px-4 sm:w-2/3 lg:w-1/2 mx-auto">
        <div className="rounded-lg shadow-lg bg-white PX-12 -mt-12 py-10 md:py-12 px-4 px-6">
          <div className="grid grid-cols-2 gap-x-6 mb-12 mx-auto">
            <IconInfo icon={<IconEmail />} text="contact@us" />
            <IconInfo icon={<IconPhone />} text="+234 810 031 7369" />
          </div>
          <div>
            <form>
              <div className="mb-4">
                <label
                  className="block text-gray-700 text-sm font-bold mb-2"
                  htmlFor="username"
                >
                  Name
                </label>
                <input
                  className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                  id="username"
                  type="text"
                  placeholder="Enter name here..."
                ></input>
              </div>
              <div className="mb-4">
                <label
                  className="block text-gray-700 text-sm font-bold mb-2"
                  htmlFor="username"
                >
                  Email
                </label>
                <input
                  className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                  id="Email"
                  type="text"
                  placeholder="Enter email here..."
                ></input>
              </div>

              <div className="mb-4">
                <label
                  className="block text-gray-700 text-sm font-bold mb-2"
                  htmlFor="large-input"
                >
                  Message
                </label>
                <textarea
                  className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                  id="large-input"
                  placeholder="Enter Message Here..."
                ></textarea>
              </div>
              <div className="flex items-center justify-between">
                <button
                  className=" w-full bg-[#1B223F] hover:bg-purple-500 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                  type="button"
                >
                  SEND
                </button>
              </div>
            </form>
          </div>
          {/* <controller
              name="name"
              control={control}
              rules={{required:true}}
              render={({field}) => (
                <formElement
                 type="text"
                  label="Name"
                   placeholder="Enter name here..."
                    fieldRef={field}
                     hasError={errors.name? .type === 'required'}
              )} */}
        </div>
      </div>
    </div>
  );
}
