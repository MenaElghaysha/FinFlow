const API_URL = "http://localhost:5155"; // change to your real port

// API helper function
//بتخلّيك تتعامل مع الـ .NET Backend من غير ما تكرر كود fetch في كل صفحة.

export async function api(path: string, options = {}) {

  const res = await fetch(`${API_URL}${path}`, {
    headers: { "Content-Type": "application/json" },
    ...options,
  });
  if (!res.ok) {
    const err = await res.json().catch(() => ({}));
    // Better error message
    if (err.errors) {
      const messages = Object.values(err.errors).flat().join("\n");
      throw new Error(messages);
    }
    throw new Error(err.detail ||err.title ||"Something went wrong");
  }
  if (res.status === 204) return null;  // No Content
  return res.json();
}