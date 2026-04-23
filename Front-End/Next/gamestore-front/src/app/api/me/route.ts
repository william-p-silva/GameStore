import { cookies } from "next/headers";


export async function GET() {
    const token = (await cookies()).get("token")?.value;

    if (!token)
        return Response.json({user: null})

    const payload: any = JSON.parse(atob(token.split(".")[1]));

    return Response.json({
        email: payload.email,
        role: payload.role,
        nome: payload.nome,
    })
}
