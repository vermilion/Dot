export interface UserSummary {
  userId: number;
  username: string;

  role: {
    roleId: number,
    title: string
  }
}
