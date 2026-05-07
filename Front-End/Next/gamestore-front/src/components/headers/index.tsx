import { cookies } from "next/headers";
import { parseJwt } from "@/lib/auth";
import Link from "next/link";
import { Menu, Search, ShoppingCart } from "lucide-react";
import { UserControl } from "./UserControl";
import { MenuTrigger } from "./menuTrigger";
import { AvatarTrigger } from "./avatarTrigger";

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
        <>
        <UserControl email={email} nome={nome}  >
            <header className="flex items-center justify-between w-full py-3 px-6 border-b border-gray-300">
                {/* Hamburguer */}
                <div className="mt-2">

                    <MenuTrigger />

                </div>

                {/* LogoTipo */}
                <div>
                    <Link href={"/"}>
                        <h1 className="text-2xl text-cinza font-bold ">GameStore</h1>
                    </Link>
                </div>

                {/* Barra de pesquisa */}
                <div className="flex-1 mx-4 max-w-2xl"> {/* flex-1 para crescer, mx-4 para o "respiro" */}
                    <div className="relative flex items-center border border-gray-300 rounded-lg px-3 py-2 group focus-within:ring focus-within:ring-gray-500 transition-all focus:text-gray-500">
                        <Search className="text-gray-400 w-5 h-5 focus-within:text-gray-500" />
                        <input
                            type="text"
                            placeholder="Buscar jogos, consoles, acessórios..."
                            className="bg-transparent border-none outline-none w-full ml-2  text-gray-700 placeholder:text-gray-400 text-md"
                        />
                    </div>
                </div>

                {/* Botões de Ação || saudação */}
                {token ? (
                    <AvatarTrigger email={email} nome={nome} />
                    
                ) : (
                    <div className="text-cinza font-medium text-lg flex gap-3">
                        <Link href={"/cadastro"} className="hover:bg-slate-200/80  rounded-xl px-3.5 py-1.5 cursor-pointer">
                            Cadastre-se
                        </Link >
                        <Link href={"/login"} className="text-white bg-cinza rounded-xl px-3.5 py-1.5 cursor-pointer" >
                            Entrar
                        </Link>

                    </div>
                )}


                {/* Carrinho */}
                <Link href={"/cliente/carrinho"} className="text-cinza">
                    <ShoppingCart />
                </Link >

            </header>
            </UserControl>
        </>
    );
}