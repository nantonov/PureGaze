import styles from "./Loading.module.css";

export const Loading = ({ message = "Loading..." }: { message?: string }) => {
  return (
    <div className={styles.loading}>
      <div className={styles.spinner}></div>
      <p className={styles.message}>{message}</p>
    </div>
  );
};
