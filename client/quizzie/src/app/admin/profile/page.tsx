import React from "react";
import { Button } from "@/components/ui/button";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";

interface AdminDashboardProps {
  //
  name: string;
  username: string;
}

const AdminDashboard = () => {
  return (
    <div>
      <div className="ml-auto py-2 px-3">
        <div className="flex grid-cols-2 border-spacing-5 ml-auto">
          <div className="border border-deepBlue w-full rounded-lg justify-center py-5 px-3">
            <Avatar className="h-20 w-20">
              <AvatarImage
                src="https://thewowstyle.com/wp-content/uploads/2015/01/images-of-nature-4.jpg"
                alt="User avatar"
              />
              {/* <AvatarFallback initials="FL" /> */}
            </Avatar>
            <div className="py-2 ">
              <span className="font-bold">First Name: </span>Placeholder Name
            </div>
            <div className="py-2 ">
              <span className="font-bold">Last Name: </span>Placeholder Name
            </div>
            <div className="py-2 ">
              <span className="font-bold">Email Address: </span>Placeholder
              Email
            </div>

            {/* <Button className="bg-blue font-bold flex items-center h-10 w-full border p-2 rounded-lg">
                Edit Profile
              </Button> */}
            <div className="flex gap-4 items-center">
              <Dialog>
                <DialogTrigger asChild>
                  <Button className="bg-blue font-bold">Edit Profile</Button>
                </DialogTrigger>
                <DialogContent className="sm:max-w-[425px]">
                  <DialogHeader>
                    <DialogTitle className="font-bold">Edit profile</DialogTitle>
                    <DialogDescription>
                      Make changes to your profile here. Click save when you&apos;re
                      done.
                    </DialogDescription>
                  </DialogHeader>
                  <div className="grid gap-4 py-4">
                    <div className="grid grid-cols-4 items-center gap-4">
                      <Label htmlFor="name" className="text-right">
                        Name
                      </Label>
                      <Input id="name" value={"name"} className="col-span-3" />
                    </div>
                    <div className="grid grid-cols-4 items-center gap-4">
                      <Label htmlFor="username" className="text-right">
                        Username
                      </Label>
                      <Input
                        id="username"
                        value="@peduarte"
                        className="col-span-3"
                      />
                    </div>
                  </div>
                  <DialogFooter>
                    <Button type="submit">Save changes</Button>
                  </DialogFooter>
                </DialogContent>
              </Dialog>
              <Dialog>
                <DialogTrigger asChild>
                  <Button className="bg-blue font-bold">Change Password</Button>
                </DialogTrigger>
                <DialogContent className="sm:max-w-[425px]">
                  <DialogHeader>
                    <DialogTitle className="font-bold">Change Password</DialogTitle>
                    <DialogDescription>
                      Make changes to your password here. Click save when you&apos;re
                      done.
                    </DialogDescription>
                  </DialogHeader>
                  <div className="grid gap-4 py-4">
                    <div className="grid grid-cols-4 items-center gap-4">
                      <Label htmlFor="name" className="text-right">
                        Old Password
                      </Label>
                      <Input
                        id="name"
                        value={"Old password"}
                        className="col-span-3"
                      />
                    </div>
                    <div className="grid grid-cols-4 items-center gap-4">
                      <Label htmlFor="username" className="text-right">
                        New Password
                      </Label>
                      <Input
                        id="password"
                        value={"New password"}
                        className="col-span-3"
                      />
                    </div>
                  </div>
                  <DialogFooter>
                    <Button type="submit">Save changes</Button>
                  </DialogFooter>
                </DialogContent>
              </Dialog>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default AdminDashboard;

{
  /* <div className="ml-auto ">
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
</Button> */
}
