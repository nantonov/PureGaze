import { useState, useRef, useEffect } from "react";
import { useEmployee } from "@/app/providers/employee/EmployeeProvider.tsx";
import { useTheme } from "@/app/providers/theme/ThemeProvider.tsx";
import { useLanguage } from "@/app/providers/i18n/LanguageProvider.tsx";
import { useAuth0 } from "@auth0/auth0-react";
import { ChevronDown, LogOut, Sun, Moon, Globe, Palette } from "lucide-react";
import { useTranslation } from "react-i18next";
import styles from "./EmployeeMenu.module.css";

export const EmployeeMenu = () => {
  const { employee, setEmployee } = useEmployee();
  const { theme, toggle } = useTheme();
  const { language, setLanguage } = useLanguage();
  const { user, logout } = useAuth0();
  const { t } = useTranslation("profile");
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
          {user?.picture ? (
            <img
              src={user.picture}
              alt={initials}
              referrerPolicy="no-referrer"
              onError={(e) => {
                (e.currentTarget as HTMLImageElement).style.display = "none";
                e.currentTarget.nextElementSibling?.removeAttribute("style");
              }}
            />
          ) : null}
          <span style={user?.picture ? { display: "none" } : undefined}>{initials}</span>
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
          <div className={styles.header}>{t('profile')}</div>

          <div className={styles.divider} />

          {/* Language */}
          <div className={styles.row}>
            <span className={styles.label}>
              <Globe size={16} /> {t('language')}
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
              <Palette size={16} /> {t('theme')}
            </span>
            <button
              type="button"
              className={styles.themeSwitch}
              onClick={toggle}
              title={t('toggleTheme')}
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
            {t('logout')}
          </button>
        </div>
      )}
    </div>
  );
};
