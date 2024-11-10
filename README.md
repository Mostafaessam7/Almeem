# Almeem Fashion - E-Commerce Store with ASP.NET Core and Angular

### Welcome to the Almeem Fashion repository! 
### Almeem Fashion is a proof-of-concept e-commerce application that brings together key components of an online store. Built with a layered architecture, the project separates the client, server, and data access logic for clear organization and scalability. By following best practices in both ASP.NET Core and Angular, this project serves as an excellent reference for developers looking to create robust, scalable web applications.


## Demo

https://github.com/user-attachments/assets/c56b23aa-d039-4350-a319-a6fc10117ec5



## Key Components

### 1. Frontend (Angular)
The Angular application is the store’s client-facing side, where users can:
- Browse products, view details, and manage their shopping basket.
- Use a responsive UI built with Angular Material and Tailwind CSS.
- Benefit from lazy-loaded routes and modular architecture, improving performance and usability.

### 2. Backend (ASP.NET Core API)
The backend API is built with ASP.NET Core, providing:
- RESTful endpoints for managing product data, orders, and user authentication.
- Patterns like Repository, Unit of Work, and Specification to keep the code maintainable and testable.
- ASP.NET Identity for user login and registration, with role-based access control.
- Redis for efficient shopping basket storage and caching.
- Stripe integration to handle secure payments with 3D Secure support.

### 3. Admin Dashboard (ASP.NET Core MVC)
The Admin Dashboard provides administrators with tools for managing the store:
- Add and update products, manage categories, and view orders.
- Implemented as a separate MVC application for secure, role-based access to admin functionalities.
- Allows admins to oversee inventory and handle order management.

### 4. Real-Time Updates (SignalR)
SignalR is used to provide real-time updates, such as order status changes, creating a more interactive experience for users and administrators.

### 5. Deployment and Hosting
Instructions are provided for publishing Almeem Fashion to Azure, enabling smooth transition from development to production. Hosting the app on Azure allows for scalable and reliable access for users.

## Learning Objectives

Building Almeem Fashion involves working with essential concepts and tools in web development, such as:
- **Full-Stack Architecture**: Understand the clean architecture of a .NET and Angular project with clear separation between frontend, backend, and data layers.
- **Design Patterns**: Utilize patterns like Repository and Unit of Work to improve data management.
- **Responsive UI Design**: Build modern, mobile-friendly UIs using Angular Material and Tailwind.
- **Scalable Data Storage**: Use Redis for quick data access and caching for the shopping basket.
- **Payment Integration**: Learn how to process secure payments with Stripe, including 3D Secure compliance.
- **Real-Time Communication**: Integrate SignalR to deliver real-time updates to users.

## Getting Started

To get started with **Almeem Fashion**, clone this repository:

```bash
git clone https://github.com/Mostafaessam7/Almeem
