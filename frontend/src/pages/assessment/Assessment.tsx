import { useState } from "react";
import { Tab, Tabs } from "@mui/material";
import PageContentBox from "@/widgets/tableBox/PageContentBox";
import NewAssessmentsTab from "./tabs/NewAssessmentsTab";
import HistoryAssessmentsTab from "./tabs/HistoryAssessmentsTab";

export default function Assessment() {
  const [tab, setTab] = useState(0);

  return (
    <PageContentBox>
      <Tabs
        value={tab}
        onChange={(_, v: number) => setTab(v)}
        sx={{
          "& .MuiTab-root": { textTransform: "none", fontWeight: 500 },
          "& .Mui-selected": { color: "var(--brand-color) !important" },
          "& .MuiTabs-indicator": { backgroundColor: "var(--brand-color)" },
        }}
      >
        <Tab label="New Assessments" />
        <Tab label="History" />
      </Tabs>

      {tab === 0 ? <NewAssessmentsTab /> : <HistoryAssessmentsTab />}
    </PageContentBox>
  );
}
