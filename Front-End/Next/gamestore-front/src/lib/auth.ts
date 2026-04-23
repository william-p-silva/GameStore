export function parseJwt(token: string) {
    try {
        const base64Payload = token.split(".")[1];

        const base64 = base64Payload
            .replace(/-/g, "+")
            .replace(/_/g, "/");

        const payload = Buffer.from(base64, "base64").toString("utf-8");

        return JSON.parse(payload);
    } catch {
        return null;
    }
}