import { FiHome } from "react-icons/fi";
import { MdOutlineQuiz } from "react-icons/md";
import { CgProfile } from "react-icons/cg";

export const userRoutes = [
  {
    label: "Home",
    route: "/dashboard",
    icon: <FiHome />,
  },
  {
    label: "My Quizzes",
    route: "/dashboard",
    icon: <MdOutlineQuiz />,
  },
  {
    label: "Profile",
    route: "/dashboard",
    icon: <CgProfile />,
  },
];

export const adminRoutes = [
  {
    label: "Home",
    route: "/dashboard",
    icon: <FiHome />,
  },
  {
    label: "Quizzes",
    route: "/dashboard",
    icon: <MdOutlineQuiz />,
  },
  {
    label: "Categories",
    route: "/dashboard",
    icon: <CgProfile />,
  },
  {
    label: "Users",
    route: "/dashboard",
    icon: <CgProfile />,
  },
];
