import { Outlet } from "react-router-dom";
import SideBar from "../../../components/SideBar/SideBar";

export default function MainLayout() {
  return (
    <div style={{ display: "flex", height: "100vh" }}>
      <SideBar />
      <main style={{ flex: 1 }}>
        <Outlet />
      </main>
    </div>
  );
}
