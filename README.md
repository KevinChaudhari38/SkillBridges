# SkillBridges

SkillBridges is a freelancing platform that connects **Clients** and **Professionals** under the supervision of an **Admin**.  
It allows seamless posting of tasks, proposals, skill management, messaging, and task approvals.

---

## 🚀 Tech Stack

- **Framework:** ASP.NET Core 9.0 (C#)
- **Database ORM:** Entity Framework Core (SQL Server)
- **Authentication:**
  - Cookie Authentication
- **Utilities & Tools:**
  - AutoMapper (object-to-object mapping)
  - MailKit (email notifications)
  - Razorpay(Payment API)

---

## 👥 User Roles & Features

### 🔹 Admin
- Manage all users (Clients & Professionals)
- Monitor and control platform activities
- Create categories

### 🔹 Client
- Create and manage tasks

- View/accept/reject professional requests on tasks
- Message professionals after task approval

### 🔹 Professional
- Add and manage skills
- View task requests
- Upload proposals
- Communicate with clients after approval

---

## ⚙️ Installation & Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/KevinChaudhari38/SkillBridges.git
   cd SkillBridges

 2. **Build Solution**
    Go to SkillBriges.sln

    In Visual Studio :
    Build-> Build Solution

    .NET CLI :
    dotnet build

3. **Apply Migrations**

PMC :
  Add-Migration init
  Update-Database

.NET CLI:
 dotnet ef migrations add init
 dotnet ef database update

 4. **Run Project**

In Visual Studio :
CLick on Run Button 

.NET CLI :
dotnet run

Get the localhost URL :- http://localhost:5145/
