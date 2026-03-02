import { useTranslation } from "react-i18next";

export default function Dashboard() {
    const { t } = useTranslation("dashboard");
    return <h1>{t("title")}</h1>;
}
