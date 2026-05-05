//app/api/logout

import { cookies } from "next/headers";
import { NextResponse } from "next/server";


export async function POST() {
    try{ 
        const cookieStore = await cookies()

        cookieStore.set("token", "", {
            httpOnly: true,
            path: "/",
            expires: new Date(0)
        })

        return NextResponse.json({message: "Logout realizado"})
     } catch (error) {
        return NextResponse.json(
          { error: "Erro ao fazer logout" },
          { status: 500 }
        );
      }
}