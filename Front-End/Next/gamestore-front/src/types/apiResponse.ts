


export type ApiResponse<T> ={
    sucesso: boolean,
    dados: T,        // T já está disponível aqui por ter sido declarado em ApiResponse<T>
    erros: string[]
}