// eslint-disable-next-line import/no-anonymous-default-export, react/display-name
import * as React from "react";
// eslint-disable-next-line import/no-anonymous-default-export, react/display-name
export default ({ text, icon }: { text: string; icon: React.ReactNode }) => (
  <div className="text-center mx-auto">
    {icon}
    <h6 className="font-medium">{text}</h6>
  </div>
);
