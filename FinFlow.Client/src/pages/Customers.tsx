import { useEffect, useState } from "react";
import { api } from "../api/client";

type Customer = {
  id: string;
  fullName: string;
  email: string;
  nationalId: string;
  monthlyIncome: number;
};


// useState → React hook to manage state in functional components.

export default function Customers() {
  const [customers, setCustomers] = useState<Customer[]>([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);

  // form state
  const [fullName, setFullName] = useState("");
  const [email, setEmail] = useState("");
  const [nationalId, setNationalId] = useState("");
  const [monthlyIncome, setMonthlyIncome] = useState("");

  const loadCustomers = async () => {
    try {
      // For now we only have Create, so we keep a local list
      // Later we can add GetAllCustomers endpoint
    const data = await api("/api/customers");
      setCustomers(data);
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadCustomers();
  }, []);


  // handleCreate → Function to handle the creation of a new customer

  const handleCreate = async (e: React.FormEvent) => {
    e.preventDefault(); // prevent the form from reloading the page when submitted
    try {
      const result = await api("/api/customers", {  
        method: "POST",
        body: JSON.stringify({
          fullName,
          email,
          nationalId,
          monthlyIncome: Number(monthlyIncome),
        }),
      });

      alert("Customer created! ID: " + result.id);
      setShowForm(false);
      setFullName("");
      setEmail("");
      setNationalId("");
      setMonthlyIncome("");
      loadCustomers();  // Refresh the list of customers after creation
    } catch (err: any) {
      alert(err.message);
    }
  };

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <div style={{ display: "flex", justifyContent: "space-between", marginBottom: 20 }}>
        <h2>Customers</h2>
        <button
          onClick={() => setShowForm(!showForm)}
          style={{ padding: "8px 16px", background: "#3b82f6", color: "white", border: "none", borderRadius: 6, cursor: "pointer" }}
        >
          {showForm ? "Cancel" : "+ New Customer"}
        </button>
      </div>


    {/* // معناه إن React مش هيعرض الفورم إلا لو showForm قيمتها true. */}


      {showForm && (
        <form onSubmit={handleCreate} style={{ background: "white", padding: 20, borderRadius: 8, marginBottom: 20 }}>
          <div style={{ display: "grid", gridTemplateColumns: "1fr", gap: 12 }}>
            <input placeholder="Full Name" value={fullName} onChange={(e) => setFullName(e.target.value)} required style={inputStyle} />
            <input placeholder="Email" type="email" value={email} onChange={(e) => setEmail(e.target.value)} required style={inputStyle} />
            <input placeholder="National ID" value={nationalId} onChange={(e) => setNationalId(e.target.value)} required style={inputStyle} />
            <input placeholder="Monthly Income" type="number" value={monthlyIncome} onChange={(e) => setMonthlyIncome(e.target.value)} required style={inputStyle} />
          </div>
          <button type="submit" style={{ marginTop: 12, padding: "8px 20px", background: "#10b981", color: "white", border: "none", borderRadius: 6 }}>
            Create Customer
          </button>

          {/* <button
            onClick={async () => {
                try {
                const res = await fetch("http://localhost:5155/api/loans"); // change port
                console.log("Status:", res.status);
                const data = await res.json();
                console.log(data);
                } catch (err) {
                console.error("Error:", err);
                }
            }}
            >
            Test API
            </button> */}
        </form>
      )}

      {/* // Table of customers */}

     {/* <div style={{ background: "white", borderRadius: 8, padding: 20 }}>
        <table style={{ width: "100%", borderCollapse: "collapse" }}>
          <thead>
            <tr style={{ textAlign: "center", borderBottom: "1px solid #e2e8f0" }}>
              <th style={{ padding: 10 }}>Name</th>
              <th>Email</th>
              <th>National ID</th>
              <th>Income</th>
              <th>ID (copy for loans)</th>
            </tr>
          </thead>
          <tbody>
            {customers.map((c) => (
              <tr key={c.id} style={{ borderBottom: "1px solid #f1f5f9" }}>
                <td style={{ padding: 10 }}>{c.fullName}</td>
                <td>{c.email}</td>
                <td>{c.nationalId}</td>
                <td>{c.monthlyIncome.toLocaleString()} EGP</td>
                <td style={{ fontSize: 12, color: "#64748b" }}>{c.id}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div> */}


        
<div className="table-card">
  <div className="table-header">
    <div>
      <h2>Customers</h2>
      <p>View and manage registered customers</p>
    </div>
    <span className="customer-count">
      {customers.length} customers
    </span>
  </div>

  <div className="table-scroll">
    <table className="customers-table">
      <thead>
        <tr>
          <th>Name</th>
          <th>Email</th>
          <th>National ID</th>
          <th>Monthly Income</th>
          <th>Customer ID</th>
        </tr>
      </thead>

      <tbody>
        {customers.map((c) => (
          <tr key={c.id}>
            <td>
              <span className="customer-name">
                {c.fullName}
              </span>
            </td>

            <td className="email-cell">{c.email}</td>

            <td className="national-id">
              {c.nationalId}
            </td>

            <td>
              <span className="income-value">
                {c.monthlyIncome.toLocaleString("en-US")}
              </span>
              <span className="currency"> EGP</span>
            </td>

            <td>
              <div className="id-cell">
                <code title={c.id}>{c.id}</code>

                <button
                  className="copy-button"
                  title="Copy customer ID"
                  aria-label={`Copy ID for ${c.fullName}`}
                  onClick={() =>
                    navigator.clipboard.writeText(c.id)
                  }
                >
                  Copy
                </button>
              </div>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  </div>
</div>


    </div>
  );
}

// inputStyle → CSS styles for the input fields in the form
const inputStyle: React.CSSProperties = {
  padding: 10,
  border: "1px solid #e2e8f0",
  borderRadius: 6,
  width: "100%",
};