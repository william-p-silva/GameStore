"use client"


import BannerCarousel from "@/components/banners/bannerCarousel";
import { AdicionarCarrinho } from "@/components/botoes/AdicionarItemCarrinho";
import { CarregandoPadrao } from "@/components/carregando/padrao";
import { GetCategorias } from "@/services/categoria";
import { GetProdutos } from "@/services/produtos";
import { CategoriaResponse } from "@/types/categoria";
import { Produto } from "@/types/produto";
import { useEffect, useState } from "react";

export default function ProdutosPage() {

    const [produtos, setProdutos] = useState<Produto[] | null>(null)
    const [categoriaAtiva, setCategoriaAtiva] = useState(null)
    const [categoria, setCategorias] = useState<CategoriaResponse[] | null>(null)


    useEffect(() => {
        async function listarProdutos() {
            const response = await GetProdutos();
            // response.dados.data é onde está a lista de produtos
            setProdutos(response.dados.data);
        }
        async function ListarCategorias() {
            const response = await GetCategorias();
            setCategorias(response.dados.data);
            
        }
        ListarCategorias()
        listarProdutos()
    }, []);

    // enquanto os dados não chegam, mostra loading
    if (!produtos) return <CarregandoPadrao /> ;

    return (
        <>
        
        <BannerCarousel />

        <div className="flex items-center justify-center mt-3 border-t border-gray-400/60 p-4 shadow-sm shadow-gray-400/60">
            {produtos.map((c: any) => (
                <button
                    key={c.id}
                    onClick={() => setCategoriaAtiva(c.categoriaId)}
                    className={`font-bold cursor-pointer transition ease-linear duration-200 px-4 py-2 rounded-lg mr-2 ${categoriaAtiva === 
                        c.categoriaId ? "bg-cinza text-white" : "bg-gray-300/50 text-cinza/70 hover:bg-gray-500/50"}`}
                >
                    {c.categoriaNome}
                </button>
            ))}

        </div>

        <div className="p-6">          

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
                        <AdicionarCarrinho id={p.id} />

                    </div>

                ))}
            </div>
        </div>
        
        </>
    );
}