# WebRTC-ChatApp
 WebRTC chat app
WebRTC Chat App is a real-time messaging and video calling application built with .NET Core, React, SignalR, and WebRTC. The app allows users to register, log in, chat one-on-one, join chat rooms, and make video calls using WebRTC technology.

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
3. Mocking SignalR & Database calls for testing


**Tech Stack**

**Backend (Messaging Service, Signaling Service & User Service)**
.NET Core 9 (ASP.NET Web API)
Entity Framework Core (SQL Server)
SignalR (Real-time messaging)
JWT Authentication
XUnit & Moq (Unit Testing)

**Frontend**
React (React 19)
React Router (Routing)
Axios (API calls)
WebRTC (Video Calling)
Jest & React Testing Library (Unit Testing)

**Database**
SQL Server
Entity Framework Code-First Approach


**Backend Setup (.NET Services)**

**Clone the repository**
1. [git clone https://github.com/your-repo/WebRTC-ChatApp.git](https://github.com/richisharma/WebRTC-ChatApp)
 cd WebRTC-ChatApp
2. Set up the database
 dotnet ef database update
3. Run the API Services
 dotnet run --project UserService
 dotnet run --project MessagingService
 dotnet run --project SignalingService
4. The API will be available at: 
 UserService : https://localhost:7140
 Messaging Service : https://localhost:7189
 Signaling Service : https://localhost:7152

**Frontend Setup (React)**
1. Clone teh repo https://github.com/richisharma/webrtc-chat-app
2. Install dependencies
   npm install
4. Start the React App
   nom start
6. The app will be available at: http://localhost:3000
