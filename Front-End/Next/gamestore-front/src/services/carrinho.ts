import { apiRequest } from "@/lib/api-client";
import { CarrinhoResponse } from "@/types/carrinho";


export async function AdicionarItemAoCarrinho(produtoID: number): Promise<CarrinhoResponse> {
    const data = await apiRequest("Carrinho/adicionarItem",{
        method: "POST",
        body: JSON.stringify({produtoID, quantidade: 1})
    }) as CarrinhoResponse;
    return data
}

export async function RemoverItemDoCarrinho(produtoId: number): Promise<CarrinhoResponse> {
    const data = await apiRequest("Carrinho/removerItem", {
        method: "DELETE",
        body: JSON.stringify({produtoId}),
    }) as CarrinhoResponse;
    return data;
}

export async function ListarItensDoCarrinho(): Promise<CarrinhoResponse> {
    const data = await apiRequest("Carrinho/carrinho") as CarrinhoResponse;
    return data;
}

export async function AtualizarQuantidadeProduto(produtoId: number, quantidade: number): Promise<CarrinhoResponse> {
    const data = await apiRequest("Carrinho/atualizarItem", {
        method: "PUT",
        body: JSON.stringify({produtoId, quantidade})
    }) as CarrinhoResponse;

    return data;
}