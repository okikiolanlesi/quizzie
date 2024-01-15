import React from "react";
import { Button } from "@/components/ui/button";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";

const AdminDashboard = () => {
  return (
    <div>
      <h1>Admin Dashboard</h1>
      <div dir="rtl">
        <div className="border-spacing-5 w-full">
          <div dir="ltr" className="flex border-black h-50 w-50">
            <div className="flex border-black h-50 w-50">
              <Avatar>
                <AvatarImage
                  src="https://thewowstyle.com/wp-content/uploads/2015/01/images-of-nature-4.jpg"
                  alt="User avatar"
                />
                {/* <AvatarFallback initials="FL" /> */}
              </Avatar>
            </div>
          </div>
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
