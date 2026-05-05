"use client"

import { RemoverItemDoCarrinho } from "@/services/carrinho";

interface Props{
  produtoId: number
  onRemovido: () => void
}

export function RemoverProduto({produtoId, onRemovido}: Props){
       async function handleAdd() {
            try {
                await RemoverItemDoCarrinho(produtoId)
                onRemovido()
                alert("Removido do carrinho");
            } catch {
                alert("Erro ao remover");
            }
        }
return(
    <button 
    onClick={handleAdd}
    className="mt-2 bg-blue-500 text-white p-2 rounded cursor-pointer"
  >
    Remover do carrinho
  </button>
)

}