// /app/login/page.tsx

"use client";

import { useState } from "react";
import { login } from "@/services/auth";
import { useRouter } from "next/navigation";

export default function LoginPage() {
  const [email, setEmail] = useState("");
  const [senha, setSenha] = useState("");
  const route = useRouter();

    async function handleLogin(e: React.FormEvent) {
        e.preventDefault();

        const res = await fetch("/api/login", {
            method: "POST",
            body: JSON.stringify({ email, senha }),
        });

        if (!res.ok)
          alert("Login invalido")

        const me = await fetch("/api/me");
        const user = await me.json();
        console.log("USER:", user);


        if (user.role == "admin")
          route.push("/admin");
        else{
          route.push("/");
        }
    }
  

  return (
    <div className="flex items-center justify-center h-screen">
      <form
        onSubmit={handleLogin}
        className="bg-white p-6 rounded shadow-md w-80"
      >
        <h1 className="text-xl mb-4">Login</h1>

        <input
          type="email"
          placeholder="Email"
          className="w-full mb-3 p-2 border"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />

        <input
          type="password"
          placeholder="Senha"
          className="w-full mb-3 p-2 border"
          value={senha}
          onChange={(e) => setSenha(e.target.value)}
        />

        <button
          type="submit"
          className="w-full bg-blue-500 text-white p-2"
        >
          Entrar
        </button>
      </form>
    </div>
  );
}