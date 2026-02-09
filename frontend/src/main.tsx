import ReactDOM from "react-dom/client";
import React from "react";
import "./app/index.css";
import App from "./app/App";

console.log("DEBUG: Auth0 Audience:", import.meta.env.VITE_AUTH0_AUDIENCE);
console.log("DEBUG: API URL:", import.meta.env.VITE_API_URL);

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
);


