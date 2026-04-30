import { ApiResponse } from "./apiResponse"
import { Page } from "./pageResponse"

export type Produto = {
    id: number,
    nome: string,
    descricao: string,
    preco: number,
    estoque: number,
    ativo: boolean,
    categoriaNome: string,
    categoriaId: number,
}


export type ProdutoResponse = ApiResponse<Page<Produto>>
