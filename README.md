# 🌦️ Weather App Project

A visualization app for regions, provinces, and cities of Spain.  
This application retrieves real-time weather data from public APIs (such as Visual Crossing Weather) and displays it interactively.

---

## 📅 Changelog

- **2025-02-05**: Initial upload — backend and frontend (no database).
- **2025-02-08**: Minor updates and fixes.
- **2025-04-18**: Added modules for weather and forecast queries, modals, models and services. Integrated Visual Crossing Weather API and added the architecture diagram.

---

## 🎯 Objective

To practice with:
- **.NET (C#)** and **SQL (Oracle DB)**
- **Angular (TypeScript)**
- **Design Patterns**
- **Onion Architecture**

Uses the **Visual Crossing Weather API** to retrieve real-time weather and forecast data.

---

## 🚀 Features

### 🔧 Backend
- Follows **Onion Architecture**
- Implements several **Design Patterns**:
  - Repository Pattern
  - Unit of Work
  - Singleton
  - Base Entity
  - Data Transfer Object (DTO)

- **Key Libraries**:
  - API calls to: [Visual Crossing Weather](https://weather.visualcrossing.com/)
  - Encryption:
    - `BCrypt.Net-Next`
    - `System.Security.Cryptography` (AES-256 encryption)
  - Logging:
    - `Serilog`
    - `Serilog.Extensions.Logging`
    - `Serilog.Sinks.File`
  - ORM:
    - `Oracle.EntityFrameworkCore` for Oracle DB integration using Docker

---

### 💻 Frontend

- Built with **Angular 18.2.14**
- Implements data communication via **parent-child components**
- Uses Angular Material for modal/popup support:
  - `@angular/material: 18.2.14`
  - `@angular/animations: 18.2.13`
  - `@angular/cdk: 18.2.14`
- Modular project structure

---

### 🗄️ Database

- Written in **Oracle DB (via SQL Developer)**
- Uses **Dockerized Oracle DB instance**
- Includes:
  - **DDL scripts** for table creation
  - **DML scripts** for sample data insertion

---

## 🧪 Installation

### ✅ Prerequisites

Ensure the following are installed on your system:

- [.NET SDK 9.0.200](https://dotnet.microsoft.com/)
- [Docker Desktop 4.40.0](https://www.docker.com/)
- [SQL Developer](https://www.oracle.com/tools/downloads/sqldev-downloads.html)
- [Node.js + npm](https://nodejs.org/) (for frontend)

---

### 🔧 Setup Steps

1. Clone the repository:
    ```bash
    git clone https://github.com/waltermillan/Weather.git
    ```

2. Follow the video guides for full setup:
    - [1st Version Setup](https://youtu.be/wiVSGGAFbRE)

3. Complete the remaining setup steps as described in the project documentation.

---

## 📄 License

**Free and open-source**
