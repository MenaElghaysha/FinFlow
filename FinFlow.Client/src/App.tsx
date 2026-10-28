// BrowserRouter → بيخلّي React يتعامل مع الـ URLs بتاعة المتصفح.

// Routes → بيجمع الـ Routes الموجودة في التطبيق.

// Route → بيربط URL بصفحة أو Component معين.

import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./components/Layout";
import Dashboard from "./pages/Dashboard";
import Customers from "./pages/Customers";
import Loans from "./pages/Loans";

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Dashboard />} />  // Default page when visiting "/"
          <Route path="customers" element={<Customers />} />
          <Route path="loans" element={<Loans />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}