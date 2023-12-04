use master

CREATE DATABASE Publications
GO

USE Publications
GO

CREATE SCHEMA ScieenceSchema
GO

CREATE TABLE ScieenceSchema.Publication
(
    -- TableId INT IDENTITY(Starting, Increment By)
    PublicationId INT IDENTITY(1,1) PRIMARY KEY,
    Description VARCHAR(8000),
    Authors VARCHAR(1000),
    Subjects VARCHAR(1000),
    PublicationDate VARCHAR(255),
    PublicationType VARCHAR(255),
    PublicationYear INT,
    Language VARCHAR(36),
    URL VARCHAR(1000),
    Title VARCHAR(500),
    DOI VARCHAR(256),
)

SELECT * FROM ScieenceSchema.Publication;