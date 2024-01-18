"use client";
import { ColumnDef } from "@tanstack/react-table";
import { Button } from "@/components/ui/button"
import { ArrowUpDown, MoreHorizontal } from "lucide-react"
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { networkInterfaces } from "os";


 export interface Quiz {
  id: string; // This is the unique ID of the row. Possibly update
  quizName: string;
  createdAt: string;
  updatedAt: string;
  status: "enabled" | "disabled";
  // action: string; // should be clickable
};

const Columns: ColumnDef<Quiz>[] = [
  {
    accessorKey: "quizName",
    header: ({ column }) => {
        return (
          <Button
            variant="ghost"
            onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          >
            Quiz Name
            <ArrowUpDown className="ml-2 h-4 w-4" />
          </Button>
        )
      },
  },
  {
    accessorKey: "createdAt",
    header: "Created At",
  },
  {
    accessorKey: "updatedAt",
    header: "Updated At",
  },
    {
        accessorKey: "status",
        header: "Status",
    },
    // {
    //     accessorKey: "action",
    //     header: "Action",
    // },
    {
        id: "actions",
        cell: ({ row }) => {
          const quiz = row.original
     
          return (
            <DropdownMenu>
              <DropdownMenuTrigger asChild>
                <Button variant="ghost" className="h-8 w-8 p-0">
                  <span className="sr-only">Open menu</span>
                  <MoreHorizontal className="h-4 w-4" />
                </Button>
              </DropdownMenuTrigger>
              <DropdownMenuContent align="end">
                <DropdownMenuLabel>Actions</DropdownMenuLabel>
                <DropdownMenuItem
                  onClick={() => navigator.clipboard.writeText(quiz.id)}
                >
                  Copy Quiz ID
                </DropdownMenuItem>
                <DropdownMenuSeparator />
                <DropdownMenuItem>Quiz Details</DropdownMenuItem>
                <DropdownMenuItem>Disable/Enable Quiz</DropdownMenuItem>
              </DropdownMenuContent>
            </DropdownMenu>
          )
        },
      },
];

export default Columns;