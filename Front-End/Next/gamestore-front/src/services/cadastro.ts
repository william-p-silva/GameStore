
export async function Cadastro(nome:string, email:string, senha:string) {
    const response = await fetch("https://localhost:7220/api/usuarios", {
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
    return data;
}