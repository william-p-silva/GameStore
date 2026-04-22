// /services/auth.ts

export async function login(email: string, senha: string) {
    const response = await fetch("https://localhost:7220/api/usuarios/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ email, senha }),
    });
  
    if (!response.ok) {
      throw new Error("Erro no login");
    }
  
    const data = await response.json();
    return data.token;
  }