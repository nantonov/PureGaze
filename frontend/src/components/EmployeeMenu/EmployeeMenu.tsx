import { useState } from "react";
import { useEmployee } from "../../app/context/EmployeeContext";
import { useTheme } from "../../app/context/ThemeContext";
import { useLanguage } from "../../app/context/LanguageContext";

export const EmployeeMenu = () => {
  const { employee } = useEmployee();
  const { theme, toggle } = useTheme();
  const { language, setLanguage } = useLanguage();

  const [open, setOpen] = useState(false);

  if (!employee) return null;

  return (
    <div className="user-menu">
      <button onClick={() => setOpen(o => !o)}>
        {employee.lastName} {employee.firstName}
      </button>

      {open && (
        <div className="dropdown">
          <div className="dropdown-item" onClick={toggle}>
            Theme: {theme}
          </div>

          <div className="dropdown-item">
            Language:
            <select
              value={language}
              onChange={e => setLanguage(e.target.value as any)}
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
