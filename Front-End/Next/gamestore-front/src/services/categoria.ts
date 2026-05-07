import { apiRequest } from "@/lib/api-client";
import { CategoriaResponse } from "@/types/categoria";

export async function GetCategorias(): Promise<CategoriaResponse[]> {
    const response = await apiRequest("Categorias",{
        method: "GET",
    }) as CategoriaResponse[]
    return response
}