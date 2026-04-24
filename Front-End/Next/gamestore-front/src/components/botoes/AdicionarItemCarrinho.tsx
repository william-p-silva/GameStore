"use client"

import { AdicionarItemCarrinho } from "@/services/carrinho/adicionarItemCarrinho"

interface Props{
    id: number
}


export function AdicionarCarrinho({id}: Props) {
    async function handleAdd() {
        try {
            await AdicionarItemCarrinho(id);
            
            alert("Adicionado ao carrinho");
            window.location.reload();
        } catch {
            alert("Erro ao adicionar");
        }
    }

    return(
        <button 
      onClick={handleAdd}
      className="mt-2 bg-blue-500 text-white p-2 rounded cursor-pointer"
    >
      Adicionar ao carrinho
    </button>
    )
}