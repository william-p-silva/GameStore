import { useRouter } from "next/navigation";
export async function Cadastro(nome:string, email:string, senha:string) {
    const response = await fetch("http://localhost:5248/api/Usuarios", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({nome, email, senha}),
    });
    if (!response.ok){
        throw new Error("Erro no login");
    }
    const data = await response.json();

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
        window.location.href = "/"
      else{
        window.location.href = "/"
      }
}