"use client";
import Loader from "@/components/Loader";
import { PaginationCustom } from "@/components/Pagination";
import QuizCard from "@/components/QuizCard";
import SearchBar from "@/components/SearchBar";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Skeleton } from "@/components/ui/skeleton";
import useCategories from "@/hooks/useCategory";
import useQuiz from "@/hooks/useQuiz";
import React, { useState } from "react";

const UserDashboard = () => {
  const [category, setCategory] = useState<string>();
  const [page, setPage] = useState<number>(1);
  const [searchTerm, setSearchTerm] = useState<string>();

  const { getQuizzes } = useQuiz();
  const { getCategories } = useCategories();

  const getQuizzesQuery = getQuizzes({
    searchTerm: searchTerm,
    pageNumber: page,
    category: category,
  });

  const getCategoriesQuery = getCategories();

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
        <Select onValueChange={(val) => setCategory(val)}>
          <SelectTrigger className="w-[180px]">
            <SelectValue placeholder="Category" />
          </SelectTrigger>
          <SelectContent>
            {getCategoriesQuery.data?.map((category) => (
              <SelectItem value={category.id}>{category.title}</SelectItem>
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
          Oops, seems we don't have what you're looking for
        </h1>
      ) : (
        <div className="grid grid-flow-row-dense grid-cols-2 md:grid-cols-3 gap-4 mt-6">
          {getQuizzesQuery.data?.results.map((quiz) => (
            <QuizCard
              quizName={quiz.title}
              quizPicture="https://th.bing.com/th/id/R.f9fad17ce2f232a4e5ef2e5d91a1bdcc?rik=%2fyZXX2VBzA5chQ&pid=ImgRaw&r=0"
              quizId={quiz.id}
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
