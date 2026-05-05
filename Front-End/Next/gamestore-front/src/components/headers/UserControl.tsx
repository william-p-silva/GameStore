"use client"

import { Menu } from "lucide-react"
import { createContext, useContext, useState, ReactNode } from "react";
import { MenuOpcoes } from "../modais/modalOpcoes"


interface UserControlProps {
    nome: string,
    email: string,
    children: ReactNode // Isso permite que o componente envolva outros elementos
}

// 1. Criamos a interface para o TypeScript saber o que o canal transmite
interface MenuContextType {
    abrirMenu: () => void;
}

// 2. Criamos o contexto propriamente dito
const MenuContext = createContext<MenuContextType | undefined>(undefined);

// Este hook será usado pelo Avatar ou pelo botão de Menu
export function useMenu() {
    const context = useContext(MenuContext);
    if (!context) {
        throw new Error("useMenu deve ser usado dentro de um UserControl");
    }
    return context;
}

export function UserControl({ nome, email, children }: UserControlProps) {
    const [isOpen, setIsOpen] = useState(false);

    const abrirMenu = () => setIsOpen(true);

    return (
        <MenuContext.Provider value={{ abrirMenu }}>
            {/* O children aqui permite que o Header passe os elementos pra cá */}
            {children}

            <MenuOpcoes
                email={email}
                nome={nome}
                modalAberto={isOpen}
                setModalAberto={setIsOpen}
            />
        </MenuContext.Provider>
    );
}