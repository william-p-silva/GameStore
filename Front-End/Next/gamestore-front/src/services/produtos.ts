


export async function GetProdutos() {
    const res = await fetch("http://localhost:5248/api/Produtos",{
    cache: "no-store",
    });

    if (!res.ok) {
        throw new Error("Erro ao buscar produtos");
    }

    return res.json();
}