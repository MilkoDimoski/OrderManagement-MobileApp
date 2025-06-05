# OrderManagement-MobileApp

This project is a mobile application developed using **Xamarin.Forms** and backed by an **ASP.NET Core Web API** with data stored in **SQL Server (SSMS)**. It demonstrates basic functionality for user login, order lookup, order editing, and saving – following a simple MVVM structure.

## 📱 Features

### 🔐 Login Page
- User selects their name from a dropdown list.
- Enters password.
- Password is verified using **AES encryption** (not hashed – for simplicity in this demo).
- Only on correct password, user is redirected to the main menu.

### 📋 Menu Page
Contains two menu items:
1. **Order Lookup**
2. **Edit Order**

---

## 1️⃣ Order Lookup
- Redirects to a page where the user can input an **Order Number**.
- Displays:
  - Order Header: `Order Number`, `Order Date`, `Order Status`.
  - Order Details: list of items with `SKU`, `Price`, `Quantity`.
- Each order may have multiple detail rows.

---

## 2️⃣ Edit Order (Tabbed Page)
- **Tab 1:** Input for Order Number.
- **Tab 2:** Displays one item at a time from the order.
  - Navigate with **Previous/Next** buttons.
  - Modify `Price` and `Quantity` for each item.
- **Tab 3:** Confirm and **Save Changes**.
  - Only here, the modified data is saved to the database.
  - Entering a different Order Number resets and reloads data from the database.

---

## 🧰 Technologies Used

- **Frontend (Mobile):** Xamarin.Forms (.NET MAUI-style MVVM)
- **Backend:** ASP.NET Core Web API
- **Database:** SQL Server (SSMS)
- **ORM/Data Access:** Dapper
- **Encryption:** AES (System.Security.Cryptography)

---

## ⚠️ Notes

> **Disclaimer:** Passwords are stored in the database in plain text (unencrypted). This is NOT a recommended practice and is used here only for demo purposes.

---

## 📸 Screenshots

![Screenshot 2025-06-05 202638](https://github.com/user-attachments/assets/c661525d-c041-4211-bb83-c594f9626deb)
![Screenshot 2025-06-05 202649](https://github.com/user-attachments/assets/16012fc3-667e-4ef0-bce3-9ea5fa70149f)
![Screenshot 2025-06-05 202943](https://github.com/user-attachments/assets/55471257-4ecb-4e15-811e-5223a52258d4)
![Screenshot 2025-06-05 202956](https://github.com/user-attachments/assets/f2e77ca7-6976-4381-9120-51b24b6904d0)
![Screenshot 2025-06-05 203013](https://github.com/user-attachments/assets/751f67eb-ba90-4266-8aad-b160f4ec3f5b)


## 🚀 Getting Started

1. Clone this repo.
2. Set up the SQL Server database with the required tables (`Orders`, `OrderDetails`, `Users`, `Firms`).
3. Configure the backend Web API connection string.
4. Run the Web API.
5. Run the Xamarin.Forms mobile app (tested on Android emulator).
6. Login, browse orders, edit and save!

---

## 🧑‍💻 Author

Milko Dimoski – 2025

