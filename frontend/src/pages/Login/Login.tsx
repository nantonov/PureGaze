import { useAuth0 } from "@auth0/auth0-react";
import styles from "./Login.module.css";

export default function Login() {
  const { loginWithRedirect } = useAuth0();

  function handleLogin() {
    loginWithRedirect({
      authorizationParams: {
        connection: "google-oauth2",
      },
    });
  }

  return (
    <div className={styles.content}>
      <div className={styles.logo}></div>
      <button className={styles.btn} onClick={handleLogin}>
        Continue with Google
      </button>
    </div>
  );
}
