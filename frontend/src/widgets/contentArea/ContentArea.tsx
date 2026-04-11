import type { ReactNode } from "react";
import styles from "./ContentArea.module.css";

interface ContentAreaProps {
  children: ReactNode;
}

export default function ContentArea({ children }: ContentAreaProps) {
  return <main className={styles.content}>{children}</main>;
}
