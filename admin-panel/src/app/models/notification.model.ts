export interface Notification {
  id: number;
  title: string;
  body: string;
  createdAt: string;
  sentBy: string;
  isSent: boolean;
}

export interface SendNotificationRequest {
  title: string;
  body: string;
  sentBy: string;
}
