import React from "react";
interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {}

const TextInput = (props: InputProps) => {
  return (
    <input className="h-9  border-2 w-full rounded-md p-2 px-4" {...props} />
  );
};

export default TextInput;
