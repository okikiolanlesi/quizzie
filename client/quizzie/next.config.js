/** @type {import('next').NextConfig} */
require("dotenv").config();

const nextConfig = {
  images: {
    domains: ["img.freepik.com", "th.bing.com"],
  },
};

module.exports = nextConfig;
