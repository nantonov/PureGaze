import { NavLink } from "react-router-dom";
import { menuItems } from "@/config/menu";
import { useState, useMemo } from "react";
import { useEmployee } from "@/contexts/EmployeeContext";
import styles from "./SideBar.module.css";
import { ChevronLeft, ChevronRight } from "lucide-react";
import { useTranslation } from "react-i18next";

export default function SideBar() {
  const { t } = useTranslation(["dashboard", "assessmentRequests", "assessments", "employees"]);
  const [collapsed, setCollapsed] = useState(false);
  const { employee } = useEmployee();
  const filteredMenuItems = useMemo(() => {
    return menuItems.filter((item) => {
      if (!item.roles) return true;
      if (!employee?.managerLevelId) return false;
      return item.roles.includes(employee.managerLevelId);
    });
  }, [employee?.managerLevelId]);

  return (
    <aside className={`${styles.sidebar} ${collapsed ? styles.collapsed : ""}`}>
      <button className={styles.toggle} onClick={() => setCollapsed((v) => !v)}>
        {collapsed ? <ChevronRight size={16} /> : <ChevronLeft size={16} />}
      </button>

      <nav className={styles.menu}>
        {filteredMenuItems.map((item) => (
          <NavLink
            key={item.path}
            to={item.path}
            className={({ isActive }) =>
              `${styles.link} ${isActive ? styles.active : ""} ${collapsed ? styles.linkCollapsed : ""}`
            }
            title={collapsed ? t(`${item.namespace}:title`, item.label) : ""}
          >
            <span className={styles.iconWrapper}>{item.icon}</span>

            {!collapsed && <span className={styles.linkLabel}>{t(`${item.namespace}:title`, item.label)}</span>}
          </NavLink>
        ))}
      </nav>
    </aside>
  );
}
