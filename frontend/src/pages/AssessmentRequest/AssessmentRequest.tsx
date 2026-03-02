import { useTranslation } from "react-i18next";

export default function AssessmentRequest() {
  const { t } = useTranslation("assessmentRequests");

  return <h1>{t("title")}</h1>;
}
