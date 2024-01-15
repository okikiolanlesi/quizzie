import React from "react";
import { Button } from "@/components/ui/button";

const AdminDashboard = () => {
  return (
    <div>
      <h1>Admin Dashboard</h1>
      <div>
        <div dir="ltr">
          AdminCard
        </div>
      </div>
      <div dir="rtl">
        <div className="border-spacing-5 w-full">
          <Button className="bg-purple font-bold h-20 w-56 border p-2 rounded-lg items-center">
            Create Quiz
          </Button>
          <Button className="bg-purple font-bold flex items-center h-20 w-56 border p-2 rounded-lg">
            View Quizzes
          </Button>
          <Button className="bg-purple font-bold flex items-center h-20 w-56 border p-2 rounded-lg">
            View Users
          </Button>
          <Button className="bg-purple font-bold flex items-center h-20 w-56 border p-2 rounded-lg">
            Update Quiz
          </Button>
        </div>
      </div>
    </div>
  );
};

export default AdminDashboard;
