Create PROCEDURE dbo.sp_LibraryManagement
AS
BEGIN

    SET NOCOUNT ON;

    -- =========================================
    -- QUERY 1 : ALL BOOKS
    -- =========================================
    PRINT 'ALL BOOKS';

    SELECT *
    FROM Books_TBL;

    -- =========================================
    -- QUERY 2 : BOOKS WITH AUTHORS
    -- =========================================

    SELECT
        b.BookID,
        b.Title,
        b.Genre,
        b.Price,
        a.AuthorName,
        a.Address
    FROM Books_TBL b
    INNER JOIN Authors_TBL a
        ON b.AuthorID = a.AuthorID;

    -- =========================================
    -- QUERY 3 : LOAN DETAILS WITH MEMBERS
    -- =========================================
    PRINT 'LOAN DETAILS';

    SELECT
        l.LoanID,
        m.MemberName,
        m.Email,
        b.Title,
        l.LoanDate,
        l.ReturnDate
    FROM Loans_TBL l
    INNER JOIN Members_TBL m
        ON l.MemberID = m.MemberID
    INNER JOIN Books_TBL b
        ON l.BookID = b.BookID;

    -- =========================================
    -- QUERY 4 : EXPENSIVE BOOKS
    -- =========================================
    PRINT 'EXPENSIVE BOOKS';

    SELECT
        BookID,
        Title,
        Genre,
        Price
    FROM Books_TBL
    WHERE Price > 500;

    -- =========================================
    -- QUERY 5 : TOTAL LOANS
    -- =========================================
    PRINT 'TOTAL LOANS';

    SELECT
        COUNT(*) AS TotalLoans
    FROM Loans_TBL;

END
GO