import { useEffect, useState } from "react";
import { api } from "../api/client";

export default function Dashboard() {

  const [stats, setStats] = useState({
    customers: 0,
    loans: 0,
    pending: 0,
    approved: 0,
  });
  const [recentLoans, setRecentLoans] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const load = async () => {
      try {
        const [customers, loans] = await Promise.all([
          api("/api/customers"),
          api("/api/loans"),
        ]);

        setStats({
          customers: customers.length,
          loans: loans.length,
          pending: loans.filter((l: any) => l.status === "Pending").length,
          approved: loans.filter((l: any) => l.status === "Approved").length,
        });

      setRecentLoans(
        //slice(0, 5) → بيجيب أول 5 عناصر من المصفوفة
        loans.slice(0, 5).map((loan: any) => ({
          ...loan,
          customerFullName:
            customers.find((c: any) => c.id === loan.customerId)?.fullName
            ?? "Unknown Customer",
        }))
);
      } catch (err) {
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    load();
  }, []);

  if (loading) return <p>Loading dashboard...</p>;
  
  return (
    <div>
      <h2 style={{ marginBottom: 20 }}>Dashboard</h2>

      {/* Summary cards */}
      <div style={{ display: "grid", gridTemplateColumns: "repeat(4, 1fr)", gap: 16, marginBottom: 30 }}>
        <Card title="Total Customers" value={stats.customers.toString()} />
        <Card title="Total Loans" value={stats.loans.toString()} />
        <Card title="Pending" value={stats.pending.toString()} color="#f59e0b" />
        <Card title="Approved" value={stats.approved.toString()} color="#10b981" />
      </div>

      {/* Recent loans table */}
      <div style={{ background: "white", borderRadius: 8, padding: 20 }}>
        <h3 style={{ marginTop: 0 }}>Recent Loans</h3>
        <table style={{ width: "100%", borderCollapse: "collapse" }}>
          <thead>
            <tr style={{ textAlign: "center", borderBottom: "1px solid #e2e8f0" }}>
              <th style={{ padding: 10 }}>Amount</th>
              <th style={{ padding: 10 }}>Customer
              </th>
              <th>Purpose</th>
              <th>Status</th>
              <th>Date</th>
            </tr>
          </thead>
          <tbody>
            {recentLoans.map((loan ) => (
              <tr key={loan.id} 
              style={{ borderBottom: "1px solid #f1f5f9" }}
              >

                <td style={{ padding: 10 }}>
                  {loan.amount.toLocaleString()} EGP
                  </td>

                <td style={{ padding: 10 }}>
                  {loan.customerFullName}
                  </td>

                <td>{loan.purpose}</td>

                <td>{loan.status}</td>
          
                <td>{new Date(loan.createdAt).toLocaleDateString()}</td>

              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

function Card({ title, value, color = "#3b82f6" }: { title: string; value: string; color?: string }) {
  return (
    <div style={{ background: "white", padding: 20, borderRadius: 8, boxShadow: "0 1px 3px rgba(0,0,0,0.1)" }}>
      <div style={{ fontSize: 14, color: "#64748b" }}>{title}</div>
      <div style={{ fontSize: 28, fontWeight: "bold", color, marginTop: 8 ,  }}>{value}</div>
    </div>
  );
}