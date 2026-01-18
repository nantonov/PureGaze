import { NavLink } from "react-router-dom";
import { menuItems } from "../../config/menu";
import { useState } from "react";
import styles from "./SideBar.module.css";

export default function SideBar() {
  const [collapsed, setCollapsed] = useState(false);

  return (
    <aside
    className={`${styles.sidebar} ${
      collapsed ? styles.collapsed : ""
    }`}
    >
       <div className={styles.header}>
        <button
        className={styles.toggle}
          onClick={() => setCollapsed(v => !v)}>
            {collapsed ? "expand" : "collapse"}
        </button>
      </div>

      <nav className={styles.menu}>
        {menuItems.map((item) => (
          <NavLink 
            key={item.path}
            to={item.path}
            className={({ isActive }) =>
              `${styles.link} ${isActive ? styles.active : ""}`
            }>
             {!collapsed && item.label}
          </NavLink>
        ))}
      </nav>
    </aside>
  );
}
