import React from "react";
import { Button } from "@/components/ui/button";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";

const AdminDashboard = () => {
  return (
    <div>
      <div className="ml-auto py-2 px-3">
        <div className="flex grid-cols-2 border-spacing-5 ml-auto">
            <div className="bg-purple w-full justify-between rounded-lg py-5 px-2">
              <Avatar className="h-20 w-20">
                <AvatarImage
                  src="https://thewowstyle.com/wp-content/uploads/2015/01/images-of-nature-4.jpg"
                  alt="User avatar"
                />
                {/* <AvatarFallback initials="FL" /> */}
              </Avatar>
              <div>
                Placeholder Name
              </div>
              <div>
                Some email address
              </div>
              <div>
                Some role
              </div>
              <Button className="bg-blue font-bold flex items-center h-10 w-full border p-2 rounded-lg">
                Edit Profile
              </Button>
            </div>
          <div className="ml-auto ">
            <Button className="bg-blue font-bold h-20 w-56 border p-2 rounded-lg items-center">
              Create Quiz
            </Button>
            <Button className="bg-blue font-bold flex items-center h-20 w-56 border p-2 rounded-lg">
              View Quizzes
            </Button>
            <Button className="bg-blue font-bold flex items-center h-20 w-56 border p-2 rounded-lg">
              View Users
            </Button>
            <Button className="bg-blue font-bold flex items-center h-20 w-56 border p-2 rounded-lg">
              Update Quiz
          </Button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default AdminDashboard;
