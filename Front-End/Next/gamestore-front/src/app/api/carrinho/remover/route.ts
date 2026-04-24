import { cookies } from "next/headers";
import { NextResponse } from "next/server";




export async function DELETE(req:Request) {

    try{
        const body = await req.json();
        console.log(body)
        const cookieStore = await cookies();
        const token = cookieStore.get("token")?.value;
    
        if(!token)
             return NextResponse.json({ error: "Não autorizado" }, { status: 401 });

        const response = await fetch("http://localhost:5248/api/Carrinho/removerItem", {
            method: "DELETE",
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

        // CORREÇÃO: Verifica se há conteúdo antes de dar .json()
        const isJson = response.headers.get("content-type")?.includes("application/json");
        const data = isJson ? await response.json() : { success: true };
        
        return NextResponse.json(data);

    } catch (error) {
        console.error(error);
        return NextResponse.json({ error: "Erro interno" }, { status: 500 });
    }
    
}