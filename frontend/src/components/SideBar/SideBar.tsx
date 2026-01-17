import { NavLink } from "react-router-dom";
import { menuItems } from "../../config/menu";

export default function SideBar() {
  return (
    <aside
      style={{
        width: "220px",
        borderRight: "1px solid #ddd",
        padding: "16px",
      }}
    >
      <nav>
        {menuItems.map((item) => (
          <div key={item.path}>
            <NavLink
              to={item.path}
              style={({ isActive }) => ({
                display: "block",
                padding: "8px",
                textDecoration: "none",
                fontWeight: isActive ? "bold" : "normal",
              })}
            >
              {item.label}
            </NavLink>
          </div>
        ))}
      </nav>
    </aside>
  );
}
