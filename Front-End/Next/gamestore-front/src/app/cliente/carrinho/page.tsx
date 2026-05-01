"use client"

import { RemoverProduto } from "@/components/botoes/removerProduto";
import { Carregando } from "@/components/carregando";
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

    if (carrinho == null) return <Carregando />;

    const itens = carrinho.dados.itens;
    const frete = carrinho.dados.totalCarrinho * 0.20;

    return (
        <div className="max-w-4xl mx-auto p-6 bg-white rounded-lg shadow-sm">
            <h1 className="text-2xl font-semibold mb-6 text-gray-800 border-b pb-4">Seu Carrinho</h1>

            {itens.length > 0 ? (
                <div className="space-y-4">
                    {itens.map((p) => (
                        <div
                            key={p.produtoId}
                            className="flex flex-col md:flex-row md:items-center justify-between p-4 border border-gray-100 rounded-xl bg-gray-50 hover:shadow-md transition-shadow"
                        >
                            <div className="flex-1">
                                <h2 className="text-lg font-bold text-gray-900">{p.nomeProduto}</h2>
                                <div className="flex gap-4 mt-1 text-sm text-gray-600">
                                    <p>Qtd: <span>-</span> <span className="font-medium">{p.quantidade}</span> <span>+</span> </p>
                                    <p>Unitário: <span className="font-medium text-emerald-600">R$ {p.precoUnitario}</span></p>
                                    <RemoverProduto produtoId={p.produtoId} />
                                </div>
                            </div>

                            <div className="mt-3 md:mt-0 text-right">
                                <p className="text-xs text-gray-500 uppercase tracking-wider">Subtotal</p>
                                <p className="text-xl font-bold text-gray-900">R$ {p.subTotal}</p>
                            </div>
                        </div>
                    ))}
                </div>
            ) : (
                <div className="text-center py-10 border-2 border-dashed border-gray-200 rounded-xl">
                    <p className="text-gray-500 italic">Você ainda não tem itens no carrinho.</p>
                </div>
            )}

            {/* Resumo Financeiro */}
            <div className="mt-8 p-6 bg-slate-900 text-white rounded-2xl shadow-lg">
                <h3 className="text-lg font-medium mb-4 border-b border-slate-700 pb-2">Resumo do Pedido</h3>

                <div className="space-y-2 text-slate-300">
                    <div className="flex justify-between">
                        <span>Produtos ({carrinho.dados.totalItens})</span>
                        <span>R$ {carrinho.dados.totalCarrinho}</span>
                    </div>
                    <div className="flex justify-between">
                        <span>Frete</span>
                        <span className="text-emerald-400">R$ {frete.toFixed(2)}</span>
                    </div>

                    <div className="flex justify-between pt-4 mt-2 border-t border-slate-700 text-xl font-bold text-white">
                        <span>Total</span>
                        <span>R$ {(parseFloat(carrinho.dados.totalCarrinho.toString()) + frete).toFixed(2)}</span>
                    </div>
                </div>

                <button className="w-full mt-6 bg-emerald-500 hover:bg-emerald-600 text-white font-bold py-3 rounded-xl transition-colors shadow-lg shadow-emerald-900/20">
                    Finalizar Compra
                </button>
            </div>
        </div>

    )
}