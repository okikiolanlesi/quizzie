"use client";
import Loader from "@/components/Loader";
import { PaginationCustom } from "@/components/Pagination";
import QuizCard from "@/components/QuizCard";
import WelcomeBanner from "@/components/WelcomeBanner";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import useAuth from "@/hooks/useAuth";
import useCategories from "@/hooks/useCategory";
import useQuiz from "@/hooks/useQuiz";
import React, { useState } from "react";

const UserDashboard = () => {
  const [category, setCategory] = useState<string>();
  const [page, setPage] = useState<number>(1);
  const [searchTerm, setSearchTerm] = useState<string>();
  const { user } = useAuth();
  const { GetQuizzes } = useQuiz();
  const { GetCategories } = useCategories();

  const getQuizzesQuery = GetQuizzes({
    searchTerm: searchTerm,
    pageNumber: page,
    category: category,
  });

  const getCategoriesQuery = GetCategories();

  return (
    <div>
      <div className="mb-5">
        <WelcomeBanner
          title={user?.firstName ? "Hi " + user?.firstName : ""}
          desc=" Embark on a Journey of Knowledge Exploration with Our Extensive
            Collection of Interactive Quizzes."
        />
      </div>

      <div className="flex flex-col sm:flex-row space-y-4 sm:space-y-0 sm:space-x-4 ">
        <input
          type="text"
          onChange={(e) => {
            setSearchTerm(e.target.value);
          }}
          placeholder="Search..."
          className="w-full sm:w-80 p-2 border rounded"
        />
        <Select onValueChange={(val) => setCategory(val)}>
          <SelectTrigger className="w-[180px]">
            <SelectValue placeholder="Category" />
          </SelectTrigger>
          <SelectContent>
            {getCategoriesQuery.data?.map((category) => (
              <SelectItem value={category.id} key={category.id}>
                {category.title}
              </SelectItem>
            ))}
          </SelectContent>
        </Select>
      </div>
      {getQuizzesQuery.isFetching ? (
        <div className=" min-h-52 flex justify-center items-center">
          <Loader size="lg" />
        </div>
      ) : getQuizzesQuery.data?.results.length === 0 ? (
        <h1 className="my-8">
          Oops, seems we don&apos;t have what you&apos;re looking for
        </h1>
      ) : (
        <div className="grid grid-flow-row-dense grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4 mt-6">
          {getQuizzesQuery.data?.results.map((quiz, index) => (
            <QuizCard
              key={quiz.id}
              quiz={quiz}
              backgroundGradient={
                (index + 1) % 2 == 0
                  ? "linear-gradient(180deg, #C66AFE 0%, #0F6DC2 100%)"
                  : "linear-gradient(180deg, #061F36 0%, #0F6DC2 100%)"
              }
            />
          ))}
        </div>
      )}
      <div className="my-7">
        {!getQuizzesQuery.isLoading &&
          !getQuizzesQuery.isError &&
          getQuizzesQuery.data && (
            <PaginationCustom
              onNextClick={() => {
                if (
                  Math.ceil(
                    getQuizzesQuery?.data?.totalCount! /
                      getQuizzesQuery?.data?.pageSize!
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
                getQuizzesQuery?.data?.totalCount! /
                  getQuizzesQuery?.data?.pageSize!
              )}
            />
          )}
      </div>
    </div>
  );
};

export default UserDashboard;
