import { useEffect, useState } from "react";
import { api } from "../api/client";
import React from "react";

type Installment = {
  id: string;
  dueDate: string;
  amount: number;
  isPaid: boolean;
  paidAt: string | null;
};

type Loan = {
  id: string;
  customerId: string;
  customerFullName: string;
  amount: number;
  durationInMonths: number;
  purpose: string;
  status: string;
  createdAt: string;
  installments: Installment[];
};

type Customer = {
  id: string;
  fullName: string;
};


export default function Loans() {

// 1- States of the component
  const [loans, setLoans] = useState<Loan[]>([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [expanded, setExpanded] = useState<string | null>(null);

  // form
  const [customerId, setCustomerId] = useState("");
  const [amount, setAmount] = useState("");
  const [duration, setDuration] = useState("");
  const [purpose, setPurpose] = useState("");
  const [customers, setCustomers] = useState<Customer[]>([]);


  const toggleInstallments = (id: string) => {
  setExpanded(expanded === id ? null : id);
};

const handlePay = async (installmentId: string) => {
  if (!confirm("Mark this installment as paid?")) return;
  try {
    await api(`/api/loans/installments/${installmentId}/pay`, { method: "PUT" });
    loadLoans();
  } catch (err: any) {
    alert(err.message);
  }
};

  // 2- Load loans from the API
  const loadLoans = async () => {
    try {
  const [loansData, customersData] = await Promise.all([
      api("/api/loans"),
      api("/api/customers"),
    ]);
    setCustomers(customersData);

    const loansWithCustomerNames = loansData.map((loan: Loan) => ({
      ...loan,
      customerFullName:
        customersData.find(
          (customer: Customer) =>
            customer.id.toLowerCase() === loan.customerId.toLowerCase()
        )?.fullName ?? "Unknown Customer",
    }));

    setLoans(loansWithCustomerNames);
  } catch (err) {
    console.error(err);
  } finally {
    setLoading(false);
  }
  
  };

  // 3- useEffect to load loans from GET /api/loans when the component mounts
  useEffect(() => {
    loadLoans();
  }, []);


  // 4- Handle form submission to create a new loan

  const handleCreate = async (e: React.FormEvent) => {
    e.preventDefault(); // prevent the form from reloading the page when submitted
    try {
      await api("/api/loans", {
        method: "POST",
        body: JSON.stringify({
          customerId,
          amount: Number(amount),
          durationInMonths: Number(duration),
          purpose,
        }),
      });
      alert("Loan created!");
      setShowForm(false);
      loadLoans();
    } catch (err: any) {
      alert(err.message);
    }
  };


  // 5- Handle approve and reject actions for loans
  const handleApprove = async (id: string) => {
    if (!confirm("Approve this loan?")) return;
    try {
      await api(`/api/loans/${id}/approve`, { method: "PUT" });
      loadLoans();
    } catch (err: any) {
      alert(err.message);
    }
  };


  // 6- Handle reject action for loans
  const handleReject = async (id: string) => {
    if (!confirm("Reject this loan?")) return;
    try {
      await api(`/api/loans/${id}/reject`, { method: "PUT" });
      loadLoans();
    } catch (err: any) {
      alert(err.message);
    }
  };

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <div style={{ display: "flex", justifyContent: "space-between", marginBottom: 20 }}>
        <h2>Loan Applications</h2>
        <button
          onClick={() => setShowForm(!showForm)}
          style={{ padding: "8px 16px", background: "#3b82f6", color: "white", border: "none", borderRadius: 6 }}
        >
          {showForm ? "Cancel" : "+ New Loan"}
        </button>
      </div>

      {showForm && (
        <form onSubmit={handleCreate} style={{ background: "white", padding: 20, borderRadius: 8, marginBottom: 20 }}>
          <div style={{ display: "grid", gridTemplateColumns: "1fr", gap: 12 }}>
            <input placeholder="Customer ID" value={customerId} onChange={(e) => setCustomerId(e.target.value)} required style={inputStyle} />
            <input placeholder="Amount" type="number" value={amount} onChange={(e) => setAmount(e.target.value)} required style={inputStyle} />
            <input placeholder="Duration (months)" type="number" value={duration} onChange={(e) => setDuration(e.target.value)} required style={inputStyle} />
            <input placeholder="Purpose" value={purpose} onChange={(e) => setPurpose(e.target.value)} required style={inputStyle} />
          </div>
          <button type="submit" style={{ marginTop: 12, padding: "8px 20px", background: "#10b981", color: "white", border: "none", borderRadius: 6 }}>
            Create Loan
          </button>
        </form>
      )}

      <div style={{ background: "white", borderRadius: 8, padding: 20 }}>
        <table style={{ width: "100%", borderCollapse: "collapse" }}>
          <thead>
            <tr style={{ textAlign: "left", borderBottom: "1px solid #e2e8f0" , margin: 10}}>
              <th style={{ padding: 10 , margin:10}}>Customer Full Name</th>
              <th style={{ padding: 10 , margin:10}}>Customer ID</th>
              <th style={{ padding: 10 , margin:10}}>Amount</th>
              <th style={{ padding: 10 , margin:10}}>Duration</th>
              <th style={{ padding: 10 , margin:10}}>Purpose</th>
              <th style={{ padding: 10 , margin:10}}>Status</th>
              <th style={{ padding: 10 , margin:10}} >Created</th>
              <th style={{ padding: 10 , margin:10}}>Actions</th>
            </tr>
          </thead>
          <tbody>


            {/* // 7- Render the list of loans in a table with approve/reject buttons for pending loans */}
   {loans.map((loan) => (
  <React.Fragment key={loan.id}>
    <tr style={{ borderBottom: "1px solid #f1f5f9" , margin: 10}}>

                <td>{loan.customerFullName}</td>
        <td>{loan.customerId}</td>

      <td style={{ padding: 10 }}>{loan.amount.toLocaleString()} EGP</td>
      <td >{loan.durationInMonths} months</td>
      <td>{loan.purpose}</td>
      <td style={{ padding: 10 , margin:10}} >{loan.status}</td>
      <td style={{ padding: 10 , margin:10}} >{new Date(loan.createdAt).toLocaleDateString()}</td>
      <td  style={{ display: "flex", gap: 2, padding: 5 , margin:10}} >
        {loan.status === "Pending" && (
          <>
            <button onClick={() => handleApprove(loan.id)} style={btnGreen}>Approve</button>
            <button onClick={() => handleReject(loan.id)} style={btnRed}>Reject</button>
          </>
        )}
        {loan.status === "Approved" && (
          <button onClick={() => toggleInstallments(loan.id)} style={btnBlue}>
            {expanded === loan.id ? "Hide" : "Show"} Installments
          </button>
        )}
      </td>
    </tr>

    {/* Installments row */}
    {expanded === loan.id && loan.installments?.length > 0 && (
      <tr>
        <td colSpan={6} style={{ background: "#f8fafc", padding: 16 }}>
          <table style={{ width: "100%" }}>
            <thead>
              <tr>
                <th>Due Date</th>
                <th>Amount</th>
                <th>Status</th>
                <th>Action</th>
              </tr>
            </thead>
            <tbody>
              {loan.installments.map((inst) => (
                <tr key={inst.id}>
                  <td>{new Date(inst.dueDate).toLocaleDateString()}</td>
                  <td>{inst.amount} EGP</td>
                  <td>{inst.isPaid ? "Paid" : "Unpaid"}</td>
                  <td>
                    {!inst.isPaid && (
                      <button onClick={() => handlePay(inst.id)} style={btnGreen}>
                        Pay
                      </button>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </td>
      </tr>
    )}
  </React.Fragment>
))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

const inputStyle: React.CSSProperties = {
  padding: 10,
  border: "1px solid #e2e8f0",
  borderRadius: 6,
  width: "100%",
};

// styles
const btnGreen = { marginRight: 8, background: "#10b981", color: "white", border: "none", padding: "4px 10px", borderRadius: 4, cursor: "pointer" };
const btnRed = { background: "#ef4444", color: "white", border: "none", padding: "4px 10px", borderRadius: 4, cursor: "pointer" };
const btnBlue = { background: "#3b82f6", color: "white", border: "none", padding: "4px 10px", borderRadius: 4, cursor: "pointer" };