import { Routes } from '@angular/router';
import { SendNotificationComponent } from './components/send-notification/send-notification.component';

export const routes: Routes = [
  { path: '', component: SendNotificationComponent },
  { path: '**', redirectTo: '' }
];
