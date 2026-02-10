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
    <div className={styles.pageWrapper}>
      <div className={styles.content}>
        <div className={styles.logo}></div>
        <button className={styles.btn} onClick={handleLogin}>
          Continue with Google
        </button>
        <button
          style={{ marginTop: "1rem", backgroundColor: "grey" }}
          className={styles.btn}
          onClick={() => {
            fetch(`${import.meta.env.VITE_API_URL}/ping`)
              .then(res => res.text())
              .then(text => alert(`BACKEND SAYS: ${text}`))
              .catch(err => alert(`ERROR: ${err.message}`));
          }}
        >
          TEST PING (Check Backend)
        </button>
      </div>
    </div>
  );
}
