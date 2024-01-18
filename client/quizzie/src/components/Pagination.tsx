import {
  Pagination,
  PaginationContent,
  PaginationEllipsis,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination";
import { useMemo } from "react";

interface IPaginationCustom {
  onPrevClick: () => void;
  onNextClick: () => void;
  onSelectPage: (page: number) => void;
  activePage: number;
  totalPages: number;
}
export function PaginationCustom({
  onPrevClick,
  onNextClick,
  onSelectPage,
  activePage,
  totalPages,
}: IPaginationCustom) {
  const getPages = () => {
    const pages = [];

    for (let i = activePage - 2; i < activePage + 2; i++) {
      if (i > 1 && i <= totalPages) {
        pages.push(i);
      }
    }

    return pages;
  };

  const pages = useMemo(getPages, [activePage, totalPages]);

  return (
    <Pagination>
      <PaginationContent className="flex-wrap">
        <PaginationPrevious
          className={`${activePage == 1 ? "text-faded" : null}`}
          onClick={onPrevClick}
          href="#"
        />
        <PaginationItem onClick={() => onSelectPage(1)}>
          <PaginationLink isActive={activePage == 1} href="#">
            1
          </PaginationLink>
        </PaginationItem>
        {pages[0] !== 2 && pages[0] !== undefined && (
          <PaginationItem>
            <PaginationEllipsis />
          </PaginationItem>
        )}
        {pages.map((page) => (
          <PaginationItem key={page} onClick={() => onSelectPage(page)}>
            <PaginationLink isActive={activePage == page} href="#">
              {page}
            </PaginationLink>
          </PaginationItem>
        ))}
        {pages.length != 0 && pages[pages.length - 1] !== totalPages && (
          <PaginationItem>
            <PaginationEllipsis />
          </PaginationItem>
        )}
        <PaginationNext
          className={`${activePage == totalPages ? "text-faded" : null}`}
          onClick={onNextClick}
          href="#"
        />
      </PaginationContent>
    </Pagination>
  );
}
