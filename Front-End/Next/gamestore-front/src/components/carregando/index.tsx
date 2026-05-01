

export function Carregando(){
    return(
    <div className="max-w-4xl mx-auto p-6 space-y-4">
      {/* Título Skeleton */}
      <div className="h-8 w-48 bg-gray-200 rounded-md animate-pulse mb-6"></div>
      
      {[1, 2, 3].map((i) => (
        <div key={i} className="flex items-center justify-between p-4 border border-gray-100 rounded-xl bg-gray-50">
          <div className="space-y-3 flex-1">
            <div className="h-4 w-3/4 bg-gray-200 rounded animate-pulse"></div>
            <div className="h-3 w-1/2 bg-gray-200 rounded animate-pulse"></div>
          </div>
          <div className="h-10 w-20 bg-gray-200 rounded-lg animate-pulse"></div>
        </div>
      ))}
      
      {/* Resumo Skeleton */}
      <div className="mt-8 h-40 bg-gray-200 rounded-2xl animate-pulse"></div>
    </div>
  );
}