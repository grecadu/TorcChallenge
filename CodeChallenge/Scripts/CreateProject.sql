-- Run just if we are using just sql server 

Use [master]

Create database CodeChallenged


USE [CodeChallenged]
GO

/****** Object:  Table [dbo].[Customers]    Script Date: 8/30/2023 6:34:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers_CustomerId]
GO

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Products_ProductId]
GO



CREATE PROCEDURE CreateOrder
    @CustomerId INT,
    @ProductId INT,
    @Quantity INT
AS
BEGIN
    DECLARE @ProductPrice DECIMAL(18, 2)
    DECLARE @TotalCost DECIMAL(18, 2)

    -- Get the price of the product based on the ProductId
    SELECT @ProductPrice = Price
    FROM Products
    WHERE Id = @ProductId

    -- Calculate the total cost
    SET @TotalCost = @ProductPrice * @Quantity

    -- Insert the new order into the Orders table
    INSERT INTO Orders (CustomerId, ProductId, Quantity, Total)
    VALUES (@CustomerId, @ProductId, @Quantity, @TotalCost)
END


INSERT INTO Customers (Name)
VALUES ('Customer 1'),
       ('Customer 2'),
       ('Customer 3'),
       ('Customer 4'),
       ('Customer 5');

-- Insertar datos de ejemplo en la tabla Product
INSERT INTO Products (Name, Price)
VALUES ('Product 1', 10.99),
       ('Product 2', 19.99),
       ('Product 3', 5.99),
       ('Product 4', 14.49),
       ('Product 5', 7.99);

-- Insertar datos de ejemplo en la tabla Order
INSERT INTO [Orders] (CustomerId, ProductId, Quantity, Total)
VALUES (1, 1, 3, 32.97),
       (2, 3, 5, 29.95),
       (1, 2, 2, 39.98),
       (3, 4, 1, 14.49),
       (2, 5, 4, 31.96),
       (4, 1, 2, 21.98);
