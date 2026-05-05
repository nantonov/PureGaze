import { useState } from "react";
import { Button, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, TextField } from "@mui/material";

type Action = "approve" | "reject";

interface AssessmentRequestActionDialogProps {
    open: boolean;
    action: Action | null;
    code: string;
    employeeFullName: string;
    onConfirm: (reason?: string) => void;
    onClose: () => void;
}

export function ConfirmActionDialog({ open, action, code, employeeFullName, onConfirm, onClose }: ConfirmActionDialogProps) {
    const [reason, setReason] = useState("");

    const handleConfirm = () => {
        onConfirm(action === "reject" ? reason : undefined);
        setReason("");
    };

    const handleClose = () => {
        setReason("");
        onClose();
    };

    return (
        <Dialog open={open} onClose={handleClose} fullWidth maxWidth="sm">
            <DialogTitle>
                {action === "approve" ? "Approve assessment request" : "Reject assessment request"}
            </DialogTitle>
            <DialogContent>
                <DialogContentText>
                    Are you sure you want to {action} assessment request{" "}
                    <strong>{code}</strong> for <strong>{employeeFullName}</strong>?
                </DialogContentText>
                {action === "reject" && (
                    <TextField
                        autoFocus
                        fullWidth
                        multiline
                        rows={3}
                        margin="normal"
                        label="Rejection reason"
                        value={reason}
                        onChange={(e) => setReason(e.target.value)}
                    />
                )}
            </DialogContent>
            <DialogActions>
                <Button onClick={handleClose}>Cancel</Button>
                <Button
                    variant="contained"
                    color={action === "approve" ? "success" : "error"}
                    onClick={handleConfirm}
                >
                    {action === "approve" ? "Approve" : "Reject"}
                </Button>
            </DialogActions>
        </Dialog>
    );
}
