using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Chương trình Xếp loại Sinh viên ===\n");

        // TODO 1: Khai báo biến thông tin sinh viên
        string hoVaTen = "Bùi Quốc Khánh";   // Thay bằng tên của bạn
        double diem = 7.5;                 // Thay bằng điểm của bạn

        // TODO 2: In ra thông tin sinh viên
        Console.WriteLine($"Họ tên: {hoVaTen}");
        Console.WriteLine($"Điểm: {diem}\n");

        // TODO 3: Xếp loại theo tiêu chí cho trước
        string xepLoai;

        if (diem >= 8.5)
            xepLoai = "Giỏi";
        else if (diem >= 7.0)
            xepLoai = "Khá";
        else if (diem >= 5.5)
            xepLoai = "Trung bình";
        else
            xepLoai = "Yếu";

        Console.WriteLine($"Xếp loại: {xepLoai}\n");

        // TODO 4: Bảng điểm 3 sinh viên
        string[] tenSV = { "Bùi Quốc Khánh", "Trần Thị B", "Lê Văn C" };
        double[] diemSV = { 8.5, 7.2, 5.8 };

        Console.WriteLine("=== Bảng Điểm ===");

        for (int i = 0; i < tenSV.Length; i++)
        {
            // TODO 5: In tên – điểm – xếp loại từng sinh viên
            string loai;

            if (diemSV[i] >= 8.5)
                loai = "Giỏi";
            else if (diemSV[i] >= 7.0)
                loai = "Khá";
            else if (diemSV[i] >= 5.5)
                loai = "Trung bình";
            else
                loai = "Yếu";

            Console.WriteLine($"{tenSV[i],-15} | Điểm: {diemSV[i],4} | Xếp loại: {loai}");
        }

        // TODO 6: Dùng while để tính tổng điểm
        double tongDiem = 0;
        int j = 0;

        while (j < diemSV.Length)
        {
            tongDiem += diemSV[j];
            j++;
        }

        Console.WriteLine($"\nTổng điểm: {tongDiem}");
        Console.WriteLine($"Điểm trung bình: {tongDiem / diemSV.Length:F2}");
    }
}
