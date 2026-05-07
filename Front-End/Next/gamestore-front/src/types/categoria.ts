import { ApiResponse } from "./apiResponse"
import { Page } from "./pageResponse"

export type Categoria = {
    id: number,
    nome: string,
    produtos: ProdutoCategoria[]
    quantidadeProdutos: number
}

export type ProdutoCategoria = {
    id: number,
    nome: string,
    preco: number,
    estoque: number,
    ativo: boolean
}

export type CategoriaResponse = ApiResponse<Page<Categoria>>