import { cookies } from "next/headers";
import { parseJwt } from "@/lib/auth";
import Link from "next/link";

export default async function Header() {
    const cookieStore =  await cookies();

    const token = cookieStore.get("token")?.value;

    let nome = "Visitante";
    let role = null;

    if (token) {
        const user = parseJwt(token);

        if (user) {
            nome = user?.nome ?? "Visitante";
            role = user?.role;
        }
    }

    return (
        <header className="bg-black text-white p-4 flex justify-between">
            <h1>GameStore</h1>
            <Link href={"/login"} >Login</Link>

            <div className="flex gap-4 items-center">
                {role == "admin" && (
                    <a href="/admin" className="text-yellow-400">
                        Admin
                    </a>
                )}

                <span>Olá, {nome}</span>
            </div>
        </header>
    );
}