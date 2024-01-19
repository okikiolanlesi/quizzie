"use client";
import Loader from "@/components/Loader";
import { PaginationCustom } from "@/components/Pagination";
import ResultCard from "@/components/ResultCard";
import useQuizSession from "@/hooks/useQuizSession";
import { useRouter, useSearchParams } from "next/navigation";
import React, { useState } from "react";

const UserDashboard = () => {
  const router = useRouter();

  const [page, setPage] = useState<number>(1);
  const [ongoingSessionsPage, setOngoingSessionsPage] = useState<number>(1);
  const [searchTerm, setSearchTerm] = useState<string>();

  const { GetQuizSessions } = useQuizSession();

  const getFinishedQuizSessionsQuery = GetQuizSessions({
    searchTerm: searchTerm,
    pageNumber: page,
    pageSize: 10,
    status: "completed",
  });

  const getOngoingQuizSessionsQuery = GetQuizSessions({
    searchTerm: searchTerm,
    pageSize: 5,
    pageNumber: ongoingSessionsPage,
    status: "ongoing",
  });

  return (
    <div>
      {(getOngoingQuizSessionsQuery.data?.results.results.length || 0) > 0 && (
        <div>
          <h1 className="text-2xl font-bold mb-5">Ongoing Sessions</h1>
          <input
            type="text"
            onChange={(e) => {
              setSearchTerm(e.target.value);
            }}
            placeholder="Search..."
            className="w-full sm:w-80 p-2 border rounded"
          />
          <div className="grid grid-flow-row-dense grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4 mt-6">
            {getOngoingQuizSessionsQuery.data?.results.results.map(
              (ongoingQuizSession, index) => {
                return (
                  <div>
                    <ResultCard
                      quizSession={ongoingQuizSession}
                      backgroundGradient={
                        (index + 1) % 2 == 0
                          ? "linear-gradient(180deg, #C66AFE 0%, #0F6DC2 100%)"
                          : "linear-gradient(180deg, #061F36 0%, #0F6DC2 100%)"
                      }
                    />
                  </div>
                );
              }
            )}
          </div>
          <div className="my-7">
            {!getOngoingQuizSessionsQuery.isLoading &&
              !getOngoingQuizSessionsQuery.isError &&
              getOngoingQuizSessionsQuery.data && (
                <PaginationCustom
                  onNextClick={() => {
                    if (
                      Math.ceil(
                        getOngoingQuizSessionsQuery?.data?.results.totalCount! /
                          getOngoingQuizSessionsQuery?.data?.results.pageSize!
                      ) != page
                    ) {
                      setOngoingSessionsPage((prev) => prev + 1);
                    }
                  }}
                  onPrevClick={() => {
                    if (page > 1) {
                      setOngoingSessionsPage((prev) => prev - 1);
                    }
                  }}
                  onSelectPage={(page) => {
                    setOngoingSessionsPage(page);
                  }}
                  activePage={ongoingSessionsPage}
                  totalPages={Math.ceil(
                    getOngoingQuizSessionsQuery?.data?.results.totalCount! /
                      getOngoingQuizSessionsQuery?.data?.results.pageSize!
                  )}
                />
              )}
          </div>
        </div>
      )}
      <h1 className="text-2xl my-5 font-bold">Completed Sessions</h1>
      <div className="flex flex-col sm:flex-row space-y-4 sm:space-y-0 sm:space-x-4 ">
        <input
          type="text"
          onChange={(e) => {
            setSearchTerm(e.target.value);
          }}
          placeholder="Search..."
          className="w-full sm:w-80 p-2 border rounded"
        />
      </div>
      {getFinishedQuizSessionsQuery.isFetching ? (
        <div className=" min-h-52 flex justify-center items-center">
          <Loader size="lg" />
        </div>
      ) : getFinishedQuizSessionsQuery.data?.results.results.length === 0 ? (
        <h1 className="my-8">
          Oops, seems we don&apos;t have what you&apos;re looking for
        </h1>
      ) : (
        <div className="grid grid-flow-row-dense grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4 mt-6">
          {getFinishedQuizSessionsQuery.data?.results.results.map(
            (quizSession, index) => (
              <ResultCard
                quizSession={quizSession}
                backgroundGradient={
                  (index + 1) % 2 == 0
                    ? "linear-gradient(180deg, #C66AFE 0%, #0F6DC2 100%)"
                    : "linear-gradient(180deg, #061F36 0%, #0F6DC2 100%)"
                }
              />
            )
          )}
        </div>
      )}
      <div className="my-7">
        {!getFinishedQuizSessionsQuery.isLoading &&
          !getFinishedQuizSessionsQuery.isError &&
          getFinishedQuizSessionsQuery.data && (
            <PaginationCustom
              onNextClick={() => {
                if (
                  Math.ceil(
                    getFinishedQuizSessionsQuery?.data?.results.totalCount! /
                      getFinishedQuizSessionsQuery?.data?.results.pageSize!
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
                getFinishedQuizSessionsQuery?.data?.results.totalCount! /
                  getFinishedQuizSessionsQuery?.data?.results.pageSize!
              )}
            />
          )}
      </div>
    </div>
  );
};

export default UserDashboard;
