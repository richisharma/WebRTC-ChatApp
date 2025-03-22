# WebRTC-ChatApp
 WebRTC chat app
WebRTC Chat App is a real-time messaging and video calling application built with **.NET Core, React, SignalR, and WebRTC**. The app allows users to **register, log in, chat one-on-one, join chat rooms, and make video** calls using WebRTC technology.

**Features**

**User Authentication**
1. Register & Login with JWT Authentication
2. Secure API requests using JWT tokens

**One-on-One Chat**
1. Send & receive messages in real-time
2. Store chat history in SQL Server using Entity Framework

**Chat Rooms (Group Chat)**
1. Create & Join Chat Rooms
2. Group messaging with SignalR

**Video Calling**
1. WebRTC integration for peer-to-peer video calls
2. Supports secure and real-time video streaming

**Real-Time Communication**
1. SignalR used for live chat updates & WebRTC signaling
2. Connection management for active users

**Unit Testing**
1. Messaging Service (MessagesControllerTests)
2. User Service (UsersControllerTests)
3. Signaling Service (SignalingHubTests)
4. Mocking SignalR & Database calls for testing

Dockerized environment for local development

**Tech Stack**

**Backend (Messaging Service, Signaling Service & User Service)**
1. .NET Core 9 (ASP.NET Web API)
2. Entity Framework Core (SQL Server)
3. SignalR (Real-time messaging)
4. JWT Authentication
5. XUnit & Moq (Unit Testing)
6. Containerization: Docker and Docker Compose

**Frontend**
1. React (React 19)
2. React Router (Routing)
3. Axios (API calls)
4. WebRTC (Video Calling)
5. Jest & React Testing Library (Unit Testing)

**Database**
1. SQL Server
2. Entity Framework Code-First Approach

**Prerequisites**
1. Docker & Docker Compose
2. Node.js and npm (if running frontend outside Docker)
3. SQL Server (running locally with SQL authentication enabled)

**Backend Setup (.NET Services)**

**Clone the repository**
1. [git clone https://github.com/your-repo/WebRTC-ChatApp.git](https://github.com/richisharma/WebRTC-ChatApp)
 cd WebRTC-ChatApp
2. Configure environment variables
   Update the .env file in the frontend folder:
   - REACT_APP_USER_SERVICE_URL=http://localhost:5001
   - REACT_APP_MESSAGING_SERVICE_URL=http://localhost:5002
   - REACT_APP_SIGNALING_SERVICE_URL=http://localhost:5003
3. The API will be available at: 
 - UserService : https://localhost:5001
 - Messaging Service : https://localhost:5002
 - Signaling Service : https://localhost:5003

**Frontend Setup (React)**
1. Clone teh repo https://github.com/richisharma/webrtc-chat-app

2. Dockerize and Run
   Build and run the services using Docker Compose:
   docker-compose up --build -d
   This will spin up the following services:
   - frontend on port 3000
   - userservice on port 5001
   - messagingservice on port 5002
   - signalingservice on port 5003
  
 3. Access the App
    Open your browser and navigate to:
    http://localhost:3000

**Dockerization Details**

The app has been fully dockerized for seamless local development. Each service (frontend, backend microservices) runs in its own container and communicates via a Docker network.
- SQL Server is expected to run on the host machine.
- Docker Compose handles building and starting the services.
- Docker Compose should have the context and volumes set to the path of the reat frontend.
![image](https://github.com/user-attachments/assets/bf84d925-55ac-4ec2-87e9-14b1cd8f5057)

- Use docker-compose.override.yml to specify the SQL Server connection using your host IP address.
- Example SQL Server connection string:
  Server=<your local IP address>;Database=UserDB;User Id=sa;Password=YourStrong!Password;TrustServerCertificate=True;

**Notes**
Make sure SQL Server allows remote connections and the firewall is not blocking port 1433.
You may need to update ConnectionStrings__DefaultConnection in docker-compose.override.yml to use your local device IP address.
