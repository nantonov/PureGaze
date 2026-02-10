import { httpClient } from "@/api/httpClient";
import { useState } from "react";

export default function Dashboard() {
  const [message, setMessage] = useState("");

  const pingPublic = async () => {
    try {
      const response = await httpClient.get("/ping/public");
      setMessage(`Public: ${response.data}`);
    } catch (error) {
      setMessage(`Public Error: ${error}`);
    }
  };

  const pingSecure = async () => {
    try {
      const response = await httpClient.get("/ping/secure");
      setMessage(`Secure: ${response.data}`);
    } catch (error) {
      setMessage(`Secure Error: ${error}`);
    }
  };

  return (
    <div style={{ padding: "20px" }}>
      <h1>Dashboard</h1>
      <div style={{ display: "flex", gap: "10px", margin: "10px 0" }}>
        <button onClick={pingPublic} style={{ padding: "10px" }}>
          Ping Public (No Auth)
        </button>
        <button onClick={pingSecure} style={{ padding: "10px" }}>
          Ping Secure (Auth Required)
        </button>
      </div>
      {message && <p>{message}</p>}
    </div>
  );
}
