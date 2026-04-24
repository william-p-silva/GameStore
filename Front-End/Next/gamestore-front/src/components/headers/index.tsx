import { cookies } from "next/headers";
import { parseJwt } from "@/lib/auth";
import Link from "next/link";

export default async function Header() {
    const cookieStore = await cookies();

    const token = cookieStore.get("token")?.value;

    let nome = "Visitante";
    let role = null;
    let email = "";

    if (token) {
        const user = parseJwt(token);

        if (user) {
            nome = user?.nome ?? "Visitante";
            role = user?.role;
            email = user.email;
        }
    }

    return (
        <header className="bg-black text-white p-4 flex justify-between">

            <h1>GameStore</h1>


            <div className="flex gap-4 items-center">
                {role === null && (
                    <>
                        <Link href={"/login"} >Entrar</Link>
                        <Link href={"/cadastro"} >Cadastrar</Link>
                    </>
                )}

                {role == "admin" && (
                    <a href="/admin" className="text-yellow-400">
                        Admin
                    </a>
                )}

                {nome != "Visitante" && (
                    <span>
                        Olá, {nome}
                    </span>
                )}

            </div>
        </header>
    );
}