import type { Config } from "tailwindcss";

const config: Config = {
  darkMode: ["class"],
  content: [
    "./src/pages/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/components/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/app/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/**/*.{ts,tsx}",
  ],
  theme: {
    extend: {
      container: {
        center: true,
        padding: "2rem",
        screens: {
          "2xl": "1400px",
        },
      },
      colors: {
        primary: "#1B223F",
      },
      textColor: {
        primary: "#1D2739",
        secondary: "#616161",
        deepBlue: "#1B223F",
        faded: "#E0E0E0",
      },
      backgroundColor: {
        primary: "#FFFFFF",
        blue: "#1B223F",
        purple: "#A934F1",
        secondary: "#006F98",
        disabled: "#E0E0E0",
      },
      borderColor: {
        deepBlue: "#1B223F",
      },
      fontFamily: {
        heading: ["Urbanist", "sans-serif"],
        body: ["Urbanist", "sans-serif"],
      },
      boxShadow: {
        card: "0px 0px 22.2264px rgba(0, 0, 0, 0.1);",
        navBar: "0px 4px 34px rgba(0, 0, 0, 0.1)",
      },
      backgroundImage: {
        "hero-bg": "url('/images/first-bg.png')",
      },
      screens: {
        slim: "330px",
        md1: "700px",
        md2: "1120px",
      },
      keyframes: {
        "accordion-down": {
          from: { height: "0" },
          to: { height: "var(--radix-accordion-content-height)" },
        },
        "accordion-up": {
          from: { height: "var(--radix-accordion-content-height)" },
          to: { height: "0" },
        },
      },
      animation: {
        "accordion-down": "accordion-down 0.2s ease-out",
        "accordion-up": "accordion-up 0.2s ease-out",
      },
    },
  },
  plugins: [require("tailwindcss-animate")],
};
export default config;
