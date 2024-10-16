CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) UNIQUE COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

CREATE TABLE cars (
    -- Every table's first column should be an id
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    -- we will set up stricter validation with our C# model
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    make VARCHAR(255) NOT NULL,
    model VARCHAR(255) NOT NULL,
    year SMALLINT UNSIGNED NOT NULL,
    price MEDIUMINT UNSIGNED NOT NULL,
    imgUrl TEXT NOT NULL,
    description TEXT,
    engineType ENUM(
        'V6',
        'V8',
        'V10',
        '4-cylinder',
        'unknown'
    ) NOT NULL,
    -- color VARCHAR(255)
    color TINYTEXT NOT NULL,
    mileage MEDIUMINT UNSIGNED NOT NULL,
    hasCleanTitle BOOLEAN NOT NULL DEFAULT true,
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
);

CREATE TABLE houses (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    sqft INT NOT NULL,
    bedrooms INT NOT NULL,
    bathrooms DOUBLE NOT NULL,
    imgUrl VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL,
    price INT NOT NULL,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
);

INSERT INTO
    houses (
        sqft,
        bedrooms,
        bathrooms,
        imgUrl,
        description,
        price,
        creatorId
    )
VALUES (
        4500,
        4,
        3.5,
        'https://images.unsplash.com/photo-1452626212852-811d58933cae?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8NCUyMGJlZHJvb20lMjBob3VzZXxlbnwwfHwwfHx8MA%3D%3D',
        'Wonderful, modern, 2 bedroom/2bathroom house',
        160000,
        '671027deae0bf51994be4661'
    );

SELECT houses.*, accounts.*
FROM houses
    JOIN accounts ON houses.creatorId = accounts.id;

DROP TABLE cars;

DROP TABLE houses;

INSERT INTO
    cars (
        make,
        model,
        year,
        price,
        imgUrl,
        description,
        engineType,
        color,
        mileage,
        hasCleanTitle,
        creatorId
    )
VALUES (
        'mazda',
        'miata',
        2009,
        6000,
        'https://images.unsplash.com/photo-1725199583250-9f59567ba965?q=80&w=2126&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D',
        'jeremys car',
        '4-cylinder',
        'blue',
        200000,
        true,
        '65f87bc1e02f1ee243874743'
    );

SELECT cars.*, accounts.*
FROM cars
    JOIN accounts ON cars.creatorId = accounts.id;