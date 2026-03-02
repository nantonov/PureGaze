import { useTranslation } from "react-i18next";

export default function Employee() {
  const { t } = useTranslation("employees");

  return <h1>{t("title")}</h1>;
}
