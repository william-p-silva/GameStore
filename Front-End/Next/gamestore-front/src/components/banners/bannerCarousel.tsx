"use client";

import React, { useCallback, useEffect, useState } from "react";
import useEmblaCarousel from "embla-carousel-react";
import Autoplay from "embla-carousel-autoplay";
import Image from "next/image";

// O seu objeto conforme solicitado
const BANNERS = [
  { id: 1, src: "/banners/game-1.jpg", alt: "Promoção de Lançamento" },
  { id: 2, src: "/banners/game-2.jpg", alt: "Descontos de Inverno" },
  { id: 3, src: "/banners/game-3.jpg", alt: "Novos Consoles" },
  { id: 4, src: "/banners/game-4.jpg", alt: "Jogos Esportivos" },
];

export default function BannerCarousel() {
  // Configuração do Carrossel com Autoplay de 4 segundos
  const [emblaRef, emblaApi] = useEmblaCarousel({ loop: true }, [
    Autoplay({ delay: 4000, stopOnInteraction: false })
  ]);
  
  const [selectedIndex, setSelectedIndex] = useState(0);

  // Lógica para sincronizar as bolinhas com o slide atual
  const onSelect = useCallback(() => {
    if (!emblaApi) return;
    setSelectedIndex(emblaApi.selectedScrollSnap());
  }, [emblaApi]);

  useEffect(() => {
    if (!emblaApi) return;
    onSelect();
    emblaApi.on("select", onSelect);
  }, [emblaApi, onSelect]);

  const scrollTo = useCallback((index: number) => emblaApi && emblaApi.scrollTo(index), [emblaApi]);

  return (
    <section className="relative w-full group">
      {/* Container Principal com bordas arredondadas */}
      <div className="overflow-hidden  shadow-xl " ref={emblaRef}>
        <div className="flex">
          {BANNERS.map((banner) => (
            <div 
              key={banner.id} 
              className="relative flex-[0_0_100%] min-w-0 h-[250px] md:h-[400px]"
            >
              {/* Image consertada: object-cover evita que a foto fique "esticada" */}
              <Image
                src={banner.src}
                alt={banner.alt}
                fill
                className="object-cover transition-transform duration-700"
                priority={banner.id === 1}
              />
              
              {/* Overlay escuro opcional para dar leitura se houver texto futuro */}
              <div className="absolute inset-0 bg-black/50 transition-colors" />
            </div>
          ))}
        </div>
      </div>

      {/* Bolinhas (Dots) Discretas e Elegantes */}
      <div className="absolute bottom-5 left-1/2 -translate-x-1/2 flex gap-1.5">
        {BANNERS.map((_, index) => (
          <button
            key={index}
            onClick={() => scrollTo(index)}
            className={`h-2 rounded-full transition-all duration-500 cursor-pointer ${
              selectedIndex === index 
                ? "w-4 bg-gray-600" // Ativa: mais larga e colorida
                : "w-2 bg-white/40 hover:bg-gray-600/70" // Inativa: pequena e opaca
            }`}
            aria-label={`Ver slide ${index + 1}`}
          />
        ))}
      </div>
    </section>
  );
}