import { cookies } from "next/headers";
import { NextRequest, NextResponse } from "next/server";

const API_BASE = process.env.NEXT_PUBLIC_API_URL;

async function handler(
    req: NextRequest,
    { params }: { params: Promise<{ path: string[] }> } // ← Promise aqui
) {

    const isPublic = req.headers.get("x-public-route") === "true";

    const headers: Record<string, string> = {
        "Content-Type": "application/json",
    };

    if (!isPublic) {
        const cookieStore = await cookies();
        const token = cookieStore.get("token")?.value;
        if (!token) {
            return NextResponse.json({ error: "Não autorizado" }, { status: 401 });
        }    
        headers["Authorization"] = `Bearer ${token}`;
    }

    const { path: pathSegments } = await params; // ← await aqui
    const path = pathSegments.join("/");

    const search = req.nextUrl.searchParams.toString();
    const url = `${API_BASE}/${path}${search ? `?${search}` : ""}`;

    const isJson = req.headers.get("content-type")?.includes("application/json");

    const response = await fetch(url, {
        method: req.method,
        headers,
        body:
            req.method !== "GET" && req.method !== "HEAD" && isJson
                ? await req.text()
                : undefined,
    });

    const contentType = response.headers.get("Content-Type");
    const data = contentType?.includes("application/json")
        ? await response.json()
        : await response.text();

    return NextResponse.json(data, { status: response.status });
}

export const GET = handler;
export const POST = handler;
export const PUT = handler;
export const DELETE = handler;
export const PATCH = handler;