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


export async function logoutService(): Promise<boolean> {
  try{
    const response = await fetch("/api/logout", {
      method: "POST",
  
    })
    return response.ok    
  }catch (error){
    console.error("Erro ao deslogar:", error);
    return false;
  }

 
}