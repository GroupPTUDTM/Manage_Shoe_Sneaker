CREATE DATABASE QLBG
GO
USE QLBG
GO


drop database QLBG

CREATE TABLE USERS (
	ID_KhachHang INT IDENTITY(1,1) ,
    HoTen NVARCHAR(60),
	NgaySinh DATETIME,
    DiaChi NVARCHAR(60),
	GioiTinh NVARCHAR(10),
    Email NVARCHAR(60),
    SoDienThoai NVARCHAR(13),
	TaiKhoan NVARCHAR(50),
	MatKhau NVARCHAR(50),
	Role BIT Default'0' ,
	PRIMARY KEY(ID_KhachHang)
);


CREATE TABLE THUONGHIEU (
    ID_ThuongHieu INT PRIMARY KEY,
    TenThuongHieu VARCHAR(50)
);

CREATE TABLE DANHMUC (
    ID_DanhMuc INT PRIMARY KEY,
    TenDanhMuc NVARCHAR(50)
);
CREATE TABLE Sizes (
    ID_Size INT PRIMARY KEY,
    Size INT
);
CREATE TABLE MAU (
    ID_Mau INT PRIMARY KEY,
    TenMau NVARCHAR(50)
);
CREATE TABLE HINHANH (
    ID_Anh INT PRIMARY KEY,
    AnhChinh VARCHAR(255),
    Anh1 VARCHAR(255),
    Anh2 VARCHAR(255),
    
);
CREATE TABLE SANPHAM (
    ID_SanPham INT PRIMARY KEY,
    TenSanPham NVARCHAR(100),
    ID_ThuongHieu INT,
    ID_DanhMuc INT,
    ID_Anh INT,
    ID_Size INT,
    ID_Mau INT,
    Mota NVARCHAR(255), -- Added description column
    DonViGia DECIMAL(10, 2),
    SoLuongTon INT,
    FOREIGN KEY (ID_ThuongHieu) REFERENCES THUONGHIEU(ID_ThuongHieu),
    FOREIGN KEY (ID_DanhMuc) REFERENCES DANHMUC(ID_DanhMuc),
    FOREIGN KEY (ID_Anh) REFERENCES HINHANH(ID_Anh),
    FOREIGN KEY (ID_Size) REFERENCES Sizes(ID_Size),
    FOREIGN KEY (ID_Mau) REFERENCES MAU(ID_Mau)
);
CREATE TABLE DATHANG (
    ID_DatHang INT PRIMARY KEY,
    ID_KhachHang INT,
    NgayDat DATE,
    SoLuong DECIMAL(10, 2),
    FOREIGN KEY (ID_KhachHang) REFERENCES USERS(ID_KhachHang)
);

CREATE TABLE CT_DATHANG (
    ID_CTDatHang INT PRIMARY KEY,
    ID_DatHang INT,
    ID_SanPham INT,
    SoLuong INT,
    DonViGia DECIMAL(10, 2),
    FOREIGN KEY (ID_DatHang) REFERENCES DATHANG(ID_DatHang),
    FOREIGN KEY (ID_SanPham) REFERENCES SANPHAM(ID_SanPham)
);


-- Thêm dữ liệu vào bảng THUONGHIEU
INSERT INTO THUONGHIEU (ID_ThuongHieu, TenThuongHieu) VALUES
    (1, N'Fila'),
    (2, N'Nike'),
    (3, N'Adidas'),
    (4, N'Puma'),
    (5, N'Converse'),
    (6, N'Vans'),
    (7, N'New Balance'),
    (8, N'Reebok'),
    (9, N'Under Armour'),
    (10, N'Asics');

-- Thêm dữ liệu vào bảng DANHMUC
INSERT INTO DANHMUC (ID_DanhMuc, TenDanhMuc) VALUES
    (1, N'Giày thể thao'),
    (2, N'Giày nam'),
    (3, N'Giày nữ'),
    (4, N'Giày Trẻ Em'),
    (5, N'Giày chạy bộ'),
    (6, N'Giày công sở'),
    (7, N'Giày sandal'),
    (8, N'Giày tây')

-- Thêm dữ liệu vào bảng Sizes
INSERT INTO Sizes (ID_Size, Size) VALUES
    (1, 35),
    (2, 36),
    (3, 37),
    (4, 38),
    (5, 39),
    (6, 40),
    (7, 41),
    (8, 42),
    (9, 43);

-- Thêm dữ liệu vào bảng Colors
INSERT INTO MAU (ID_Mau, TenMau) VALUES
    (1, N'Trắng'),
    (2, N'Đen'),
    (3, N'Đỏ'),
    (4, N'Xám'),
    (5, N'Màu sắc');
  

-- Thêm dữ liệu vào bảng IMAGES
INSERT INTO HINHANH(ID_Anh, AnhChinh, Anh1, Anh2) VALUES
    (1, 'Giay001-avt.jpg', 'Giay001-a1.jpg', 'Giay001-a2.jpg'),
    (2, 'Giay002-avt.jpg', 'Giay002-a1.jpg', 'Giay002-a2.jpg'),
    (3, 'Giay003-avt.jpg', 'Giay003-a1.jpg', 'Giay003-a2.jpg'),
    (4, 'Giay004-avt.jpg', 'Giay004-a1.jpg', 'Giay004-a2.jpg'),
    (5, 'Giay005-avt.jpg', 'Giay005-a1.jpg', 'Giay005-a2.jpg'),
    (6, 'Giay006-avt.jpg', 'Giay006-a1.jpg', 'Giay006-a2.jpg'),
    (7, 'Giay007-avt.jpg', 'Giay007-a1.jpg', 'Giay007-a2.jpg'),
    (8, 'Giay008-avt.jpg', 'Giay008-a1.jpg', 'Giay008-a2.jpg'),
    (9, 'Giay009-avt.jpg', 'Giay009-a1.jpg', 'Giay009-a2.jpg'),
    (10,'Giay010-avt.jpg', 'Giay010-a1.jpg', 'Giay010-a2.jpg'),
    (11, 'Giay011-avt.jpg', 'Giay011-a1.jpg', 'Giay011-a2.jpg'),
    (12, 'Giay012-avt.jpg', 'Giay012-a1.jpg', 'Giay012-a2.jpg'),
    (13, 'Giay013-avt.jpg', 'Giay013-a1.jpg', 'Giay013-a2.jpg'),
    (14, 'Giay014-avt.jpg', 'Giay014-a1.jpg', 'Giay014-a2.jpg'),
    (15, 'Giay015-avt.jpg', 'Giay015-a1.jpg', 'Giay015-a2.jpg'),
    (16, 'Giay016-avt.jpg', 'Giay016-a1.jpg', 'Giay016-a2.jpg'),
    (17, 'Giay017-avt.jpg', 'Giay017-a1.jpg', 'Giay017-a2.jpg'),
    (18, 'Giay018-avt.jpg', 'Giay018-a1.jpg', 'Giay018-a2.jpg'),
    (19, 'Giay019-avt.jpg', 'Giay019-a1.jpg', 'Giay019-a2.jpg'),
    (20, 'Giay020-avt.jpg', 'Giay020-a1.jpg', 'Giay020-a2.jpg');


-- Thêm dữ liệu vào bảng SANPHAM
INSERT INTO SANPHAM (ID_SanPham, TenSanPham, ID_ThuongHieu, ID_DanhMuc, ID_Anh, ID_Size, ID_Mau, Mota, DonViGia, SoLuongTon) VALUES
    (1, N'Fila Running Shoes 1', 1, 1, 1, 1, 1, N'High-performance running shoes with advanced technology.', 79.99, 100),
    (2, N'Fila Running Shoes 2', 1, 1, 2, 2, 2, N'Durable running shoes designed for all terrains.', 89.99, 150),
    (3, N'Fila Sneakers 1', 1, 1, 3, 3, 3, N'Classic sneakers with a stylish and comfortable design.', 99.99, 120),
    (4, N'Fila Sneakers 2', 1, 1, 4, 4, 4, N'Modern sneakers with a sleek and trendy look.', 109.99, 80),
    (5, N'Fila Casual Shoes 1', 1, 2, 5, 5, 5, N'Casual shoes for everyday comfort and style.', 69.99, 200),
    (6, N'Fila Casual Shoes 2', 1, 2, 3, 1, 5, N'Fashionable casual shoes for a relaxed look.', 79.99, 180),
    (7, N'Fila Sandals 1', 1, 7, 7, 5, 4, N'Comfortable sandals for a day at the beach or casual outings.', 49.99, 90),
    (8, N'Fila Sandals 2', 1, 7, 8, 1, 3, N'Stylish sandals with a modern design for various occasions.', 59.99, 110),
    (9, N'Fila Boots 1', 1, 2, 9, 4, 2, N'Durable boots for outdoor activities and harsh conditions.', 129.99, 70),
    (10, N'Fila Boots 2', 1, 2, 10, 9, 5, N'Trendy boots for a fashionable and bold statement.', 139.99, 60),
    (11, N'Fila Running Shoes 3', 1, 3, 11, 2, 3, N'High-performance running shoes with advanced technology.', 99.99, 130),
    (12, N'Fila Running Shoes 4', 1, 4, 12, 3, 4, N'Durable running shoes designed for all terrains.', 109.99, 110),
    (13, N'Fila Sneakers 3', 1, 1, 13, 4, 5, N'Classic sneakers with a stylish and comfortable design.', 89.99, 170),
    (14, N'Fila Sneakers 4', 1, 1, 14, 5, 1, N'Modern sneakers with a sleek and trendy look.', 119.99, 90),
    (15, N'Fila Casual Shoes 3', 1, 3, 15, 3, 2, N'Casual shoes for everyday comfort and style.', 79.99, 200),
    (16, N'Fila Casual Shoes 4', 1, 4, 16, 2, 3, N'Fashionable casual shoes for a relaxed look.', 89.99, 180),
    (17, N'Fila Sandals 3', 1, 7, 17, 3, 4, N'Comfortable sandals for a day at the beach or casual outings.', 59.99, 130),
    (18, N'Fila Sandals 4', 1, 7, 18, 2, 5, N'Stylish sandals with a modern design for various occasions.', 69.99, 150),
    (19, N'Fila Boots 3', 1, 2, 19, 7, 1, N'Durable boots for outdoor activities and harsh conditions.', 149.99, 70),
    (20, N'Fila Boots 4', 1, 2, 20, 8, 2, N'Trendy boots for a fashionable and bold statement.', 159.99, 60);



	SELECT * FROM SANPHAM

-- Thêm dữ liệu vào bảng USERS
INSERT INTO USERS (HoTen, NgaySinh, DiaChi, GioiTinh, Email, SoDienThoai, TaiKhoan, MatKhau, Role) VALUES
    (N'Nguyen Van A', '1990-01-01', N'123 Street, City', N'Nam', 'nguyenvana@gmail.com', '0123456789', 'user1', 'password1', 0),
    (N'Tran Thi B', '1985-05-15', N'456 Street, Town', N'Nữ', 'tranthib@gmail.com', '0987654321', 'user2', 'password2', 0),
    (N'Le Van C', '1992-08-20', N'789 Street, Village', N'Nam', 'levanc@gmail.com', '0123456789', 'user3', 'password3', 0),
    (N'Pham Thanh D', '1988-12-10', N'101 Street, Suburb', N'Nam', 'phamthanhd@gmail.com', '0987654321', 'user4', 'password4', 0),
    (N'Vo Thi E', '1995-03-25', N'202 Street, County', N'Nữ', 'vothie@gmail.com', '0123456789', 'user5', 'password5', 0),
    (N'Truong Van F', '1982-06-18', N'303 Street, District', N'Nam', 'truongvanf@gmail.com', '0987654321', 'user6', 'password6', 0),
    (N'Nguyen Thi G', '1997-09-05', N'404 Street, Province', N'Nữ', 'nguyenthig@gmail.com', '0123456789', 'user7', 'password7', 0),
    (N'Hoang Van H', '1980-02-28', N'505 Street, State', N'Nam', 'hoangvanh@gmail.com', '0987654321', 'user8', 'password8', 0),
    (N'Le Thi I', '1994-11-15', N'606 Street, Country', N'Nữ', 'lethii@gmail.com', '0123456789', 'user9', 'password9', 0),
    (N'Nguyen Van J', '1987-07-08', N'707 Street, Republic', N'Nam', 'nguyenvanj@gmail.com', '0987654321', 'user10', 'password10', 0);

-- Thêm dữ liệu vào bảng DATHANG
INSERT INTO DATHANG (ID_DatHang, ID_KhachHang, NgayDat, SoLuong) VALUES
    (1, 1, '2023-01-01', 2),
    (2, 2, '2023-01-02', 3),
    (3, 3, '2023-01-03', 1),
    (4, 4, '2023-01-04', 4),
    (5, 5, '2023-01-05', 2),
    (6, 6, '2023-01-06', 1),
    (7, 7, '2023-01-07', 3),
    (8, 8, '2023-01-08', 2),
    (9, 9, '2023-01-09', 1),
    (10, 10, '2023-01-10', 5);

-- Thêm dữ liệu vào bảng CT_DATHANG
INSERT INTO CT_DATHANG (ID_CTDatHang, ID_DatHang, ID_SanPham, SoLuong, DonViGia) VALUES
    (1, 1, 1, 2, 79.99),
    (2, 2, 3, 1, 99.99),
    (3, 3, 5, 1, 69.99),
    (4, 4, 7, 4, 49.99),
    (5, 5, 9, 2, 129.99),
    (6, 6, 2, 1, 89.99),
    (7, 7, 4, 3, 109.99),
    (8, 8, 6, 2, 79.99),
    (9, 9, 8, 1, 59.99),
    (10, 10, 10, 5, 139.99);


	Select * from USERS
	
	
	
	  DELETE FROM USERS WHERE ID_KhachHang='0';

	  INSERT INTO USERS (HoTen, NgaySinh, DiaChi, GioiTinh, Email, SoDienThoai, TaiKhoan, MatKhau, Role) VALUES
    (N'Nguyen Van An', '1990-01-01', N'123 Street, City', N'Nam', 'nguyenvanan@gmail.com', '0123456789', 'admin', '123', 1)

	-- Create Sequence
