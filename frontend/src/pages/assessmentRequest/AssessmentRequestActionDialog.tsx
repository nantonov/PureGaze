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

export function AssessmentRequestActionDialog({ open, action, code, employeeFullName, onConfirm, onClose }: AssessmentRequestActionDialogProps) {
    const [reason, setReason] = useState("");
    const [isDisabled, setIsDisabled] = useState(true);
    
    const handleConfirm = () => {
        onConfirm(action === "reject" ? reason : undefined);
        setReason("");
    };

    const handleClose = () => {
        setReason("");
        onClose();
    };

    const handleReasonChange = (value:string) => {
        setReason(value);
        setIsDisabled(value.length === 0);
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
                        onChange={(e) => handleReasonChange(e.target.value)}
                    />
                )}
            </DialogContent>
            <DialogActions>
                <Button
                    variant="contained"
                    color={action === "approve" ? "success" : "error"}
                    onClick={handleConfirm}
                    disabled={action === "reject" && isDisabled}
                >
                    {action === "approve" ? "Approve" : "Reject"}
                </Button>
                <Button onClick={handleClose}>Cancel</Button>
            </DialogActions>
        </Dialog>
    );
}
