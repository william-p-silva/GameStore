import { apiRequest } from "@/lib/api-client";
import { ProdutoResponse } from "@/types/produto";



export async function GetProdutos(): Promise<ProdutoResponse> {
    const data = await apiRequest("Produtos", {}, true)
    return data;
}