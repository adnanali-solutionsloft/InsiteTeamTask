export enum AttendanceType {
    SeasonTicket,
    GameTicket
}

export interface Attendance {
    attendanceType: AttendanceType;
    memberId: number;
    barcode: string;
}