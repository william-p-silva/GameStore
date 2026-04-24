import { cookies } from "next/headers";
import { NextResponse } from "next/server";


export async function ListarCarrinho() {

    const cookieStore = await cookies();
    const token = cookieStore.get("token")?.value;

    if (!token) {
        return NextResponse.json({ error: "Não autorizado" }, { status: 401 });
    }

    // 2. Pegar o corpo da requisição que veio do front-end


    // 3. Repassar para o seu backend externo
    const response = await fetch("http://localhost:5248/api/Carrinho/carrinho", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
        },
        cache: "no-store"
    });

    if (!response.ok) {
        throw new Error(`Falha em listar itens do carrinho`);
    }
    return response.json();
}