import { ApiResponse } from "./apiResponse"


export type Carrinho = {
    id: number,
    usuarioId: number,
    usuarioEmail: string,
    itens: CarrinhoItens[]
    totalItens: number,
    totalCarrinho: number,
};


export type CarrinhoItens = {
    produtoId: number,
    nomeProduto: string,
    quantidade: number,
    estoque: number,
    precoUnitario: number,
    subTotal: number,
};

export type CarrinhoResponse = ApiResponse<Carrinho>;