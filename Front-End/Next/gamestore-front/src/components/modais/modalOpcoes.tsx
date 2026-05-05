import { logoutService } from "@/services/auth"
import { Heart, LogOut, Package, Settings, ShoppingCart, User, X } from "lucide-react"
import Link from "next/link"
import { useRouter } from "next/navigation"
import Swal from "sweetalert2"

interface MenuOpcoesProps {
    modalAberto: boolean,
    setModalAberto: (aberto: boolean) => void
    nome: string,
    email: string
}


export function MenuOpcoes({ modalAberto, setModalAberto, nome, email }: MenuOpcoesProps) {

    const router = useRouter()

    const linksNav = [
        { label: "Meu Perfil", icon: <User size={20} />, href: "/cliente" },
        { label: "Carrinho", icon: <ShoppingCart size={20} />, href: "/cliente/carrinho" },
        { label: "Favoritos", icon: <Heart size={20} />, href: "/favoritos" },
        { label: "Meus Pedidos", icon: <Package size={20} />, href: "/pedidos" },
        { label: "Configurações", icon: <Settings size={20} />, href: "/configuracoes" },
    ]

    const handleLogout = async () => {
        // 1. Pergunta se quer sair
        const result = await Swal.fire({
            title: 'Deseja sair?',
            text: "Sua sessão será encerrada.",
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Sim, sair',
            cancelButtonText: 'Cancelar'
        });

        if (result.isConfirmed) {
            executeLogout(); // Chamamos a função que faz o trabalho pesado
        }
    };

    const executeLogout = async () => {
        const success = await logoutService();

        if (success) {
            await Swal.fire({
                title: 'Sucesso!',
                text: 'Até logo!',
                icon: 'success',
                timer: 1500,
                showConfirmButton: false
            });
            router.push('/');
            router.refresh();
        } else {
            // 2. Se falhar, oferece a opção de tentar de novo
            const retry = await Swal.fire({
                title: 'Erro ao sair',
                text: 'Não conseguimos processar seu logout. Quer tentar novamente?',
                icon: 'error',
                showCancelButton: true,
                confirmButtonText: 'Tentar de novo 🔄',
                cancelButtonText: 'Fechar'
            });

            if (retry.isConfirmed) {
                executeLogout(); // Recursividade: tenta executar a lógica de novo
            }
        }
    };

    return (
        <>
            <div className={`fixed inset-0 bg-black/50 z-40 transition-opacity duration-300 
            ${modalAberto ? "opacity-100 visible" : "opacity-0 invisible"}`}
                onClick={() => setModalAberto(false)} />

            <aside className={`fixed top-0 left-0 h-full w-70 bg-white z-50 shadow-xl transform transition-transform duration-300 ease-in-out flex flex-col ${modalAberto ? "translate-x-0" : "-translate-x-full"}`}>
                {/* 👤 Header: Avatar + Info + Botão Fechar */}
                <div className="p-6 border-b border-gray-300 flex items-center justify-between">
                    <div className="flex items-center gap-3">
                        <div className="w-12 h-12 bg-slate-200 rounded-full flex items-center justify-center text-slate-600">
                            <User size={24} />
                        </div>
                        <div className="flex flex-col">
                            <span className="font-bold text-slate-800 leading-tight">{nome}</span>
                            <span className="text-xs text-slate-500">{email}</span>
                        </div>
                    </div>
                    <button
                        onClick={() => setModalAberto(false)}
                        className="p-2 rounded-lg transition-colors text-slate-400 hover:text-red-600 cursor-pointer"
                    >
                        <X size={25} />
                    </button>
                </div>

                <nav className="p-4 flex flex-col gap-2">
                    {linksNav.map((item, index) => (
                        <Link
                            key={index}
                            href={item.href}
                            className="flex items-center gap-4 p-3 rounded-lg hover:bg-slate-200/80 text-slate-600 hover:text-slate-900 transition-all group"
                        >
                            <span className="group-hover:scale-110 transition-transform">{item.icon}</span>
                            <span className="text-sm font-medium">{item.label}</span>
                        </Link>
                    ))}
                </nav>

                <div className="mt-auto p-6 border-gray-300 border-t ">
                    <button className="flex items-center gap-3 text-red-500 font-semibold hover:bg-red-100/80 w-full py-2 px-3 rounded-lg cursor-pointer transition-all group"
                        onClick={handleLogout}
                    >
                        <span className="group-hover:scale-110 transition-transform">
                            <LogOut size={20} />
                        </span>
                        <span>Sair</span>
                    </button>
                </div>

            </aside>
        </>
    )
}