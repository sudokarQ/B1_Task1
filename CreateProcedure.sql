CREATE PROCEDURE CalculateSumAndMedian
AS
BEGIN
    DECLARE @SumIntegers FLOAT;
    DECLARE @MedianDecimal FLOAT;

    -- Calculate Sum of Integers
    SELECT @SumIntegers = SUM(CAST([Integer] AS FLOAT))
    FROM ImportedData;

    -- Calculate Median of Decimals
    ;WITH MedianCTE AS (
        SELECT [Decimal],
               ROW_NUMBER() OVER (ORDER BY [Decimal]) AS RowAsc,
               ROW_NUMBER() OVER (ORDER BY [Decimal] DESC) AS RowDesc
        FROM ImportedData
    )
    SELECT @MedianDecimal = AVG(CAST([Decimal] AS FLOAT))
    FROM MedianCTE
    WHERE RowAsc = RowDesc OR RowAsc + 1 = RowDesc OR RowAsc = RowDesc + 1;

    -- Return Sum and Median
    SELECT @SumIntegers AS SumIntegers, @MedianDecimal AS MedianDecimal;
END;
