import { Snackbar, Alert } from "@mui/material";

interface Props {
  message: string | null;
  onClose: () => void;
}

export default function ErrorSnackbar({ message, onClose }: Props) {
  return (
    <Snackbar
      open={message !== null}
      autoHideDuration={4000}
      onClose={onClose}
      anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
    >
      <Alert severity="error" onClose={onClose} sx={{ width: "100%" }}>
        {message}
      </Alert>
    </Snackbar>
  );
}
