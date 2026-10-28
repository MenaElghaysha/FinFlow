const mockLoans = [
  { id: "1", customer: "Ahmed Ali", amount: 50000, status: "Pending", date: "2025-10-20" },
  { id: "2", customer: "Sara Mohamed", amount: 30000, status: "Approved", date: "2025-10-18" },
  { id: "3", customer: "Omar Hassan", amount: 75000, status: "Rejected", date: "2025-10-15" },
];

export default function Dashboard() {
  return (
    <div>
      <h2 style={{ marginBottom: 20 }}>Dashboard</h2>

      {/* Summary cards */}
      <div style={{ display: "grid", gridTemplateColumns: "repeat(4, 1fr)", gap: 16, marginBottom: 30 }}>
        <Card title="Total Customers" value="12" />
        <Card title="Total Loans" value="28" />
        <Card title="Pending" value="5" color="#f59e0b" />
        <Card title="Approved" value="18" color="#10b981" />
      </div>

      {/* Recent loans table */}
      <div style={{ background: "white", borderRadius: 8, padding: 20 }}>
        <h3 style={{ marginTop: 0 }}>Recent Loans</h3>
        <table style={{ width: "100%", borderCollapse: "collapse" }}>
          <thead>
            <tr style={{ textAlign: "-khtml-center", borderBottom: "1px solid #e2e8f0" }}>
              <th style={{ padding: 10 }}>Customer</th>
              <th>Amount</th>
              <th>Status</th>
              <th>Date</th>
            </tr>
          </thead>
          <tbody>
            {mockLoans.map((loan) => (
              <tr key={loan.id} style={{ borderBottom: "1px solid #f1f5f9" }}>
                <td style={{ padding: 10 }}>{loan.customer}</td>
                <td>{loan.amount.toLocaleString()} EGP</td>
                <td>
                  <span style={{
                    padding: "4px 10px",
                    borderRadius: 12,
                    fontSize: 12,
                    background: loan.status === "Approved" ? "#d1fae5" :
                                loan.status === "Pending" ? "#fef3c7" : "#fee2e2",
                    color: loan.status === "Approved" ? "#065f46" :
                           loan.status === "Pending" ? "#92400e" : "#991b1b"
                  }}>
                    {loan.status}
                  </span>
                </td>
                <td>{loan.date}</td>
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
      <div style={{ fontSize: 28, fontWeight: "bold", color, marginTop: 8 }}>{value}</div>
    </div>
  );
}