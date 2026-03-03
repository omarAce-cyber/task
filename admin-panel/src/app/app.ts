import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  template: `
    <div class="app-wrapper">
      <header class="app-header">
        <h1>🔔 Notification Admin Panel</h1>
      </header>
      <main>
        <router-outlet />
      </main>
    </div>
  `,
  styles: [`
    .app-wrapper {
      min-height: 100vh;
      background: #f0f2f5;
    }
    .app-header {
      background: linear-gradient(135deg, #667eea, #764ba2);
      color: white;
      padding: 16px 24px;
      text-align: center;
      h1 {
        margin: 0;
        font-size: 22px;
      }
    }
    main {
      padding: 20px;
    }
  `]
})
export class App {}
