CREATE TABLE ImportedData (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DateColumn DATE,
    LatinColumn NVARCHAR(10),
    RussianColumn NVARCHAR(10),
    IntegerColumn INT,
    DecimalColumn DECIMAL(18, 8)
);
