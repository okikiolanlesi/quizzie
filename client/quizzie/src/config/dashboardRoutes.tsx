import { FiHome } from "react-icons/fi";
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
    route: "/dashboard",
    icon: <MdOutlineQuiz />,
  },
  {
    id: 3,
    label: "Profile",
    route: "/dashboard",
    icon: <CgProfile />,
  },
];

export const adminRoutes = [
  {
    id: 1,
    label: "Home",
    route: "/dashboard",
    icon: <FiHome />,
  },
  {
    id: 2,
    label: "Quizzes",
    route: "/dashboard",
    icon: <MdOutlineQuiz />,
  },
  {
    id: 3,
    label: "Categories",
    route: "/dashboard",
    icon: <CgProfile />,
  },
  {
    id: 4,
    label: "Users",
    route: "/dashboard",
    icon: <CgProfile />,
  },
];
