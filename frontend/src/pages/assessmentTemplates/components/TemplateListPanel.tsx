import { Box, IconButton, Menu, MenuItem, Stack, Tooltip, Typography } from "@mui/material";
import { MoreVertical, Plus } from "lucide-react";
import { useState } from "react";
import type { Template } from "@/entities/template/Template.ts";
import { ConfirmDeleteDialog } from "@/shared/ui/ConfirmDeleteDialog.tsx";

interface Props {
  templates: Template[];
  selectedId: number | null;
  onSelect: (id: number) => void;
  onCreate: () => void;
  onDelete: (id: number) => void;
}

export default function TemplateListPanel({
  templates,
  selectedId,
  onSelect,
  onCreate,
  onDelete,
}: Props) {
  const [menuAnchor, setMenuAnchor] = useState<{
    el: HTMLElement;
    template: Template;
  } | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<Template | null>(null);

  return (
    <Stack
      sx={{
        width: 260,
        flexShrink: 0,
        height: "100%",
        borderRight: "1px solid var(--border-color, rgba(0,0,0,0.12))",
        bgcolor: "background.paper",
      }}
    >
      <Stack
        direction="row"
        alignItems="center"
        justifyContent="space-between"
        sx={{ px: 2, py: 1.5, borderBottom: "1px solid var(--border-color, rgba(0,0,0,0.08))" }}
      >
        <Stack direction="row" alignItems="center" spacing={1}>
          <Typography variant="subtitle1" sx={{ fontWeight: 600 }}>
            Templates
          </Typography>
          <Typography variant="caption" color="text.secondary">
            {templates.length}
          </Typography>
        </Stack>
        <Tooltip title="New template">
          <IconButton size="small" onClick={onCreate} sx={{ color: "text.secondary" }}>
            <Plus size={16} />
          </IconButton>
        </Tooltip>
      </Stack>

      <Box sx={{ flex: 1, overflowY: "auto", py: 1 }}>
        {templates.length === 0 ? (
          <Typography variant="body2" color="text.secondary" sx={{ p: 2, textAlign: "center" }}>
            No templates yet
          </Typography>
        ) : (
          templates.map((t) => {
            const isSelected = t.id === selectedId;
            return (
              <Box
                key={t.id}
                onClick={() => onSelect(t.id)}
                sx={{
                  px: 2,
                  py: 1.25,
                  mx: 1,
                  my: 0.5,
                  borderRadius: 1.5,
                  cursor: "pointer",
                  borderLeft: "3px solid",
                  borderLeftColor: isSelected ? "var(--brand-color)" : "transparent",
                  bgcolor: isSelected ? "rgba(198, 48, 49, 0.08)" : "transparent",
                  transition: "background-color 120ms ease",
                  "&:hover": {
                    bgcolor: isSelected ? "rgba(198, 48, 49, 0.12)" : "action.hover",
                  },
                  "&:hover .row-actions": { opacity: 1 },
                }}
              >
                <Stack
                  direction="row"
                  alignItems="center"
                  justifyContent="space-between"
                  spacing={1}
                >
                  <Box sx={{ minWidth: 0, flex: 1 }}>
                    <Typography
                      variant="body2"
                      sx={{
                        fontWeight: 600,
                        color: isSelected ? "var(--brand-color)" : "text.primary",
                        overflow: "hidden",
                        textOverflow: "ellipsis",
                        whiteSpace: "nowrap",
                      }}
                    >
                      {t.codeName || `Template #${t.id}`}
                    </Typography>
                    <Typography
                      variant="caption"
                      color="text.secondary"
                      sx={{
                        display: "block",
                        overflow: "hidden",
                        textOverflow: "ellipsis",
                        whiteSpace: "nowrap",
                      }}
                    >
                      {t.codeDisplay}
                    </Typography>
                  </Box>
                  <IconButton
                    size="small"
                    className="row-actions"
                    onClick={(e) => {
                      e.stopPropagation();
                      setMenuAnchor({ el: e.currentTarget, template: t });
                    }}
                    sx={{ opacity: isSelected ? 1 : 0, transition: "opacity 120ms ease" }}
                  >
                    <MoreVertical size={16} />
                  </IconButton>
                </Stack>
              </Box>
            );
          })
        )}
      </Box>

      <Menu
        anchorEl={menuAnchor?.el ?? null}
        open={Boolean(menuAnchor)}
        onClose={() => setMenuAnchor(null)}
      >
        <MenuItem
          onClick={() => {
            if (menuAnchor) setDeleteTarget(menuAnchor.template);
            setMenuAnchor(null);
          }}
          sx={{ color: "error.main" }}
        >
          Delete
        </MenuItem>
      </Menu>

      <ConfirmDeleteDialog
        open={deleteTarget !== null}
        label="Template"
        onCancel={() => setDeleteTarget(null)}
        onConfirm={() => {
          if (deleteTarget) onDelete(deleteTarget.id);
          setDeleteTarget(null);
        }}
      />
    </Stack>
  );
}
