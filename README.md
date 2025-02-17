
###  SkillNode - AI-Powered Learning Platform 
SkillNode is an AI-powered learning platform built using .NET Core Web API, Microservices, and Azure.  
This repository contains the backend services for user authentication, profile management, and course management.

---

## Tech Stack
 Backend: ASP.NET Core Web API (.NET 8)  
 Database: SQL Server (EF Core)  
 Authentication: JWT + Role-Based Access  
 API Gateway: Azure API Management  
 Microservices Communication: REST API + gRPC  
 Messaging System: RabbitMQ  
 File Storage: Azure Blob Storage  
 Search Engine: ElasticSearch / Azure Cognitive Search  
 Payment Integration: Stripe / PayPal  
 DevOps & CI/CD: GitHub Actions + Docker + Kubernetes (AKS)  
 Cloud Hosting: Azure App Services  

---

##  Features Implemented
###  Authentication & Authorization
-  JWT Authentication (`POST /api/users/login`)
-  Role-Based Access Control (`[Authorize(Roles = "Admin")]`)
-  Secure API endpoints using `[Authorize]`

###  User Profile Management
-  Update Profile (`PUT /api/users/update-profile`)
-  Change Password (`PUT /api/users/change-password`)
-  Admin Role Management (`PUT /api/users/update-role/{userId}`)

###  Database & EF Core
-  User Model (`Users` table)
-  EF Core Migrations Applied
-  Verified Database Schema & Data

---

##  How to Run the Project
###  Clone the Repository
```sh
git clone https://github.com/yourusername/SkillNode-Backend.git
cd SkillNode-Backend
```

###  Setup Database Connection
- Open `appsettings.json`  
- Update your SQL Server connection string under `"ConnectionStrings"`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SkillNodeDB;User Id=youruser;Password=yourpassword;"
}
```

###  Apply EF Core Migrations
```sh
dotnet ef database update
```

###  Run the API
```sh
dotnet run
```
 API will be available at:  
```
http://localhost:5001
```

---

## API Endpoints
###  User Authentication
| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/users/register` | Register a new user |
| `POST` | `/api/users/login` | Authenticate & get JWT token |
| `GET` | `/api/users/me` | Get current logged-in user |

### Role-Based Access
| Method | Endpoint | Role Required |
|--------|----------|--------------|
| `GET` | `/api/users` | Admin |
| `PUT` | `/api/users/update-role/{userId}` | Admin |
| `GET` | `/api/users/student-resource` | Student |
| `GET` | `/api/users/instructor-resource` | Instructor |

###  User Profile Management
| Method | Endpoint | Description |
|--------|----------|-------------|
| `PUT` | `/api/users/update-profile` | Update user profile |
| `PUT` | `/api/users/change-password` | Change user password |

---

## Next Steps
 ### Upcoming Features:
 #### Course Management API (Create, Update, Delete Courses)
 #### Microservices Architecture Implementation
 #### Payment System Integration (Stripe/PayPal)  
 #### API Gateway & Azure Integration

---
