import { EmployeeMenu } from "../EmployeeMenu/EmployeeMenu";
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
};
