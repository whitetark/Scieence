use master
DROP DATABASE ScieencePubDb

CREATE DATABASE ScieencePubDb
GO

USE ScieencePubDb;
GO

DROP TABLE PublicationSchema.Publications

CREATE TABLE PublicationSchema.Publications
(
    -- TableId INT IDENTITY(Starting, Increment By)
    PublicationId INT IDENTITY(1,1) PRIMARY KEY,
    Description VARCHAR(8000),
    Authors VARCHAR(2000),
    Subjects VARCHAR(1000),
    PublicationDate VARCHAR(255),
    PublicationType VARCHAR(255),
    PublicationYear INT,
    Language VARCHAR(36),
    URL VARCHAR(1000),
    Title VARCHAR(500),
    DOI VARCHAR(256),
)

CREATE TABLE PublicationSchema.Accounts
(
    AccountId INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(32),
    PasswordHash VARCHAR(1000),
    RefreshToken VARCHAR(2000),
    TokenCreated VARCHAR(2000),
    TokenExpires VARCHAR(2000)
)

DROP TABLE PublicationSchema.Accounts

CREATE TABLE PublicationSchema.Favourites
(
    FavId INT IDENTITY(1,1) PRIMARY KEY,
    AccountId INT REFERENCES PublicationSchema.Accounts(AccountId),
    PublicationId INT REFERENCES PublicationSchema.Publications(PublicationId)
)
DROP TABLE PublicationSchema.Favourites

SELECT * FROM PublicationSchema.Publications;

USE ScieencePubDb;
GO

BULK INSERT PublicationSchema.Publications
FROM 'C:\Users\white\Desktop\data.csv'
WITH
(
    DATAFILETYPE = 'char',
    FIRSTROW=2,
    ROWTERMINATOR='\n',
    FIELDTERMINATOR = '|',
    MAXERRORS = 2
)
GO