import { EmployeeMenu } from "@/components/EmployeeMenu/EmployeeMenu";
import styles from "./Header.module.css";
import AuthButtons from "@/components/AuthButtons/AuthButtons";

export default function Header() {
  return (
    <header className={styles.header}>
      <div className={styles.left}>
        <span className={styles.logo}>Inno Assessment Portal</span>
      </div>
      <AuthButtons />
      <div className={styles.right}>
        <EmployeeMenu />
      </div>
    </header>
  );
}
