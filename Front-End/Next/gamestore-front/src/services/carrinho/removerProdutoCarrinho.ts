
export async function RemoverProdutoCarrinho(produtoId: number) {
    const response = await fetch("/api/carrinho/remover",{
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({produtoId})
    })

    if (!response.ok)
        throw new Error("Erro ao remover item no carrinho.")

     return response.json();
}