import { Outlet } from "react-router-dom";
import SideBar from "../../components/SideBar/SideBar";
import styles from "./MainLayout.module.css";

export default function MainLayout() {
  return (
    <div className={styles.layout}>
      <SideBar />
      <main className={styles.content}>
        <Outlet />
      </main>
    </div>
  );
}
