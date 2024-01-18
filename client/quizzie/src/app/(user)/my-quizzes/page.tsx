"use client";
import Loader from "@/components/Loader";
import { PaginationCustom } from "@/components/Pagination";
import QuizCard from "@/components/QuizCard";
import ResultCard from "@/components/ResultCard";
import useQuizSession from "@/hooks/useQuizSession";
import React, { useState } from "react";

const UserDashboard = () => {
  const [status, setStatus] = useState<"ongoing" | "completed" | undefined>();
  const [page, setPage] = useState<number>(1);
  const [searchTerm, setSearchTerm] = useState<string>();

  const { GetQuizSessions } = useQuizSession();

  const getQuizSessionsQuery = GetQuizSessions({
    searchTerm: searchTerm,
    pageNumber: page,
    status: status,
  });

  return (
    <div>
      <div className="flex flex-col sm:flex-row space-y-4 sm:space-y-0 sm:space-x-4 ">
        <input
          type="text"
          onChange={(e) => {
            setSearchTerm(e.target.value);
          }}
          placeholder="Search..."
          className="w-full sm:w-80 p-2 border rounded"
        />
        {/* <Select onValueChange={(val) => setCategory(val)}>
          <SelectTrigger className="w-[180px]">
            <SelectValue placeholder="Category" />
          </SelectTrigger>
          <SelectContent>
              <SelectItem value={category.id} key={category.id}>
                Ongoing
              </SelectItem>
              <SelectItem value={category.id} key={category.id}>
                {category.title}
              </SelectItem>
          </SelectContent>
        </Select> */}
      </div>
      {getQuizSessionsQuery.isFetching ? (
        <div className=" min-h-52 flex justify-center items-center">
          <Loader size="lg" />
        </div>
      ) : getQuizSessionsQuery.data?.results.results.length === 0 ? (
        <h1 className="my-8">
          Oops, seems we don&apos;t have what you&apos;re looking for
        </h1>
      ) : (
        <div className="grid grid-flow-row-dense grid-cols-2 md:grid-cols-3 gap-4 mt-6">
          {getQuizSessionsQuery.data?.results.results.map((quizSession) => (
            <ResultCard quizSession={quizSession} />
          ))}
        </div>
      )}
      <div className="my-7">
        {!getQuizSessionsQuery.isLoading &&
          !getQuizSessionsQuery.isError &&
          getQuizSessionsQuery.data && (
            <PaginationCustom
              onNextClick={() => {
                if (
                  Math.ceil(
                    getQuizSessionsQuery?.data?.results.totalCount! /
                      getQuizSessionsQuery?.data?.results.pageSize!
                  ) != page
                ) {
                  setPage((prev) => prev + 1);
                }
              }}
              onPrevClick={() => {
                if (page > 1) {
                  setPage((prev) => prev - 1);
                }
              }}
              onSelectPage={(page) => {
                setPage(page);
              }}
              activePage={page}
              totalPages={Math.ceil(
                getQuizSessionsQuery?.data?.results.totalCount! /
                  getQuizSessionsQuery?.data?.results.pageSize!
              )}
            />
          )}
      </div>
    </div>
  );
};

export default UserDashboard;
