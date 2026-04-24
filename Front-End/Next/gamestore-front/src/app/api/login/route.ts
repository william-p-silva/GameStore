
import { NextResponse } from "next/server";

export async function POST(req: Request) {
    try {
        const body = await req.json();
        console.log("BODY RECEBIDO:", body);

        const response = await fetch("http://localhost:5248/api/Usuarios/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(body),
        });

        console.log("STATUS API:", response.status);

        if (!response.ok) {
            const errorText = await response.text();
            console.log("ERRO API:", errorText);

            return NextResponse.json(
                { error: errorText },
                { status: response.status }
            );
        }

        const data = await response.json();

        const res = NextResponse.json({ success: true });

        res.cookies.set("token", data.token, {
            httpOnly: true,
            path: "/",
        });

        return res;
    } catch (error) {
        console.error("ERRO NO LOGIN:", error);

        return NextResponse.json(
            { error: "Erro interno" },
            { status: 500 }
        );
    }
}