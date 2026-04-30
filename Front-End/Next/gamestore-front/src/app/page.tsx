"use client"


import { GetProdutos } from "@/services/produtos";
import { Produto } from "@/types/produto";
import { useEffect, useState } from "react";

export default function ProdutosPage() {

    const [produtos, setProdutos] = useState<Produto[] | null>(null)


    useEffect(() => {
        async function listarProdutos() {
            const response = await GetProdutos();
            // response.dados.data é onde está a lista de produtos
            setProdutos(response.dados.data);
        }
        listarProdutos()
    }, []);

    // enquanto os dados não chegam, mostra loading
    if (!produtos) return <p>Carregando produtos...</p>;

    return (
        <div className="p-6">
            <h1 className="text-2xl mb-6">Produtos</h1>

            <div className="grid grid-cols-4 gap-4">
                {produtos.map((p: any) => (
                    <div
                        key={p.id}
                        className="border p-4 rounded shadow"
                    >
                        <h2 className="text-lg font-bold">{p.nome}</h2>
                        <p className="text-gray-600">{p.descricao}</p>
                        <p className="text-gray-600">R$ {p.preco}</p>
                        <p className="text-gray-600">Estoque: {p.estoque}</p>
                        <p className="text-gray-600">Categoria: {p.categoriaNome}</p>
                        <p className="text-gray-600">ID: {p.id}</p>



                    </div>

                ))}
            </div>
        </div>
    );
}