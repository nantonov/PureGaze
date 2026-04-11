import { useMuiTheme } from "@/shared/hooks/useMuiTheme.ts";
import {ThemeProvider, Box } from "@mui/material";

export default function PageContentBox({ children }: { children: React.ReactNode }) {
    const muiTheme = useMuiTheme();
    
    return (
        <ThemeProvider theme={muiTheme}>
            <Box
                sx={{
                    display: "flex",
                    flexDirection: "column",
                    gap: 2,
                    height: "100%",
                    minHeight: 0,
                    width: "100%",
                    boxSizing: "border-box",
                    overflow: "hidden",
                }}
            >
                {children}
            </Box>
        </ThemeProvider>
    );
}