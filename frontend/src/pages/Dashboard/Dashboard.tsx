import { httpClient } from "@/api/httpClient";
import { useState } from "react";

export default function Dashboard() {
  const [message, setMessage] = useState("");

  const ping = async () => {
    try {
      const response = await httpClient.get("/ping");
      setMessage(`Success: ${response.data}`);
    } catch (error) {
      setMessage(`Error: ${error}`);
    }
  };

  return (
    <div style={{ padding: "20px" }}>
      <h1>Dashboard</h1>
      <button onClick={ping} style={{ padding: "10px", margin: "10px 0" }}>
        Test Ping (Auth Required)
      </button>
      {message && <p>{message}</p>}
    </div>
  );
}
