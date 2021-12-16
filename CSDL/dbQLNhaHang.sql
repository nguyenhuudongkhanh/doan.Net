
CREATE DATABASE QL_NHAHANG_2
USE QL_NHAHANG_2

CREATE TABLE TBL_KHACHHANG
(
	PK_iKhachHangID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	sTenKhachHang NVARCHAR(50),
	sSoDienThoai VARCHAR(11),
	sDiaChi NVARCHAR(150),
	iDiemSo INT,
)

GO
CREATE TABLE TBL_GIAMGIA
(
	PK_sGiamGiaID VARCHAR(50) NOT NULL PRIMARY KEY,
	sTenGiamGia NVARCHAR(150),
	fTiLeGiamGia FLOAT, -- TÍNH THEO PHẦN TRĂM ( VÍ DỤ MÓN ĂN 100K GIẢM GIÁ 5% => 100K - 100K * 5%)
	dNgayBatDau DATE,
	dNgayKetThuc DATE,
	bTrangThai BIT DEFAULT 1, -- HIỂN THỊ GIẢM GIÁ HOẶC KHÔNG
)
GO
CREATE TABLE TBL_CHUCVU
(
	PK_sChucVuID VARCHAR(50) NOT NULL PRIMARY KEY,
	sTenChucVu NVARCHAR(150) NOT NULL,
)
GO

CREATE TABLE TBL_NHANVIEN
(
	PK_iNhanVienID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	FK_sChucVuID VARCHAR(50),
	sTenNhanVien NVARCHAR(50),
	sTenDangNhap VARCHAR(50),
	sMatKhau VARCHAR(50),
	sSoDienThoai CHAR(11),
	sEmail VARCHAR(50),
	sDiaChi NVARCHAR(150),
	sGioiTinh BIT, --1 LÀ NAM 0 LÀ NỮ
	bTrangThaiTaiKhoan BIT DEFAULT 1, --Tài khoản còn sử dụng được hay không
)



GO
CREATE TABLE TBL_LOAIBANAN
(
	PK_sLoaiBanAnID VARCHAR(30) NOT NULL PRIMARY KEY,
	sTenLoaiBanAn NVARCHAR(150) NOT NULL,
	fPhuThu FLOAT -- PHỤ THU BÀN ĂN
)
GO
CREATE TABLE TBL_BANAN
(
	PK_iBanAnID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	FK_sLoaiBanAnID VARCHAR(30),
	sTenBanAn NVARCHAR(50),
	bTrangThaiBanAn BIT DEFAULT 0, --1 là đang được sử dụng, 0 là còn trống

)
GO
CREATE TABLE TBL_LOAIMONAN
(
	PK_sLoaiMonAnID VARCHAR(30) NOT NULL PRIMARY KEY,
	sTenLoaiMonAn NVARCHAR(150) NOT NULL,
)
GO
CREATE TABLE TBL_MONAN
(
	PK_iMonAnID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	FK_sLoaiMonAnID VARCHAR(30) NOT NULL,
	FK_sGiamGiaID VARCHAR(50) NOT NULL,
	sTenMonAn NVARCHAR(150) NOT NULL,
	fGiaMonAn FLOAT NOT NULL,
	sHinhAnhMonAn VARCHAR(150),
	bTrangThai BIT DEFAULT 1, -- Hiển thị món ăn hay không
)


GO
CREATE TABLE TBL_HINHANH --Một món ăn có thể có nhiều hình ảnh
(
	PK_iHinhAnhID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	FK_iMonAnID INT,
	sHinhAnh VARCHAR(150)
)
GO

CREATE TABLE TBL_HOADON
(
	PK_iHoaDonID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	FK_iKhachHangID INT, --CÓ THỂ NULL
	FK_iNhanVienID INT NOT NULL, -- NHÂN VIÊN THỰC HIỆN HÓA ĐƠN
	FK_iBanAnID INT NOT NULL,
	dNgayLapHoaDon DATE
)

GO
CREATE TABLE TBL_CHITIETHOADON
(
	PK_iChiTietHoaDonID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	FK_iHoaDonID INT NOT NULL,
	FK_iMonAn INT NOT NULL,
	iSoLuong INT NOT NULL DEFAULT 1
)
GO
CREATE TABLE TBL_HINHTHUCTHANHTOAN
(
	PK_sHinhThucThanhToanID VARCHAR(30) NOT NULL PRIMARY KEY,
	sTenHinhThucThanhToan NVARCHAR(50) NOT NULL,
	bTrangThai BIT DEFAULT 1, -- HIỂN THỊ HOẶC KHÔNG
)
GO
CREATE TABLE TBL_THANHTOAN
(
	PK_iThanhToanID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	FK_iHoaDonID INT NOT NULL,
	FK_sHinhThucThanhToanID VARCHAR(30) DEFAULT 'TIENMAT',
	fTongTien FLOAT NOT NULL,
	dNgayThanhToan DATE,
)
--Phần liên kết các bản
SELECT * FROM TBL_THANHTOAN

ALTER TABLE TBL_NHANVIEN
ADD CONSTRAINT FK_TBLNHANVIEN_TBLCHUCVU FOREIGN KEY (FK_SCHUCVUID) REFERENCES TBL_CHUCVU(PK_SCHUCVUID)
GO
ALTER TABLE TBL_BANAN
ADD CONSTRAINT FK_TBLBANAN_TBLLOAIBANAN FOREIGN KEY (FK_SLOAIBANANID) REFERENCES TBL_LOAIBANAN(PK_SLOAIBANANID)

GO
ALTER TABLE TBL_MONAN
ADD CONSTRAINT FK_TBLMONAN_TBLOAIMONAN FOREIGN KEY(FK_SLOAIMONANID) REFERENCES TBL_LOAIMONAN(PK_SLOAIMONANID)
GO
ALTER TABLE TBL_MONAN
ADD CONSTRAINT FK_MONAN_TBLGIAMGIA FOREIGN KEY(FK_SGIAMGIAID) REFERENCES TBL_GIAMGIA(PK_SGIAMGIAID)
GO
ALTER TABLE TBL_HINHANH
ADD CONSTRAINT FK_TBLHINHANH_TBLMONAN FOREIGN KEY(FK_iMonAnID) REFERENCES TBL_MONAN(PK_IMONANID)
GO
ALTER TABLE TBL_HOADON
ADD CONSTRAINT FK_TBLHOADON_TBLKHACHHANG FOREIGN KEY(FK_IKHACHHANGID) REFERENCES TBL_KHACHHANG(PK_IKHACHHANGID)
GO
ALTER TABLE TBL_HOADON
ADD CONSTRAINT FK_TBLHOADON_TBLNHANVIEN FOREIGN KEY(FK_INHANVIENID) REFERENCES TBL_NHANVIEN(PK_INHANVIENID)
GO
ALTER TABLE TBL_HOADON
ADD CONSTRAINT FK_TBLHOADON_TBLBANAN FOREIGN KEY(FK_IBANANID) REFERENCES TBL_BANAN(PK_IBANANID)
GO

ALTER TABLE TBL_CHITIETHOADON
ADD CONSTRAINT FK_TBLCHITIETHOADON_TBLHOADON FOREIGN KEY(FK_IHOADONID) REFERENCES TBL_HOADON(PK_IHOADONID)
GO
ALTER TABLE TBL_THANHTOAN
ADD CONSTRAINT FK_TBLTHANHTOAN_TBLHOADON FOREIGN KEY(FK_IHOADONID) REFERENCES TBL_HOADON(PK_IHOADONID)
GO
ALTER TABLE TBL_THANHTOAN
ADD CONSTRAINT FK_TBLTHANHTOAN_TBLHTTT FOREIGN KEY(FK_sHinhThucThanhToanID) REFERENCES TBL_HINHTHUCTHANHTOAN(PK_sHinhThucThanhToanID)

-- PHẦN NHẬP DỮ LIỆU
--bảng giảm giá
INSERT INTO TBL_GIAMGIA(PK_sGiamGiaID, sTenGiamGia, fTiLeGiamGia)
VALUES	('CHUAGIAMGIA', N'Chưa được giảm giá', 0),
		('GIAMGIA10', N'Giảm giá 10%', 10),
		('GIAMGIACUOINGAY', N'Cuối ngày giảm giá 70%', 70)
--bảng chức vụ
INSERT INTO TBL_CHUCVU
VALUES	('THUNGAN', N'Nhân viên thu ngân'),
		('TAPVU', N'Nhân viên tạp vụ'),
		('DAUBEP', N'Nhân viên đầu bếp')
-- bảng nhân viên
INSERT INTO TBL_NHANVIEN(FK_sChucVuID, sTenNhanVien, sTenDangNhap, sMatKhau, sGioiTinh)
VALUES	('THUNGAN', N'THU NGÂN', 'thungantest', '123', 1),
		('TAPVU', N'TẠP VỤ', 'tapvutest', '123', 1),
		('DAUBEP', N'ĐẦU BẾP', 'daubeptest', '123', 1)
-- bảng loại bàn ăn
INSERT INTO TBL_LOAIBANAN(PK_sLoaiBanAnID, sTenLoaiBanAn, fPhuThu)
VALUES	('VIP', N'Loại bàn ăn VIP', 50000),
		('TAMTRUNG', N'Loại bàn ăn tầm trung', 30000),
		('BTHUONG', N'Loại bàn ăn bình thường', 0)
--bảng bàn ăn
INSERT INTO TBL_BANAN(FK_sLoaiBanAnID, sTenBanAn)
VALUES	('VIP', N'VIP-1'),('VIP', N'VIP-2'), ('VIP', N'VIP-3'),('VIP', N'VIP-4'),('VIP', N'VIP-5'),
		('TAMTRUNG', N'TT-1'),('TAMTRUNG', N'TT-2'),('TAMTRUNG', N'TT-3'),('TAMTRUNG', N'TT-4'),
		('BTHUONG', N'B-1'), ('BTHUONG', N'B-2'),('BTHUONG', N'B-3'),('BTHUONG', N'B-4'),('BTHUONG', N'B-5')
-- bảng loại món ăn
INSERT INTO TBL_LOAIMONAN
VALUES	('M-CHAY', N'Món ăn chay'),
		('M-MAN', N'Món ăn mặn'),
		('M-TRANGMIENG', N'Món ăn tráng miệng'),
		('NC', N'Nước uống')	
		
-- bảng món ăn
INSERT INTO TBL_MONAN(FK_sLoaiMonAnID, FK_sGiamGiaID, sTenMonAn, fGiaMonAn, sHinhAnhMonAn)
VALUES	('M-CHAY', 'CHUAGIAMGIA', N'Giò lụa chay', 67000, 'gio-lua-chay.jpg'),
		('M-CHAY', 'CHUAGIAMGIA', N'Miếng xào', 49000, 'mieng-xao.jpg'),
		('M-CHAY', 'CHUAGIAMGIA', N'Súp bí đỏ', 52000, 'sup-bi-do.jpg'),
		('M-CHAY', 'CHUAGIAMGIA', N'Canh nấm hạt sen', 99000, 'canh-nam-hat-sen.jpg'),
		('M-CHAY', 'CHUAGIAMGIA', N'Chè sắn chay', 20000, 'che-san-chay.jpg'),
--https://eva.vn/bep-eva/20-mon-chay-ngon-thong-dung-don-gian-de-lam-tai-nha-c162a429084.html
		('M-MAN', 'CHUAGIAMGIA', N'Sườn xào chua ngọt',65000, 'suon-xao-chua-ngot-768x512.jpg'),
		('M-MAN', 'CHUAGIAMGIA', N'Thịt rang cháy cạnh',55000, 'thi-rang-chay-canh.jpg'),
		('M-MAN', 'CHUAGIAMGIA', N'Thịt viên chiên',25000, 'thit-vien-chien.jpg'),
		('M-MAN', 'CHUAGIAMGIA', N'Thịt kho đậu phụ nấm',65000, 'thit-kho-dau-phu-nam.png'),
		('M-MAN', 'CHUAGIAMGIA', N'Mực nhồi thịt hấp',65000, 'muc-nhoi-thit-hap-742x420.jpg'),
--https://jamja.vn/blog/mon-an-man-ngay-he.html
		('M-TRANGMIENG', 'CHUAGIAMGIA', N'Bánh da lợn', 25000, 'Banh-da-lon.jpg'),
		('M-TRANGMIENG', 'CHUAGIAMGIA', N'Bánh pía', 25000, 'Banh-pia-768x768.jpg'),
		('M-TRANGMIENG', 'CHUAGIAMGIA', N'Sương sáo mật ong', 45000, 'Mon-suong-sao-mat-ong.jpg'),
--https://dattiectainha24h.vn/mon-trang-mieng-viet-nam/
		('NC', 'CHUAGIAMGIA', N'Nước lọc', 7000, 'not-image'),
		('NC', 'CHUAGIAMGIA', N'Bia heniken', 19000, 'no-image')
--Bảng hóa đơn
SET DATEFORMAT DMY
INSERT INTO TBL_HOADON(FK_iNhanVienID, FK_iBanAnID, dNgayLapHoaDon)
VALUES	(1, 1, '6/11/2021')
--Bảng chi tiết hóa đơn
INSERT INTO TBL_CHITIETHOADON
VALUES	(1, 1, 1),(1, 2, 3),(1, 3, 2) --THỰC ĐƠN CÓ 3 MÓN ĂN
-- bảng hình thức thanh toán
INSERT INTO tbl_HinhThucThanhToan
VALUES	('TIENMAT', N'Tiền mặt', 1),
		('MONO', 'MOMO', 1),
		('TTD', N'Thẻ tín dụng', 1)
-- bảng thanh toán

-- Viết hàm bên WF để tính tổng tiền dễ hơn



--select * from TBL_HOADON

--select * from tbl_chitiethoadon where FK_iHoaDonID = 1
-- --Tính tổng tiền = Tổng(Giá sản phẩm - Giá sản phẩm*giảm giá) + phụ thu bàn ăn
-- select fGiaMonAn from TBL_MONAN
-- --Lấy tiền phụ thu
-- select fPhuThu from TBL_BANAN, TBL_LOAIBANAN where TBL_BANAN.FK_sLoaiBanAnID = TBL_LOAIBANAN.PK_sLoaiBanAnID and PK_iBanAnID = 1
----Lấy tiền 1 sản phẩm (giá sản phẩm - giá sản phẩm * giảm giá)
--select (fGiaMonAn - fGiaMonAn*fTiLeGiamGia) from TBL_MONAN, TBL_GIAMGIA where TBL_MONAN.FK_sGiamGiaID = TBL_GIAMGIA.PK_sGiamGiaID and PK_iMonAnID = 1
--select (fGiaMonAn - fGiaMonAn*fTiLeGiamGia) from TBL_MONAN, TBL_GIAMGIA where TBL_MONAN.FK_sGiamGiaID = TBL_GIAMGIA.PK_sGiamGiaID and PK_iMonAnID = 2
--select (fGiaMonAn - fGiaMonAn*fTiLeGiamGia) from TBL_MONAN, TBL_GIAMGIA where TBL_MONAN.FK_sGiamGiaID = TBL_GIAMGIA.PK_sGiamGiaID and PK_iMonAnID = 3

--select * from TBL_HOADON, TBL_CHITIETHOADON where TBL_HOADON.PK_iHoaDonID = TBL_CHITIETHOADON.FK_iHoaDonID and PK_iHoaDonID = 1

