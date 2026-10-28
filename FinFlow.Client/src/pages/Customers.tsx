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
      setLoading(false);
    } catch (err) {
      console.error(err);
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
    } catch (err: any) {
      alert(err.message);
    }
  };

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

      {/* <div style={{ background: "white", borderRadius: 8, padding: 20 }}>
        <p style={{ color: "#64748b" }}>
          Note: Your backend currently has only Create Customer.  
          After you create customers, they will appear here once we add a GetAll endpoint.
        </p>
      </div> */}
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