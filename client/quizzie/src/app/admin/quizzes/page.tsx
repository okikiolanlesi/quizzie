import React from "react";
import {
  Table,
  TableBody,
  TableCaption,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Button } from "@/components/ui/button";
import { DataTable } from "@/components/DataTable";

// TODO: Add functionality to table dropdown (enable/disable, details page, ?softdelete?)

async function getData(): Promise<Quiz[]> {
  // Fetch data the API here
  return [
    {
      id: "728ed52f",
      quizName: "stories",
      createdAt: "today",
      updatedAt: "today",
      status: "active",
    },
    // ...
  ]
}

async function CreateQuiz() {
  const data = await getData()
   
  return (
    <div>
        <div className="flex flex-end">
            <Button className="bg-purple font-bold h-10 w-30 border p-2 rounded-lg items-center bottom-20">
                 Create Quiz
            </Button>
        </div>
      <div className="container mx-auto py-10">
        <DataTable columns={columns} data={data} />
      </div>
      <Table>
        <TableCaption>All Quizzes.</TableCaption>
        <TableHeader className="bg-blue">
          <TableRow>
            <TableHead className="w-[200px]">Quiz Name</TableHead>
            <TableHead>Created On</TableHead>
            <TableHead>Updated At</TableHead>
            <TableHead className="text-right">Status</TableHead>
            <TableHead className="text-right">Action</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          <TableRow>
            <TableCell className="font-medium">{}</TableCell>
            <TableCell>{}</TableCell>
            <TableCell>{}</TableCell>
            <TableCell className="text-right">{}</TableCell>
            <TableCell className="text-right">{}</TableCell>
          </TableRow>
        </TableBody>
      </Table>
    </div>
  );
};

export default CreateQuiz;
