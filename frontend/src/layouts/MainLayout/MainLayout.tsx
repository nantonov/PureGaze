import { Outlet } from "react-router-dom";
import SideBar from "@/shared/components/SideBar/SideBar";
import Header from "@/shared/components/Header/Header";
import ContentArea from "@/shared/components/ContentArea/ContentArea";
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
