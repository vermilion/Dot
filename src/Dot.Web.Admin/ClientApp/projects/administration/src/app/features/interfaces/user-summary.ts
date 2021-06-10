export interface UserSummary {
  userId: number;
  username: string;

  lastLoginDate: Date;
  auditData: any;

  role: {
    roleId: number,
    title: string
  }
}
