import ThemeToggle from "../ThemeToggle/ThemeToggle";
import styles from "./Header.module.css";

export default function Header() {
  return (
    <header className={styles.header}>
      <div className={styles.left}>
        <span className={styles.logo}>MyApp</span>
      </div>

      <div className={styles.right}>
        <select className={styles.select}>
          <option value="en">EN</option>
          <option value="ru">RU</option>
        </select>

        <ThemeToggle />

        <div className={styles.user}>
          user settings
        </div>
      </div>
    </header>
  );
};
