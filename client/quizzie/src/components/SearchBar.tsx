import React from "react";

function SearchBar () {
    return (
        <div>
            <div className="mb-4">
            <input
              type="text"
              placeholder="Search..."
              className="w-full p-2 border rounded"
            />
          </div>
        </div>
    )
};

export default SearchBar;