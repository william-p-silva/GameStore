"use client"

import { useMenu } from "./UserControl"; // Importamos o hook que criamos
import { Menu } from "lucide-react";

export function MenuTrigger() {
  const { abrirMenu } = useMenu();

  return (
    <button onClick={abrirMenu} className="cursor-pointer">
      <Menu />
    </button>
  );
}