import { EmployeeMenu } from "@/widgets/employeeMenu/EmployeeMenu.tsx";
import styles from "./Header.module.css";

export default function Header() {
  return (
    <header className={styles.header}>
      <div className={styles.left}>
        <span className={styles.logo}>Inno Assessment Portal</span>
      </div>
      <div className={styles.right}>
        <EmployeeMenu />
      </div>
    </header>
  );
}
