import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  messages: any[] = [];
  newMessage = '';
  connected = false;

  constructor() { }

  ngOnInit(): void {
    this.messages = [
      { sender: 'System', message: 'Hello! How can we help you today?', timestamp: new Date() }
    ];
    this.connected = true;
  }

  sendMessage(): void {
    if (this.newMessage.trim() === '') return;

    // Add user message
    this.messages.push({
      sender: 'User',
      message: this.newMessage,
      timestamp: new Date()
    });

    // Mock auto response
    setTimeout(() => {
      this.messages.push({
        sender: 'System',
        message: 'Thank you for your message. Our team will respond shortly.',
        timestamp: new Date()
      });
    }, 500);

    this.newMessage = '';
  }
}
