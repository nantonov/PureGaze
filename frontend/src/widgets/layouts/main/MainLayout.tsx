import { Outlet } from "react-router-dom";
import SideBar from "@/widgets/sidebar/SideBar.tsx";
import Header from "@/widgets/header/Header.tsx";
import ContentArea from "@/widgets/contentArea/ContentArea.tsx";
import styles from "./MainLayout.module.css";

export default function MainLayout() {
  return (
    <div className={styles.layout}>
      <Header />
      <div className={styles.body}>
        <SideBar />
        <ContentArea>
          <Outlet />
        </ContentArea>
      </div>
    </div>
  );
}
