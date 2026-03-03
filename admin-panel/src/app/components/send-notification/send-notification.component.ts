import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NotificationService } from '../../services/notification.service';
import { SendNotificationRequest } from '../../models/notification.model';

@Component({
  selector: 'app-send-notification',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './send-notification.component.html',
  styleUrls: ['./send-notification.component.scss']
})
export class SendNotificationComponent {
  title = '';
  body = '';
  sentBy = 'Admin';
  loading = false;
  successMessage = '';
  errorMessage = '';

  constructor(private notificationService: NotificationService) {}

  sendNotification(): void {
    if (!this.title.trim() || !this.body.trim()) {
      this.errorMessage = 'Title and Body are required.';
      this.successMessage = '';
      return;
    }

    this.loading = true;
    this.successMessage = '';
    this.errorMessage = '';

    const request: SendNotificationRequest = {
      title: this.title,
      body: this.body,
      sentBy: this.sentBy
    };

    this.notificationService.sendNotification(request).subscribe({
      next: (response) => {
        this.loading = false;
        this.successMessage = 'Notification sent successfully!';
        this.title = '';
        this.body = '';
      },
      error: (err) => {
        this.loading = false;
        this.errorMessage = err?.error?.message || 'Failed to send notification. Please try again.';
      }
    });
  }
}
