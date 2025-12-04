## Pc Builder

#### Description:

This is a web application that allows users to configure and order custom personal computers. It was created as a final project for a university course and was developed in early-to-mid 2023.

Pc Builder lets users select the exact components they want to have in their system, saving them the trouble of having to build it themselves.
The main features include configuring custom systems (with component compatibility checking), browsing a list of preconfigured systems, shopping cart, placing orders and leaving reviews.
The ordering functionality is mocked and the app is not integrated with any real payment systems. CRUD functionality is also included for users with admin privileges.

The application also includes role based granular access, handled by ASP.NET Identity and using JWT-s for identification.

#### Used Technologies:

The backend was built using C# and .NET Framework, utilizing Entity Framework, ASP.NET Identity and PostgreSQL for database.

The frontend was built using TypeScript and React, utilizing Bootstrap, React Router, Axios etc.

## Status

Not in active development.

## Screenshots

<img width="2015" height="1262" alt="Screenshot 2025-12-04 142203" src="https://github.com/user-attachments/assets/ee146991-fbcd-4da3-b2c4-04e52725d96e" />
&nbsp;
<img width="2036" height="1268" alt="Screenshot 2025-12-04 142247" src="https://github.com/user-attachments/assets/bcaeeda1-486b-4e58-bb11-f27bd7c660da" />
&nbsp;
<img width="1903" height="1255" alt="Screenshot 2025-12-04 142323" src="https://github.com/user-attachments/assets/b1a6d276-cfb4-4d4d-8b3d-a2ed53dca7ff" />
&nbsp;
<img width="1812" height="871" alt="Screenshot 2025-12-04 142358" src="https://github.com/user-attachments/assets/d4e1c2d9-5bfe-4132-9939-6562d3b1b54b" />
&nbsp;
<img width="1852" height="1175" alt="Screenshot 2025-12-04 142426" src="https://github.com/user-attachments/assets/6a89831d-2697-451f-9eb7-4d793405a7ba" />

## Setup Instructions

#### Prerequisites:

.NET 9.0, Node.js with npm, Docker Desktop, an IDE.

#### Backend:

1. Clone this repository.

2. Open the backend folder in JetBrains Rider or another IDE.

3. Install Entity Framework:

```
dotnet tool install --global dotnet-ef
```

4. Open Docker Desktop and setup the database:

```
docker-compose up
```

5. Run the backend application.

#### Frontend:

1. Open the frontend folder in VSC or another IDE.

2. Install dependencies:

```
npm install
```

3. Start the application:

```
npm run start
```

4. To visit the application:

```
http://localhost:3000
```

Both the frontend and backend have to be running at the same time!
