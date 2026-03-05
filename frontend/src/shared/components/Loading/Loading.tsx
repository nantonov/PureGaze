import styles from "./Loading.module.css";

interface LoadingProps {
  message?: string;
  fullScreen?: boolean;
}

export const Loading = ({ message = "", fullScreen = true }: LoadingProps) => {
  const containerClass = fullScreen ? styles.overlay : styles.local;

  return (
    <div className={containerClass}>
      <div className={styles.orbitSpinner}></div>
      {message && <p className={styles.message}>{message}</p>}
    </div>
  );
};
