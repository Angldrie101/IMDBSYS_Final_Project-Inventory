Create Database INVENTORY

USE INVENTORY

CREATE TABLE USERACCOUNT (
    USERID integer IDENTITY(1,1) primary key,
	USERNAME varchar (50) not null,
    USERPASSWORD varchar (50) not null,
	ROLEID int,
);

CREATE TABLE PRODUCTINFO (
    PRODUCTCODE integer primary key,
	PRODUCTNAME varchar (50) not null,
	PRODUCTQNTY integer not null,
	PRODUCTPRICE varchar (50) not null
);

INSERT PRODUCTINFO (PRODUCTCODE, PRODUCTNAME, PRODUCTQNTY, PRODUCTPRICE) VALUES (1, 'Coca-Cola', 20000, '270.00'),
																				(2, 'Sprite', 10000, '270.00'),
																				(3, 'Pepsi', 15000, '270.00'),
																				(4, 'RC Cola', 8000, '270.00'),
																				(5, '7-Up', 9000, '270.00'),
																				(6, 'Royal', 16000, '270.00'),
																				(7, 'Fanta', 17000, '270.00')


CREATE TABLE PURCHASE (
    PRODUCTCODE integer primary key,
	PRODUCTNAME varchar (50) not null,
	PRODUCTQNTY integer not null,
	PRODUCTPRICE varchar (50) not null
);

CREATE TABLE [dbo].[Role](
	[roleId] [int] IDENTITY(1,1) NOT NULL,
	[roleName] [nvarchar](50) NULL,
	[roleDescription] [nvarchar](100) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

USE INVENTORY
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([roleId], [roleName], [roleDescription]) VALUES (1, N'EMPLOYEE', N'EMPLOYEE ')
INSERT [dbo].[Role] ([roleId], [roleName], [roleDescription]) VALUES (2, N'MANAGER', N'MANAGER')
INSERT [dbo].[Role] ([roleId], [roleName], [roleDescription]) VALUES (3, N'ADMIN', N'ADMIN')

SET IDENTITY_INSERT [dbo].[Role] OFF

CREATE VIEW vw_all_product_info AS
SELECT PRODUCTCODE, PRODUCTNAME, PRODUCTQNTY, PRODUCTPRICE FROM PRODUCTINFO

SELECT * FROM vw_all_product_info

CREATE PROCEDURE sp_ProductInfo AS
SELECT PRODUCTCODE, PRODUCTNAME, PRODUCTQNTY, PRODUCTPRICE FROM PRODUCTINFO
GO;
EXEC sp_ProductInfo

SELECT * FROM USERACCOUNT

SELECT * FROM PRODUCTINFO

SELECT * FROM PURCHASE

