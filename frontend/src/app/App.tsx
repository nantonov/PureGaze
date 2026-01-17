import "./App.css";
import { router } from "./router";
import { RouterProvider } from "react-router-dom";

export default function App() {
  return (
    <>
      <h1>Inno Assessment Portal</h1>
      <RouterProvider router={router} />
    </>
  );
}
