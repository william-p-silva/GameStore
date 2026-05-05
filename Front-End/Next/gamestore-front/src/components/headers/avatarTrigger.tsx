"use client"


import { useMenu } from "./UserControl"; // Importamos o hook que criamos
import { Menu } from "lucide-react";

interface AvatarTriggerProps {
    nome: string,
    email: string
}

export function AvatarTrigger({ nome, email }: AvatarTriggerProps) {
    const { abrirMenu } = useMenu();

    return (
        <div className="flex items-center gap-4">
            <div className="flex items-center gap-2 cursor-pointer  transition-opacity">
                <div onClick={abrirMenu} className="w-9 h-9 bg-gray-800 text-white rounded-full flex items-center justify-center font-bold text-sm hover:opacity-80">
                    {nome.charAt(0).toUpperCase()}
                </div>
                <span className="text-gray-800 font-medium hidden md:block">
                    Olá, {nome.split(" ")[0]}
                </span>
            </div>
        </div>
    );
}