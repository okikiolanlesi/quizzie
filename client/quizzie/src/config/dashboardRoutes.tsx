import { FiHome, FiUsers, FiFolder } from "react-icons/fi";
import { MdOutlineQuiz } from "react-icons/md";
import { CgProfile } from "react-icons/cg";

export const userRoutes = [
  {
    id: 1,
    label: "Home",
    route: "/dashboard",
    icon: <FiHome />,
  },
  {
    id: 2,
    label: "My Quizzes",
    route: "/my-quizzes",
    icon: <MdOutlineQuiz />,
  },
  {
    id: 3,
    label: "Profile",
    route: "/profile",
    icon: <CgProfile />,
  },
];

export const adminRoutes = [
  {
    id: 1,
    label: "Home",
    route: "/admin/dashboard",
    icon: <FiHome />,
  },
  {
    id: 2,
    label: "Quizzes",
    route: "/admin/quizzes",
    icon: <MdOutlineQuiz />,
  },
  {
    id: 3,
    label: "Categories",
    route: "/admin/categories",
    icon: <FiFolder />,
  },
  {
    id: 4,
    label: "Users",
    route: "/admin/users",
    icon: <FiUsers />,
  },
  {
    id: 5,
    label: "Profile",
    route: "/admin/profile",
    icon: <CgProfile />,
  },
];
