import { useTranslation } from "react-i18next";

export default function Assessment() {
  const { t } = useTranslation("assessments");

  return <h1>{t("title")}</h1>;
}