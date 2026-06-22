import { NavLink } from "react-router-dom";
import { useTranslation } from "react-i18next";
import type { MenuItem } from "@/shared/model/MenuItem";
import styles from "./SideBar.module.css";

interface Props {
  item: MenuItem;
  collapsed: boolean;
}

export default function SideBarLink({ item, collapsed }: Props) {
  const { t } = useTranslation(item.namespace);
  const label = t("title", item.label);

  return (
    <NavLink
      to={item.path!}
      className={({ isActive }) => `${styles.link} ${isActive ? styles.active : ""}`}
      title={collapsed ? label : ""}
    >
      <span className={styles.iconWrapper}>{item.icon}</span>
      {!collapsed && <span className={styles.linkLabel}>{label}</span>}
    </NavLink>
  );
}
