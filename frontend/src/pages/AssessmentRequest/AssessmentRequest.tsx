import { Box, Tab, Tabs, Typography } from "@mui/material";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import AssessmentRequestToMe from "@/pages/AssessmentRequest/AssessmentRequestToMe.tsx";
import MyAssessmentRequest from "@/pages/AssessmentRequest/MyAssessmentRequest.tsx";

export default function AssessmentRequest() {
  const { t } = useTranslation("assessmentRequests");
  const [tab, setTab] = useState(0);

  const handleChange = (_: React.SyntheticEvent, newValue: number) => {
    setTab(newValue);
  };
  
  return (
    <Box sx={{ display: "flex", flexDirection: "column", gap: 2 }}>
        <Typography variant="h4">
            {t("title")}
        </Typography>

      <Tabs value={tab} onChange={handleChange}>
        <Tab label={t("tabs.requestsToMe", { defaultValue: "Requests to me" })} />
        <Tab label={t("tabs.myRequests", { defaultValue: "My requests" })} />
      </Tabs>

        <Box hidden={tab !== 0}>
            <AssessmentRequestToMe />
        </Box>

        <Box hidden={tab !== 1}>
            <MyAssessmentRequest />
        </Box>
    </Box>
  );
}
