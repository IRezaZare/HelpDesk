# HelpDesk

An **ASP.NET Core** help desk / ticketing project with Identity-based authentication and a layered architecture (`Controllers`, `Services`, `Entities`, `Views`).

---

## ✨ Features

* Ticket CRUD (create, view, update, change status)
* Roles & permissions via **ASP.NET Core Identity**
* Ticket categories / priorities
* Internal notes & activity log
* Advanced search & filtering
* Layered structure: `Controllers`, `Services`, `Entities`, `ViewModels`, `Views`

---

## 🧰 Tech Stack

* C# & **ASP.NET Core** (MVC/Razor Pages)
* **Identity** for authentication/roles
* **Entity Framework Core**
* **SQL Server/LocalDB** (or any EF Core provider)
* **AutoMapper** (based on `Mapping` folder)

---

## 🚀 Quick Start

### Prerequisites

* **.NET SDK** (preferably .NET 7 or 8)
* **SQL Server** or **LocalDB**
* Git

### Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/IRezaZare/HelpDesk.git
   cd HelpDesk
   ```
2. Configure connection string in `appsettings.json`:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=HelpDeskDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
     }
   }
   ```
3. Apply migrations:

   ```bash
   dotnet tool install --global dotnet-ef   # if not installed
   dotnet ef database update
   ```

   > If no migrations exist, create one:

   ```bash
   dotnet ef migrations add Init
   dotnet ef database update
   ```
4. Run the project:

   ```bash
   dotnet run
   ```
5. Open browser at `https://localhost:5001` or `http://localhost:5000`

---

## ⚙️ Configuration

* **Identity**: seed initial roles/users (Admin/Agent/User)
* **Email** (optional): configure SMTP in `appsettings.json`
* **Logging**: adjust log level in `appsettings*.json`

---

## 🗂 Project Structure

```
Areas/Identity/Pages     # Identity and account pages
Controllers              # Web controllers
Data                     # DbContext and database config
Entities                 # Domain models/tables
Extensions               # Service/middleware extensions
Helpers                  # Utility functions
Interfaces               # Service contracts
Mapping                  # AutoMapper profiles
Models / ViewModels      # DTOs/ViewModels
Services                 # Business logic
Views                    # Razor Views
wwwroot                  # Static files (css/js/img)
```

---

## 🧪 Common Scripts

```bash
# Development run
ASPNETCORE_ENVIRONMENT=Development dotnet run

# Run tests (if test project exists)
dotnet test

# Build release
dotnet publish -c Release -o ./publish
```

---

## ✅ Roadmap

* [ ] Complete ticket CRUD (statuses: Open/In Progress/Resolved/Closed)
* [ ] Advanced filtering & sorting
* [ ] File attachments for tickets
* [ ] Email/Web notifications
* [ ] Admin dashboard (KPIs: avg response time, open tickets, SLA)
* [ ] Unit & integration tests
* [ ] Dockerfile & GitHub Actions (CI/CD)

---

## 🤝 Contributing

1. Fork the repo
2. Create a branch: `feature/awesome`
3. Commit/Push
4. Open a Pull Request

> Please add clear descriptions and screenshots (if UI related).

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

## 🧑‍💻 Author

* **Reza Zare**

---

# راهنمای HelpDesk

یک پروژه‌ی **ASP.NET Core** برای مدیریت تیکت‌های پشتیبانی (Help Desk) با احراز هویت کاربرها (Identity) و معماری لایه‌ای (Controllers/Services/Entities/Views).

---

## ✨ امکانات

* ثبت و مدیریت تیکت‌ها (ایجاد، مشاهده، تغییر وضعیت)
* نقش‌ها و سطوح دسترسی مبتنی بر **ASP.NET Core Identity**
* دسته‌بندی/اولویت‌بندی تیکت‌ها
* یادداشت داخلی و تاریخچه فعالیت‌ها
* جست‌وجو و فیلتر پیشرفته
* معماری لایه‌ای: `Controllers`, `Services`, `Entities`, `ViewModels`, `Views`

---

## 🧰 تکنولوژی‌ها

* C# و **ASP.NET Core** (MVC/Razor Pages)
* **Identity** برای احراز هویت و نقش‌ها
* **Entity Framework Core**
* **SQL Server/LocalDB** (یا هر دیتابیس سازگار)
* **AutoMapper** (بر اساس پوشه `Mapping`)

---

## 🚀 راه‌اندازی سریع

### پیش‌نیازها

* **.NET SDK** (ترجیحاً نسخه‌های 7 یا 8)
* **SQL Server** یا **LocalDB**
* Git

### مراحل اجرا

1. کلون:

   ```bash
   git clone https://github.com/IRezaZare/HelpDesk.git
   cd HelpDesk
   ```
2. تنظیم کانکشن‌استرینگ در `appsettings.json`
3. اجرای مایگریشن:

   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef database update
   ```
4. اجرای پروژه:

   ```bash
   dotnet run
   ```
5. آدرس مرورگر: `https://localhost:5001` یا `http://localhost:5000`

---

## ⚙️ پیکربندی‌ها

* **Identity**: تعریف نقش/کاربر اولیه (Admin/Agent/User)
* **ایمیل** (اختیاری): پیکربندی SMTP در `appsettings.json`
* **Logging**: تغییر سطح لاگ‌ها از `appsettings*.json`

---

## 🗂 ساختار پروژه

(مطابق نمودار بالا)

---

## 🧪 اسکریپت‌های متداول

```bash
ASPNETCORE_ENVIRONMENT=Development dotnet run

# اجرای تست‌ها (در صورت وجود)
dotnet test

# ساخت خروجی Release
dotnet publish -c Release -o ./publish
```

---

## ✅ نقشه راه

* [ ] تکمیل CRUD تیکت‌ها (Open/In Progress/Resolved/Closed)
* [ ] فیلتر و مرتب‌سازی پیشرفته
* [ ] پیوست فایل‌ها
* [ ] اعلان ایمیلی/وب
* [ ] داشبورد ادمین (میانگین پاسخ، تعداد تیکت باز، SLA)
* [ ] تست‌های واحد/یکپارچه
* [ ] Dockerfile و CI/CD

---

## 🤝 مشارکت

1. Fork کنید
2. Branch بسازید: `feature/awesome`
3. Commit/Push
4. Pull Request باز کنید

---

## 📄 مجوز

این پروژه تحت [لایسنس MIT](LICENSE) منتشر شده است.

---

## 🧑‍💻 نویسنده

* **رضا زارع**
