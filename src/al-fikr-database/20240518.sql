CREATE TABLE DocumentTheme(
    Id INT auto_increment PRIMARY KEY ,
    IdDocument INT NOT NULL ,
    IdTheme INT NOT NULL,
    CONSTRAINT doc_fk FOREIGN KEY(IdDocument) REFERENCES Document(Id),
    CONSTRAINT th_fk FOREIGN KEY (IdTheme) REFERENCES Theme(Id),
    CONSTRAINT th_doc UNIQUE (IdDocument,IdTheme)
);

CREATE TABLE DocumentCatalogue(
    Id INT auto_increment PRIMARY KEY ,
    IdDocument INT NOT NULL ,
    IdCatalogue INT NOT NULL,
    CONSTRAINT docu_fk FOREIGN KEY(IdDocument) REFERENCES Document(Id),
    CONSTRAINT cat_fk FOREIGN KEY (IdCatalogue) REFERENCES Catalogue(Id),
    CONSTRAINT cat_doc UNIQUE (IdDocument,IdCatalogue)
);

CREATE TABLE DocumentAuthor(
    Id INT auto_increment PRIMARY KEY ,
    IdDocument INT NOT NULL ,
    IdAuthor INT NOT NULL,
    Role VARCHAR(255) NOT NULL,
    CONSTRAINT docum_fk FOREIGN KEY(IdDocument) REFERENCES Document(Id) ON DELETE CASCADE,
    CONSTRAINT auth_fk FOREIGN KEY (IdAuthor) REFERENCES Author(Id) ON DELETE CASCADE,
    CONSTRAINT auth_doc UNIQUE (IdDocument,IdAuthor)
)