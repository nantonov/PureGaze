import { useTranslation } from "react-i18next";
import type { MenuItem } from "@/types/MenuItem";
import SideBarLink from "./SideBarLink";
import styles from "./SideBar.module.css";

interface Props {
  item: MenuItem;
  collapsed: boolean;
}

export default function SideBarSection({ item, collapsed }: Props) {
  const { t } = useTranslation(item.namespace);
  const label = t("title", item.label);

  return (
    <div className={styles.section}>
      <div className={`${styles.sectionHeader} ${collapsed ? styles.sectionHeaderCollapsed : ""}`}>
        <span className={styles.iconWrapper}>{item.icon}</span>
        {!collapsed && <span className={styles.sectionLabel}>{label}</span>}
      </div>
      <div className={styles.sectionChildren}>
        {item.children!.map((child) => (
          <SideBarLink key={child.path} item={child} collapsed={collapsed} />
        ))}
      </div>
    </div>
  );
}
