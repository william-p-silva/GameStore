

export async function apiRequest(path: string, options?: RequestInit, isPublic: boolean = false) {
    const response = await fetch(`/api/proxy/${path}`, {
        headers: {
            "Content-Type": "application/json",
            "x-public-route": isPublic ? "true" : "false",
        },
        ...options,
    });


    // Se o servidor retornar 401, o token expirou ou não existe.
    // Redirecionamos para o login de forma centralizada — assim
    // nenhum service ou componente precisa se preocupar com isso.
    if (response.status === 401) {
        window.location.href = "/login";
        // Lançamos o erro mesmo assim para interromper a execução
        // da função que chamou o apiRequest.
        throw new Error("Sessão expirada. Redirecionando para o login...");
    }

    if (!response.ok) throw new Error(`Erro: ${response.status}`);
    
    return response.json();

}