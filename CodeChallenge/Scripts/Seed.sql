
-- This file need to be run if we are using migrations to create the db

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
