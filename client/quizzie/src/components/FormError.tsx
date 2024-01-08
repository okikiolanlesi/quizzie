import React from "react";

function FormError({ error }: { error: string }) {
  return <div className=" text-red-600 text-xs my-1">{error}</div>;
}

export default FormError;
