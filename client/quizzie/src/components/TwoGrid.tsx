import { ReactNode } from "react";

interface ITwoGridProps {
  firstBlock: ReactNode;
  secondBlock: ReactNode;
  invert?: boolean;
}

const TwoGrid = ({
  firstBlock,
  secondBlock,
  invert = false,
}: ITwoGridProps) => {
  return (
    <>
      {!invert ? (
        <div
          className={`flex flex-col  space-y-10 items-center md:flex-row md:justify-between md:space-y-0 md:space-x-12`}
        >
          <div className="w-full h-full">{firstBlock}</div>
          <div className="w-full h-full">{secondBlock}</div>
        </div>
      ) : (
        <div
          className={`flex flex-col-reverse items-center md:flex-row md:justify-between md:space-x-12 `}
        >
          <div className="w-full h-full ">{secondBlock}</div>
          <div className="w-full h-full mb-8 md:mb-0">{firstBlock}</div>
        </div>
      )}
    </>
  );
};

export default TwoGrid;
