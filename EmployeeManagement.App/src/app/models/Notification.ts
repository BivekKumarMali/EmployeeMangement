export interface NotificationDetails {
  nid: number;
  name: string;
  date: string;
  action: string;
  did: number;
}

export interface IsRead {
  nid: number;
  rid: number;
  userId: string;
}
