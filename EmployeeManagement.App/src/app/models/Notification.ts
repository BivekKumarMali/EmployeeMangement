export interface Notification {
  userNotification: NotificationDetails;
  count: number;
}

export interface NotificationDetails {
  nid: number;
  name: string;
  date: string;
  action: string;
  did: number;
}
