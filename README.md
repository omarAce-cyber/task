# Remote Notification System

A full-stack push notification system built with **.NET Core Web API**, **Angular**, and **Flutter**, using **Firebase Cloud Messaging (FCM)**.

## Architecture Overview

```
repository/
├── backend/           # .NET Core Web API (N-Tier Architecture)
├── admin-panel/       # Angular Admin Panel
└── mobile-app/        # Flutter Mobile Application (MVVM)
```

---

## 1. Backend – .NET Core Web API

### Architecture (N-Tier)

```
NotificationSystem.Core/           # Core Layer
├── Entities/                      # Domain entities (Notification, Device, NotificationLog)
└── Interfaces/                    # Repository and service abstractions

NotificationSystem.DataAccess/     # Data Access Layer
├── Data/                          # AppDbContext + Design-time factory
└── Repositories/                  # Repository implementations

NotificationSystem.Business/       # Business Layer
└── Services/                      # Service implementations (FCM, Notification, Device)

NotificationSystem.API/            # Presentation Layer
├── Controllers/                   # API controllers
└── DTOs/                          # Request/response DTOs
```

### Setup

**Prerequisites:** .NET 9 SDK, SQL Server

1. Navigate to the backend folder:
   ```bash
   cd backend
   ```

2. Update the connection string in `NotificationSystem.API/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=NotificationSystemDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

3. Configure Firebase – replace the `Firebase:ServiceAccountJson` value with your Firebase Admin SDK service account JSON (download from Firebase Console > Project Settings > Service Accounts):
   ```json
   "Firebase": {
     "ServiceAccountJson": "{ ... paste your service account JSON here ... }"
   }
   ```

4. Apply migrations and start the API:
   ```bash
   cd NotificationSystem.API
   dotnet ef database update --project ../NotificationSystem.DataAccess
   dotnet run
   ```
   The API will be available at `http://localhost:5000`.

### API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/notification/send` | Send a push notification to all devices |
| GET | `/api/notification` | Get all notifications |
| POST | `/api/device/register` | Register a new device token |

### Database Schema

**Notifications**
- `Id` (int, PK)
- `Title` (string)
- `Body` (string)
- `CreatedAt` (datetime)
- `SentBy` (string)
- `IsSent` (bool)

**Devices**
- `Id` (int, PK)
- `DeviceToken` (string)
- `DeviceName` (string)
- `RegisteredAt` (datetime)

**NotificationLogs** (Bonus)
- `Id` (int, PK)
- `NotificationId` (int, FK)
- `DeviceId` (int, FK)
- `SentAt` (datetime)
- `Status` (string)

---

## 2. Angular Admin Panel

### Setup

**Prerequisites:** Node.js 18+, Angular CLI

1. Navigate to the admin-panel folder:
   ```bash
   cd admin-panel
   npm install
   ```

2. Update the API URL in `src/environments/environment.ts`:
   ```typescript
   export const environment = {
     production: false,
     apiUrl: 'http://localhost:5000'
   };
   ```

3. Start the development server:
   ```bash
   ng serve
   ```
   The app will be available at `http://localhost:4200`.

### Features

- **Send Notification** form with Title, Body, and Sent By fields
- Real-time success/error feedback
- Built with Angular standalone components, HttpClient, and reactive forms

---

## 3. Flutter Mobile Application

### Architecture (MVVM)

```
lib/
├── models/                        # Data models
│   └── notification_model.dart
├── services/                      # Firebase & API services
│   ├── api_service.dart
│   └── firebase_service.dart
├── viewmodels/                    # State management
│   └── notification_viewmodel.dart
├── views/                         # UI screens
│   └── notification_list_view.dart
└── main.dart
```

### Setup

**Prerequisites:** Flutter SDK 3.x, Android Studio / Xcode

1. Navigate to the mobile-app folder:
   ```bash
   cd mobile-app
   ```

2. Set up Firebase:
   - Create a Firebase project at [console.firebase.google.com](https://console.firebase.google.com)
   - Enable **Cloud Messaging**
   - Download `google-services.json` (Android) → place it in `android/app/`
   - Download `GoogleService-Info.plist` (iOS) → place it in `ios/Runner/`

3. Update the API URL in `lib/services/api_service.dart`:
   ```dart
   static const String baseUrl = 'http://YOUR_BACKEND_URL';
   ```

4. Install dependencies and run:
   ```bash
   flutter pub get
   flutter run
   ```

### Features

- Receives FCM push notifications (foreground & background)
- Shows system notifications when the app is in foreground
- **"List All Notifications"** screen fetches notifications from the backend API
- Registers device token with the backend on first launch
- Pull-to-refresh support
- MVVM pattern using `provider` for state management

---

## Firebase Setup Guide

1. Go to [Firebase Console](https://console.firebase.google.com)
2. Create a new project
3. Enable **Cloud Messaging** (FCM)
4. **For Backend:** Go to Project Settings > Service Accounts > Generate new private key → Download JSON and paste it in `appsettings.json`
5. **For Flutter Android:** Add an Android app, download `google-services.json` → place in `android/app/`
6. **For Flutter iOS:** Add an iOS app, download `GoogleService-Info.plist` → place in `ios/Runner/`
