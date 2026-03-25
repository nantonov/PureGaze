import { useState, useMemo } from "react";
import { ChevronLeft, ChevronRight } from "lucide-react";
import { menuItems } from "@/config/menu";
import type { MenuItem } from "@/types/MenuItem";
import { useEmployee } from "@/contexts/EmployeeContext";
import SideBarLink from "./SideBarLink";
import SideBarSection from "./SideBarSection";
import styles from "./SideBar.module.css";

function isVisible(item: MenuItem, managerLevel?: string): boolean {
  if (!item.roles) return true;
  if (!managerLevel) return false;
  return item.roles.includes(managerLevel);
}

export default function SideBar() {
  const [collapsed, setCollapsed] = useState(false);
  const { employee } = useEmployee();

  const visibleItems = useMemo(
    () => menuItems.filter((item) => isVisible(item, employee?.managerLevel)),
    [employee?.managerLevel]
  );

  return (
    <aside className={`${styles.sidebar} ${collapsed ? styles.collapsed : ""}`}>
      <button className={styles.toggle} onClick={() => setCollapsed((v) => !v)}>
        {collapsed ? <ChevronRight size={16} /> : <ChevronLeft size={16} />}
      </button>

      <nav className={styles.menu}>
        {visibleItems.map((item) =>
          item.children ? (
            <SideBarSection key={item.namespace} item={item} collapsed={collapsed} />
          ) : (
            <SideBarLink key={item.path} item={item} collapsed={collapsed} />
          )
        )}
      </nav>
    </aside>
  );
}
