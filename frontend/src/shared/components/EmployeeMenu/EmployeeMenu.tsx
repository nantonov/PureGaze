import { useState, useRef, useEffect } from "react";
import { useEmployee } from "@/contexts/EmployeeContext";
import { useTheme } from "@/contexts/ThemeContext";
import { useLanguage } from "@/contexts/LanguageContext";
import { useAuth0 } from "@auth0/auth0-react";
import { ChevronDown, LogOut, Sun, Moon, Globe, Palette } from "lucide-react";
import styles from "./EmployeeMenu.module.css";

export const EmployeeMenu = () => {
  const { employee, setEmployee } = useEmployee();
  const { theme, toggle } = useTheme();
  const { language, setLanguage } = useLanguage();
  const { logout } = useAuth0();
  const [isOpen, setIsOpen] = useState(false);
  const menuRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (menuRef.current && !menuRef.current.contains(event.target as Node)) {
        setIsOpen(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  if (!employee) return null;

  const handleLogout = () => {
    setEmployee(null);
    logout({ logoutParams: { returnTo: window.location.origin } });
  };

  const initials = `${employee.firstName?.[0] || ""}${employee.lastName?.[0] || ""}`;

  return (
    <div className={styles.container} ref={menuRef}>
      <button
        className={`${styles.trigger} ${isOpen ? styles.active : ""}`}
        onClick={() => setIsOpen(!isOpen)}
      >
        <div className={styles.avatar}>
          <span>{initials}</span>
        </div>
        <div className={styles.info}>
          <span className={styles.name}>
            {employee.firstName} {employee.lastName}
          </span>
        </div>
        <ChevronDown
          size={14}
          className={styles.chevron}
          style={{ transform: isOpen ? "rotate(180deg)" : "none" }}
        />
      </button>

      {isOpen && (
        <div className={styles.dropdown}>
          <div className={styles.header}>Settings</div>

          <div className={styles.divider} />

          {/* Language */}
          <div className={styles.row}>
            <span className={styles.label}>
              <Globe size={16} /> Language
            </span>
            <div className={styles.selectWrapper}>
              <select
                className={styles.select}
                value={language}
                onChange={(e) => setLanguage(e.target.value as "en" | "ru")}
              >
                <option value="en">English</option>
                <option value="ru">Русский</option>
              </select>
            </div>
          </div>

          {/* Theme */}
          <div className={`${styles.row} ${styles.rowTopMargin}`}>
            <span className={styles.label}>
              <Palette size={16} /> Theme
            </span>
            <button
              type="button"
              className={styles.themeSwitch}
              onClick={toggle}
              title="Toggle theme"
            >
              <span
                className={`${styles.themeIcon} ${theme === "light" ? styles.themeActive : ""}`}
              >
                <Sun size={16} />
              </span>

              <span className={`${styles.themeIcon} ${theme === "dark" ? styles.themeActive : ""}`}>
                <Moon size={16} />
              </span>
            </button>
          </div>

          <div className={styles.divider} />

          <button type="button" className={styles.logoutBtn} onClick={handleLogout}>
            <LogOut size={16} />
            Logout
          </button>
        </div>
      )}
    </div>
  );
};
