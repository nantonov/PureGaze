import { Tab, Tabs, Typography, Stack, Button } from "@mui/material";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { assessmentRequestApi } from "@/features/assessment-request/api/assessmentRequestApi.ts";
import AssessmentRequestToMe from "@/features/assessment-request/ui/AssessmentRequestToMe";
import MyAssessmentRequest from "@/features/assessment-request/ui/MyAssessmentRequest";
import PageContentBox from "@/widgets/tableBox/PageContentBox.tsx";

export default function AssessmentRequest() {
  const { t } = useTranslation("assessmentRequests");
  const [tab, setTab] = useState(0);
  const [myRequestsRefresh, setMyRequestsRefresh] = useState(0);

  const handleChange = (_: React.SyntheticEvent, newValue: number) => {
    setTab(newValue);
  };

  const handleCreateAssessmentRequest = async () => {
    await assessmentRequestApi.CreateAssessmentRequest();
    setMyRequestsRefresh((n) => n + 1);
  };

  return (
    <PageContentBox>
      <Stack
        direction="row"
        justifyContent="space-between"
        alignItems="center"
        paddingTop={1}
        sx={{ flexShrink: 0, width: "100%", minWidth: 0, boxSizing: "border-box" }}
      >
        <Typography variant="h4">{t("title")}</Typography>

        {tab === 1 && (
          <Stack direction="row" spacing={2} alignItems="center" sx={{ flexShrink: 0 }}>
            <Button variant="contained" onClick={handleCreateAssessmentRequest}>
              New Request
            </Button>
          </Stack>
        )}
      </Stack>

      <Tabs value={tab} onChange={handleChange}>
        <Tab label={t("tabs.requestsToMe", { defaultValue: "To Me" })} />
        <Tab label={t("tabs.myRequests", { defaultValue: "From Me" })} />
      </Tabs>

      {tab === 0 ? (
        <AssessmentRequestToMe />
      ) : (
        <MyAssessmentRequest refreshTrigger={myRequestsRefresh} />
      )}
    </PageContentBox>
  );
}
