

export async function AdicionarItemCarrinho(produtoID: number) {

    const quantidade = 1;
    console.log("ENVIANDO:", produtoID, quantidade); // 👈 AQUI
    const response = await fetch("/api/carrinho/adicionar",{
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            
        },
        body: JSON.stringify({produtoID, quantidade}),
    });

    if (!response.ok)
        throw new Error("Erro ao adicionar item no carrinho.")

     const data = await response.json();

     return data;
}