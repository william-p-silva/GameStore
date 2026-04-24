"use client"

import { RemoverProdutoCarrinho } from "@/services/carrinho/removerProdutoCarrinho";

interface Props{
  produtoId: number
}




export function RemoverProduto({produtoId}: Props){
       async function handleAdd() {
            try {
                await RemoverProdutoCarrinho(produtoId)
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