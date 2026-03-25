import { createTheme } from "@mui/material";
import { useTheme } from "@/contexts/ThemeContext";

export function useMuiTheme() {
    const { theme } = useTheme();
    return createTheme({ palette: { mode: theme } });
}
