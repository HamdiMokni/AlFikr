CREATE TABLE IF NOT EXISTS Editor
(
    Id            INTEGER AUTO_INCREMENT PRIMARY KEY,
    Name          VARCHAR(255),
    ArName        VARCHAR(255),
    WebSite       VARCHAR(255),
    Email         VARCHAR(255),
    Phone         VARCHAR(255),
    About         TEXT,
    City          VARCHAR(255),
    Country       VARCHAR(255),
    Address       VARCHAR(255),
    PostalCode    VARCHAR(255),
    Multiplyer    INTEGER,
    PhotoFileName VARCHAR(255),
    Status        VARCHAR(255),
    AddedOn       DATETIME
);

CREATE TABLE IF NOT EXISTS Collection
(
    Id          INTEGER AUTO_INCREMENT PRIMARY KEY,
    IdEditor    INTEGER      NOT NULL,
    Title       VARCHAR(255) NOT NULL,
    ArTitle     VARCHAR(255) NOT NULL,
    ShortTitle  VARCHAR(255) NOT NULL,
    Description TEXT,
    CONSTRAINT fk_editor
        FOREIGN KEY (IdEditor)
            REFERENCES Editor (Id)
);

CREATE TABLE IF NOT EXISTS Document
(
    Id                      INTEGER AUTO_INCREMENT PRIMARY KEY,
    IdEditor                INTEGER,
    IdCollection            INTEGER,
    Doi                     VARCHAR(255),
    MarcRecordNumber        VARCHAR(255),
    OriginalTitle           VARCHAR(255),
    TitlesVariants          VARCHAR(255),
    Subtitle                VARCHAR(255),
    Foreword                VARCHAR(255),
    Keywords                VARCHAR(255),
    FileDocument            VARCHAR(255),
    FileFormat              VARCHAR(255),
    CoverPage               VARCHAR(255),
    Url                     VARCHAR(255),
    DocumentType            VARCHAR(255),
    OriginalLanguage        VARCHAR(255),
    LanguagesVariants       VARCHAR(255),
    Translator              VARCHAR(255),
    AccessType              VARCHAR(255),
    Author                  VARCHAR(255),
    State                   VARCHAR(255),
    Price                   FLOAT,
    PublicationDate         VARCHAR(255),
    Country                 VARCHAR(255),
    PhysicalDescription     VARCHAR(255),
    AccompanyingMaterials   VARCHAR(255),
    AccompanyingMaterialsNb VARCHAR(255),
    VolumeNb                VARCHAR(255),
    Abstract                VARCHAR(255),
    Notes                   VARCHAR(255),
    BlankPages              INT,
    SellingPrice            FLOAT,
    DigitalPrice            FLOAT,
    Downloadable            BOOLEAN,
    MarcFile                VARCHAR(255),
    IsMultiVolume           BOOLEAN,
    DocumentVolumeRefs      INTEGER,
    IdOriginal              INTEGER,
    IsTranslated            BOOLEAN,
    FOREIGN KEY (IdEditor) REFERENCES Editor (Id),
    FOREIGN KEY (IdCollection) REFERENCES Collection (Id)
);

create table if not exists Thesis
(
    Id 			INTEGER primary key references Document (Id),
    Speciality 	varchar(255),
    Discipline 	VARCHAR(255),
    Institution VARCHAR(255),
    University 	VARCHAR(255),
    type 		VARCHAR(255),
    DefenceDate DATETIME,
    Supervisor VARCHAR(255),
    reviewer VARCHAR(255),
    NbPages INT,
    constraint Thesis_ibfk_1 FOREIGN KEY (Id) REFERENCES Document (Id)
);

create table if not exists Supervisor
( 
	Id INTEGER auto_increment primary key,
    SupervisorName NVARCHAR(255) null,
    SupervisorArName NVARCHAR(255) null,
    SupervisorTitle NVARCHAR(255) null,
    AddedOn DATETIME
);

create table if not exists thesisSupervisor (
	Id INTEGER auto_increment primary key,
    ThesisId INT,
    SupervisorId INT,
    Role NVARCHAR(255),
    CONSTRAINT fk_thesis FOREIGN KEY (ThesisId) REFERENCES Thesis(Id),
    CONSTRAINT fk_supervisor FOREIGN KEY (SupervisorId) REFERENCES Supervisor(Id)
);

create table if not exists reviewer( 
	Id INTEGER auto_increment primary key,
    reviewerName NVARCHAR(255) null,
    reviewerArName NVARCHAR(255) null,
    IdEditor INTEGER not null,
    AddedOn DATETIME,
    FOREIGN KEY (IdEditor) REFERENCES Editor (Id)
);

create table if not exists thesisReviewer(
	Id INTEGER auto_increment primary key,
    ThesisId INT,
    ReviewerId INT,
    Role NVARCHAR(255),
    CONSTRAINT fk_thesisReviewer_thesis FOREIGN KEY (ThesisId) REFERENCES Thesis(Id),
    CONSTRAINT fk_thesisReviewer_reviewer FOREIGN KEY (ReviewerId) REFERENCES reviewer(Id)
);

CREATE TABLE IF NOT EXISTS Author
(
    Id          INTEGER AUTO_INCREMENT PRIMARY KEY,
    OrcId       NVARCHAR(255) NULL,
    FirstName   NVARCHAR(255) NULL,
    LastName    NVARCHAR(255) NULL,
    ArName      NVARCHAR(255) NULL,
    DateOfBirth DATETIME      NULL,
    Civility    NVARCHAR(255) NULL,
    City        NVARCHAR(255) NULL,
    Adress      NVARCHAR(255) NULL,
    PostalCode  NVARCHAR(255) NULL,
    Country     NVARCHAR(255) NULL,
    Position    NVARCHAR(255) NULL,
    Email       NVARCHAR(255) NULL,
    Biography   TEXT          NULL,
    Phone       NVARCHAR(255) NULL,
    Photo       NVARCHAR(255) NULL,
    UNIQUE `unique_firstName_lastName` (FirstName, LastName)
);

CREATE TABLE IF NOT EXISTS TranslationGroup
(
    Id          INTEGER AUTO_INCREMENT PRIMARY KEY,
    IdDocument  INTEGER NOT NULL unique,
    IdGroupRefs INTEGER NULL,
    CONSTRAINT fk_document FOREIGN KEY (IdDocument) REFERENCES Document (Id),
    CONSTRAINT fk_translationGroup FOREIGN KEY (IdGroupRefs) REFERENCES TranslationGroup (Id)
);

CREATE TABLE IF NOT EXISTS Theme
(
    Id          INTEGER AUTO_INCREMENT PRIMARY KEY,
    Title       NVARCHAR(255) NULL,
    ArTitle     NVARCHAR(255) NULL,
    ShortTitle  NVARCHAR(255) NULL,
    Description NVARCHAR(255) NULL
);

CREATE TABLE IF NOT EXISTS SubTheme
(
    Id           INTEGER AUTO_INCREMENT PRIMARY KEY,
    IdTheme      INTEGER       NOT NULL,
    IdCollection INTEGER       NULL,
    Title        NVARCHAR(255) NULL,
    ArTitle      NVARCHAR(255) NULL,
    ShortTitle   NVARCHAR(255) NULL,
    Description  NVARCHAR(255) NULL,
    FOREIGN KEY (IdTheme) REFERENCES Theme (Id),
    FOREIGN KEY (IdCollection) REFERENCES Collection (Id)
);

CREATE TABLE IF NOT EXISTS Collection
(
    Id          INTEGER NOT NULL AUTO_INCREMENT,
    IdEditor    INTEGER NOT NULL,
    Title       TEXT,
    ArTitle     TEXT,
    ShortTitle  TEXT,
    Description TEXT,
    PRIMARY KEY (Id),
    FOREIGN KEY (IdEditor) REFERENCES Editor (Id)
);

CREATE TABLE IF NOT EXISTS Catalogue
(
    Id          INTEGER AUTO_INCREMENT NOT NULL,
    Title       VARCHAR(255)           NOT NULL,
    ArTitle     VARCHAR(255)           NOT NULL,
    IdOwner     INTEGER                NOT NULL,
    OwnerName   NVARCHAR(50),
    OwnerType   NVARCHAR(50),
    ShortTitle  NVARCHAR(255),
    Description NVARCHAR(255),
    PRIMARY KEY (Id)
);

CREATE TABLE IF NOT EXISTS Ebook
(
    Id           INTEGER PRIMARY KEY REFERENCES Document (Id),
    EditionNum   varchar(255),
    EditionPlace VARCHAR(255),
    ISBN         VARCHAR(255),
    Genre        VARCHAR(255),
    Category     VARCHAR(255),
    NbPages      INT
);

CREATE TABLE IF NOT EXISTS Chapter
(
    Id            INT AUTO_INCREMENT PRIMARY KEY,
    Title         VARCHAR(255),
    IdDocument    INT,
    ChapterNumber INT,
    StartPage     INT,
    StartPageOuv  INT,
    EndPage       INT,
    Description   TEXT,
    State         VARCHAR(255),
    FOREIGN KEY (IdDocument) REFERENCES Document (Id)
);

CREATE TABLE IF NOT EXISTS VolumeGroup
(
    Id          INT AUTO_INCREMENT PRIMARY KEY,
    IdDocument  INT REFERENCES Document (Id),
    NumVolume   INT,
    IdGroupRefs INT REFERENCES VolumeGroup (Id)
);

CREATE TABLE IF NOT EXISTS DocumentFiles
(
    Id           INT AUTO_INCREMENT PRIMARY KEY,
    IdDocument   INT          NOT NULL,
    Reference    VARCHAR(255) NOT NULL,
    Title        VARCHAR(255) NOT NULL,
    FileDocument VARCHAR(255),
    FileFormat   VARCHAR(255),
    StartPage    INT          NOT NULL,
    EndPage      INT          NOT NULL,
    State        VARCHAR(255) NOT NULL,
    AddedOn      DATETIME,
    FOREIGN KEY (IdDocument) REFERENCES Document (Id)
);

CREATE TABLE IF NOT EXISTS Users 
(
    Id                      INTEGER AUTO_INCREMENT PRIMARY KEY,
    IdAffiliation 			INT NOT NULL,
    RoleInAffiliation 		VARCHAR(255) NOT NULL,
    AccountType 			VARCHAR(255) NOT NULL,
    Code 					VARCHAR(255) NOT NULL,
    Email 					VARCHAR(255) NOT NULL,
    EmailConfirmed 			BOOLEAN NOT NULL,
    EmailConfirmationCode 	VARCHAR(255) NOT NULL,
    CodeExpirationDate 		DATETIME NOT NULL,
    Password 				VARCHAR(255) NOT NULL,
    Status 					VARCHAR(255) NOT NULL,
    AddedOn 				DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS Individuals
(
    Id                  	INTEGER PRIMARY key references Users (id) ,
    FirstName               VARCHAR(255) NOT NULL,
    LastName                VARCHAR(255) NOT NULL,
    ArName                  VARCHAR(255),
    ORCID                   VARCHAR(255),
    Gender                  VARCHAR(255),
    DateOfBirth             DATETIME,
    Phone                   VARCHAR(255),
    Country                 VARCHAR(255),
    City                    VARCHAR(255),
    Address                 VARCHAR(255),
    PostalCode              VARCHAR(255),
    PhotoFileName           VARCHAR(255),
    Profession              VARCHAR(255),
    Organization            VARCHAR(255)
   );