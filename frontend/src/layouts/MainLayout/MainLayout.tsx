import { Outlet } from "react-router-dom";
import SideBar from "../../components/SideBar/SideBar";
import Header from "../../components/Header/Header";
import styles from "./MainLayout.module.css";

export default function MainLayout() {
  return (
    <div className={styles.layout}>
      <Header />
      <div className={styles.body}>
        <SideBar />
        <main className={styles.content}>
          <Outlet />
        </main>
      </div>
    </div>
  );
}
