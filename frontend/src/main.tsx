import ReactDOM from "react-dom/client";
import React, { Suspense } from "react";
import "./app/index.css";
import App from "./app/App";
import "@/shared/config/i18n.ts";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <Suspense fallback="Loading...">
      <App />
    </Suspense>
  </React.StrictMode>,
);
