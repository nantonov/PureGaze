import { useTheme } from "../../app/providers/ThemeProvider";

export default function ThemeToggle() {
  const { theme, toggle } = useTheme();

  return (
    <button onClick={toggle}>
      {theme === "light" ? "D" : "L"}
    </button>
  );
};