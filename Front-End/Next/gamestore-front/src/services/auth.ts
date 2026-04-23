// /services/auth.ts
export async function login(email: string, senha: string) {
  const res = await fetch("/api/login", {
      method: "POST",
      body: JSON.stringify({ email, senha }),
  });

  if (!res.ok) {
      throw new Error("Erro ao logar");
  }
}