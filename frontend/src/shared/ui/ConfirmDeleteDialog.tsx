import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@mui/material";

export interface ConfirmDeleteProps {
  open: boolean;
  label: string;
  onCancel: () => void;
  onConfirm: () => void;
}

export function ConfirmDeleteDialog({ open, label, onCancel, onConfirm }: ConfirmDeleteProps) {
  return (
    <Dialog open={open} onClose={onCancel} maxWidth="xs" fullWidth>
      <DialogTitle>Delete {label}?</DialogTitle>
      <DialogContent>
        <DialogContentText>
          This will permanently delete the selected {label.toLowerCase()}. This action cannot be
          undone.
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={onCancel}>Cancel</Button>
        <Button variant="contained" color="error" onClick={onConfirm}>
          Delete
        </Button>
      </DialogActions>
    </Dialog>
  );
}
