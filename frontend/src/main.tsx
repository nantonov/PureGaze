import ReactDOM from "react-dom/client";
import "./app/index.css";
import App from "./app/App";
import { ThemeProvider } from "./app/providers/ThemeProvider";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <ThemeProvider>
    <App />
  </ThemeProvider>
);
