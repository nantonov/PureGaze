import { useState } from "react";
import { useEmployee } from "@/app/context/EmployeeContext";
import { useTheme } from "@/app/context/ThemeContext";
import { useLanguage } from "@/app/context/LanguageContext";
import styles from "./EmployeeMenu.module.css";

export const EmployeeMenu = () => {
  const { employee } = useEmployee();
  const { theme, toggle } = useTheme();
  const { language, setLanguage } = useLanguage();

  const [open, setOpen] = useState(false);

  if (!employee) return null;

  return (
    <div className={styles.userMenu}>
      <button onClick={() => setOpen((o) => !o)}>
        {employee.lastName} {employee.firstName}
      </button>

      {open && (
        <div className={styles.dropdown}>
          <button type="button" className={styles.dropdownItem} onClick={toggle}>
            Theme: {theme}
          </button>

          <div className={styles.dropdownItem}>
            <span>Language:</span>
            <select
              className={styles.select}
              value={language}
              onChange={(e) => setLanguage(e.target.value === "ru" ? "ru" : "en")}
            >
              <option value="en">EN</option>
              <option value="ru">RU</option>
            </select>
          </div>
        </div>
      )}
    </div>
  );
};
