import { createTheme } from "@mui/material";
import { useTheme } from "@/app/providers/theme/ThemeProvider.tsx";

export function useMuiTheme() {
    const { theme } = useTheme();
    return createTheme({ palette: { mode: theme } });
}
