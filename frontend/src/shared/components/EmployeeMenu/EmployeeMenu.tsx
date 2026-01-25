import { useState } from "react";
import { useEmployee } from "@/contexts/EmployeeContext";
import { useTheme } from "@/contexts/ThemeContext";
import { useLanguage } from "@/contexts/LanguageContext";
import { useAuth0 } from "@auth0/auth0-react";
import styles from "./EmployeeMenu.module.css";

export const EmployeeMenu = () => {
  const { employee, setEmployee  } = useEmployee();
  const { theme, toggle } = useTheme();
  const { language, setLanguage } = useLanguage();
  const { logout } = useAuth0();
  const [open, setOpen] = useState(false);

  if (!employee) return null;

  const handleLogout = () => {
    setEmployee(null);
    logout({
      logoutParams: {
        returnTo: window.location.origin,
      },
    });
  };

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
          <button
            type="button"
            className={styles.dropdownItem}
            onClick={handleLogout}
          >
            Logout
          </button>
        </div>
      )}
    </div>
  );
};
