
import { AdicionarCarrinho } from "@/components/botoes/AdicionarItemCarrinho";

import { GetProdutos } from "@/services/produtos";

export default async function ProdutosPage() {
    const data = await GetProdutos();
    const produtos = data.dados.data ?? data;


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

                           <AdicionarCarrinho id={p.id}  />

                    </div>

                ))}
            </div>
        </div>
    );
}