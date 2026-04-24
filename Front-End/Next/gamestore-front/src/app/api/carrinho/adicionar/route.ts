import { cookies } from "next/headers";
import { NextResponse } from "next/server";


export async function POST(req: Request) {
    const body = await req.json();
    console.log("BODY API:", body);
    try {
        const cookieStore = cookies();
        const token = (await cookieStore).get("token")?.value;

        if (!token) {
            return NextResponse.json({ error: "Não autorizado" }, { status: 401 });
        }
        console.log(token)

        const response = await fetch("http://localhost:5248/api/Carrinho/adicionarItem", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify(body)
        });
        if (!response.ok) {
            const text = await response.text();
            return NextResponse.json({ error: text }, { status: 400 });
        }

        const data = await response.json();
        return NextResponse.json(data);

    } catch (error) {
        console.error(error);
        return NextResponse.json({ error: "Erro interno" }, { status: 500 });
    }

}