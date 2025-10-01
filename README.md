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


** Necessary Things to Do Before Running the project*

1] Install MailKit

In Visual Studio :
Go to Tools -> NuGet Package Manager ->Manage NuGet Packages for solution
- Search for MailKit package and install it according to suitable version


  <img width="1920" height="1020" alt="image" src="https://github.com/user-attachments/assets/2a0b0c18-560b-4023-b51e-5f09a5ce8f13" />

.NET CLI :
dotnet add package MailKit


In Your appsettings.json / appsettings.Development.json  Add following:
<img width="419" height="144" alt="image" src="https://github.com/user-attachments/assets/062208af-c3d2-45eb-b835-db31ac26ac67" />

  "EmailSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "Username": "YOUR EMAIL ID",
    "Password": "YOUR GOOGLE APP PASSWORD",
    "EnableSsl": true,
    "FromName": "SkillBridge",
    "FromEmail": "bizzconnect2000@gmail.com"
  },


2] Install Razorpay

In Visual Studio :
Go to Tools -> NuGet Package Manager ->Manage NuGet Packages for solution
- Search for Razorpay package and install it according to suitable version
 <img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/e28fd73c-a727-4623-96c2-ee740f4159b4" />

 
.NET CLI :

 dotnet add package razorpay

 
In Your appsettings.json / appsettings.Development.json  Add following:
<img width="419" height="144" alt="image" src="https://github.com/user-attachments/assets/df756030-8ef3-416c-9439-b870ed291ddf" />

"Razorpay": {
  "Key": "YOUR_RAZORPAY_API_KEY",
  "Secret": "YOUR_RAZORPAY_SECRET_ID"
}


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



