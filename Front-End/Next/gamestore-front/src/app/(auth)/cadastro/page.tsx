'use client'

import { Cadastro } from "@/services/cadastro";
import { useState } from "react"



export default function CadastroPage() {

    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");
    const [nome, setNome] = useState("");
    const [loanding, setLoanding] = useState(false);

    async function handleCadastro(e: React.FormEvent) {
        e.preventDefault();
        try {
            setLoanding(true);
            const data = await Cadastro(nome, email, senha);
            alert("Cadastro realizado com sucesso");
        }
        catch (error) {
            console.error(error);
            alert("Erro ao cadastrar usuário");
        }
        finally{
            setLoanding(false);
        }
    }
    return (
        <div className="flex items-center justify-center h-screen">
            <form
                onSubmit={handleCadastro}
                className="bg-white p-6 rounded shadow-md w-80"
            >
                <h1 className="text-xl mb-4">Cadastro</h1>

                <input
                    type="text"
                    placeholder="Nome"
                    className="w-full mb-3 p-2 border"
                    value={nome}
                    onChange={(e) => setNome(e.target.value)}
                />

                <input
                    type="email"
                    placeholder="Email"
                    className="w-full mb-3 p-2 border"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />


                <input
                    type="password"
                    placeholder="Senha"
                    className="w-full mb-3 p-2 border"
                    value={senha}
                    onChange={(e) => setSenha(e.target.value)}
                />


                <button
                    type="submit"
                    className="w-full bg-blue-500 text-white p-2"
                >
                    Cadastro
                </button>
            </form>
        </div>
    )
}