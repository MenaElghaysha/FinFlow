//Layout.jsx : ده الملف اللى بيحتوى على الـ Layout العام للصفحات كلها

// Link : للتنقل بين الصفحات من غير ما تعمل Reload للصفحة كلها
//Outlet : المكان اللى react roure بيعرض فيه الصفحات الحاليه حسب URL
import { Link, Outlet } from "react-router-dom";

// Layout component with sidebar and top bar
// 'export default' is a syntax used to share a single main value (like a function, class, or object)
//  from one file so it can be used in another.
export default function Layout() {
  return (
    // Flex container for sidebar and main content
    <div
      style={{ display: "flex", minHeight: "100vh", fontFamily: "sans-serif" }}
    >
      {/* Sidebar */}
      <aside
        style={{
          width: 220,
          background: "#1e293b",
          color: "white",
          padding: 20,
        }}
      >
        <h2 style={{ marginBottom: 30, color: "#ffb650" }}>
          FinFlow
        </h2>
        <nav style={{ display: "flex", flexDirection: "column", gap: 12 }}>
          <Link to="/" style={{ color: "white", textDecoration: "none" }}>
            Dashboard
          </Link>
          <Link
            to="/customers"
            style={{ color: "white", textDecoration: "none" }}
          >
            Customers
          </Link>
          <Link to="/loans" style={{ color: "white", textDecoration: "none" }}>
            Loans
          </Link>
        </nav>
      </aside>

      {/* Main content */}
      <div style={{ flex: 1, background: "#f1f5f9" }}>
        {/* Top bar */}
        <header
          style={{
            background: "white",
            padding: "16px 24px",
            borderBottom: "1px solid #e2e8f0",
          }}
        >
          <h1 style={{ margin: 0, fontSize: 20 }}>Loan Management</h1>
        </header>

        <main style={{ padding: 24 }}>
          <Outlet /> // This is where the current page content will be rendered
          based on the route
        </main>
        
      </div>
    </div>
  );
}
