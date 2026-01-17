CREATE DATABASE OrderManagement;
GO

USE OrderManagement;
GO

CREATE TABLE products (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) NOT NULL UNIQUE,
    sku NVARCHAR(100) NOT NULL UNIQUE,
    description NVARCHAR(MAX) NULL,
    price DECIMAL(18,2) NOT NULL,
    stock_quantity INT NOT NULL,
    category NVARCHAR(100) NOT NULL,
    created_at DATETIME2 DEFAULT GETDATE(),
    updated_at DATETIME2 DEFAULT GETDATE()
);

CREATE TABLE orders (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT NOT NULL,
    order_number NVARCHAR(50) NOT NULL UNIQUE,
    customer_name NVARCHAR(100) NOT NULL,
    quantity INT NOT NULL,
    customer_email NVARCHAR(255) NOT NULL UNIQUE,
    order_date DATE NOT NULL,
    delivery_date DATE NULL,
    created_at DATETIME2 DEFAULT GETDATE(),
    updated_at DATETIME2 DEFAULT GETDATE(),

    CONSTRAINT FK_orders_products
        FOREIGN KEY (product_id)
        REFERENCES products(id)
);

EXEC sp_help products;
EXEC sp_help orders;

SELECT * FROM products;
SELECT * FROM orders;
