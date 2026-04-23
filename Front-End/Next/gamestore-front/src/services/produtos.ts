


export async function GetProdutos() {
    const res = await fetch("https://localhost:7220/api/produtos",{
    cache: "no-store",
    });

    if (!res.ok) {
        throw new Error("Erro ao buscar produtos");
    }

    return res.json();
}