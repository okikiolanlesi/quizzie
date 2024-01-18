export default function profile() {
  return (
    <div className="p-16">
      <div className="p-8 bg-[#1B223F] shadow mt-24">
        <div className="grid grid-cols-1 md:grid-cols-3">
          <div className="grid grid-cols-3 text-center order-last md:order-first mt-20 md:mt-0">
            <div>
              <p className="font-bold text-white text-xl">10</p>
              <p className="text-white">Quizz</p>
            </div>
            <div>
              <p className="font-bold text-white text-xl">5</p>
              <p className="text-white">Finished</p>
            </div>
            <div>
              <p className="font-bold text-white text-xl">2</p>
              <p className="text-white">Progress</p>
            </div>
          </div>
          <div className="relative">
            <div className="w-48 h-48 bg-indigo-100 mx-auto rounded-full shadow-2xl absolute inset-x-0 top-0 -mt-24 flex items-center justify-center text-purple-500">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="h-24 w-24"
                viewBox="0 0 20 20"
                fill="currentColor"
              >
                <path
                  fill-rule="evenodd"
                  d="M10 9a3 3 0 100-6 3 3 0 000 6zm-7 9a7 7 0 1114 0H3z"
                  clip-rule="evenodd"
                />
              </svg>
            </div>
          </div>

          <div className="space-x-8 flex justify-between mt-32 md:mt-0 md:justify-center">
            <button className="text-white py-2 px-4 uppercase rounded bg-purple-500 hover:bg-purple-400 shadow hover:shadow-lg font-medium transition transform hover:-translate-y-0.5">
              Update
            </button>
            <button className="text-purple-500 py-2 px-4 uppercase rounded bg-white hover:bg-purple-500 hover:text-white shadow hover:shadow-lg font-medium transition transform hover:-translate-y-0.5">
              Message
            </button>
          </div>
        </div>

        <div className="mt-20 text-center border-b pb-12">
          <h1 className="text-4xl font-medium text-white">
            Michael Patrick, <span className="font-light text-white">27</span>
          </h1>
          <p className="font-light text-white mt-3">Lagos, Nigeria</p>

          <p className="mt-8 text-white">Solution Manager - Creative Officer</p>
          <p className="mt-2 text-white">patrick222@gmail.com</p>
        </div>

        <div className="mt-12 flex flex-col justify-center">
          <p className="text-white text-center font-bold lg:px-16 py-6">
            Profile Information
          </p>
          {/* <div className="w-full max-w-xs"> */}
          <form className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
            <div className="mb-4">
              <label
                className="block text-gray-700 text-sm font-bold mb-2"
                htmlFor="username"
              >
                Fullname
              </label>
              <input
                className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                id="fullname"
                type="text"
                placeholder="Michael Patrick"
              ></input>
            </div>
            <div className="mb-4">
              <label
                className="block text-gray-700 text-sm font-bold mb-2"
                htmlFor="email"
              >
                Email
              </label>
              <input
                className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                id="email"
                type="text"
                placeholder="patrick222@gmail.com"
              ></input>
            </div>
            <div className="mb-4">
              <label
                className="block text-gray-700 text-sm font-bold mb-2"
                htmlFor="Phone Number"
              >
                Phone Number
              </label>
              <input
                className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                id="phone number"
                type="text"
                placeholder="08023457892"
              ></input>
            </div>
            <div className="mb-4">
              <label
                className="block text-gray-700 text-sm font-bold mb-2"
                htmlFor="phone number"
              >
                Address
              </label>
              <input
                className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                id="address"
                type="text"
                placeholder="12,Ajao Estate, Lagos Nigeria"
              ></input>
            </div>

            <div className="flex items-center justify-between">
              <button
                className=" w-full bg-purple-500 hover:bg-[#1B223F] text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                type="button"
              >
                Save Changes
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
    // </div>
  );
}
