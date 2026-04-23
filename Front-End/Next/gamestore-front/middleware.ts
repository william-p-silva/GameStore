import { NextResponse } from "next/server";
import type { NextRequest } from "next/server";
import { parseJwt } from "./src/lib/auth";

export function middleware(req: NextRequest) {
    const token = req.cookies.get("token")?.value;

    if (!token) {
        return NextResponse.redirect(new URL("/login", req.url));
    }

    const user = parseJwt(token);
    const pathname = req.nextUrl.pathname;
    if (pathname.startsWith("/admin") && user.role !== "Admin") {
        return NextResponse.redirect(new URL("/cliente", req.url));
    }
    return NextResponse.next();
}
export const config = {
    matcher: ["/admin/:path*", "/cliente/:path*"],
};