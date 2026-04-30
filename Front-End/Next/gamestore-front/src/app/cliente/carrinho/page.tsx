"use client"

import { RemoverProduto } from "@/components/botoes/removerProduto";
import { ListarItensDoCarrinho } from "@/services/carrinho"
import { CarrinhoResponse } from "@/types/carrinho";
import { useEffect, useState } from "react";




export default function Carrinho() {

    const [carrinho, setCarrinho] = useState<CarrinhoResponse | null>(null)

    useEffect(() => {
        async function ListarCarrinho() {
            const data = await ListarItensDoCarrinho();
            setCarrinho(data)
        }
        ListarCarrinho()
    }, []);

    if (carrinho == null) return <p>Carregando ...</p>

    const itens = carrinho.dados.itens;
    const frete = carrinho.dados.totalCarrinho * 0.20;

    return (
        <div className="w-full p-4">
            {itens.length > 0 ? (
                <>
                    {itens.map((p) => (
                        <div key={p.produtoId} >
                            <h2 className="text-lg font-bold">{p.nomeProduto}</h2>
                            <p className="text-gray-50">R$ {p.precoUnitario}</p>
                            <p className="text-gray-50">Quantidade: {p.quantidade}</p>
                            <p className="text-gray-50">Subtotal: R$ {p.subTotal}</p>
                            
                        </div>
                    ))}
                </>
            ) : (
                <p>Você ainda não tem itens no carrinho</p>
            )}
            <div className="w-full p-2 bg-gray-400 my-3 rounded-2xl">
                <p>Produtos: {carrinho.dados.totalItens}</p>
                <p>Total: R$ {carrinho.dados.totalCarrinho}</p>
                <p>Frete: R$ {frete.toFixed(2)}</p>
            </div>
        </div>

    )
}