import { RemoverProduto } from "@/components/botoes/removerProduto";
import { ListarCarrinho } from "@/services/carrinho/listarItensCarrinho";



export default async function Carrinho() {


    const data = await ListarCarrinho();
    if (!data.sucesso) {
        console.log("Erro na API")
        throw new Error("Erro na API")
    }
    const itens = data.dados.itens ?? data;
    const carrinho = data.dados ?? data;

    const frete = Math.round((carrinho.totalCarrinho * 0.20) * 100) /100

    return (
        <div className="w-full p-4">
            {itens != null && (
                <>
                    {itens.map((p: any) => (
                        <div
                            key={p.produtoId}
                            className="w-full p-2 bg-gray-400 my-3 rounded-2xl"
                        >

                            <h2 className="text-lg font-bold">{p.nomeProduto}</h2>
                            <p className="text-gray-50">R$ {p.produtoId}</p>
                            <p className="text-gray-50">R$ {p.precoUnitario}</p>
                            <p className="text-gray-50">Quantidade: {p.quantidade}</p>
                            <p className="text-gray-50">R$ {p.subTotal}</p>
                            <RemoverProduto produtoId={p.produtoId} />
                        </div>

                    ))
                    }
                </>
            )}
            <div className="w-full p-2 bg-gray-400 my-3 rounded-2xl">
                <p>Produtos:{carrinho.totalItens}</p>
                <p>Total: R${carrinho.totalCarrinho}</p>
                <p>frete: {frete}</p>
            </div>
            {itens == null && (
                <div>
                    <p>Você ainda não tem produtos no carrinho</p>
                </div>
            )}
        </div>

    )
}