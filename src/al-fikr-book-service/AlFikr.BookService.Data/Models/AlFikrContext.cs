using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace AlFikr.BookService.Data.Models;

public partial class AlFikrContext : DbContext
{
    public AlFikrContext()
    {
    }

    public AlFikrContext(DbContextOptions<AlFikrContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Catalogue> Catalogues { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentFile> DocumentFiles { get; set; }

    public virtual DbSet<Ebook> Ebooks { get; set; }

    public virtual DbSet<Editor> Editors { get; set; }

    public virtual DbSet<SubTheme> SubThemes { get; set; }

    public virtual DbSet<Theme> Themes { get; set; }

    public virtual DbSet<TranslationGroup> TranslationGroups { get; set; }

    public virtual DbSet<VolumeGroup> VolumeGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Author");

            entity.HasIndex(e => e.IdEditor, "Author_Editor_Id_fk");

            entity.HasIndex(e => new { e.FirstName, e.LastName }, "unique_firstName_lastName").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Adress)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ArName)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Biography).HasColumnType("text");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Civility)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.FirstName)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IdEditor).HasColumnType("int(11)");
            entity.Property(e => e.LastName)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.OrcId)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Position)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.IdEditorNavigation).WithMany(p => p.Authors)
                .HasForeignKey(d => d.IdEditor)
                .HasConstraintName("Author_Editor_Id_fk");
        });

        modelBuilder.Entity<Catalogue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Catalogue");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.ArTitle).HasMaxLength(255);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IdOwner).HasColumnType("int(11)");
            entity.Property(e => e.OwnerName)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.OwnerType)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ShortTitle)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Chapter");

            entity.HasIndex(e => e.IdDocument, "IdDocument");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.ChapterNumber).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.EndPage).HasColumnType("int(11)");
            entity.Property(e => e.IdDocument).HasColumnType("int(11)");
            entity.Property(e => e.StartPage).HasColumnType("int(11)");
            entity.Property(e => e.StartPageOuv).HasColumnType("int(11)");
            entity.Property(e => e.State).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.Chapters)
                .HasForeignKey(d => d.IdDocument)
                .HasConstraintName("Chapter_ibfk_1");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Collection");

            entity.HasIndex(e => e.IdEditor, "fk_editor");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.ArTitle).HasMaxLength(255);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IdEditor).HasColumnType("int(11)");
            entity.Property(e => e.ShortTitle).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.IdEditorNavigation).WithMany(p => p.Collections)
                .HasForeignKey(d => d.IdEditor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_editor");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Document");

            entity.HasIndex(e => e.IdCollection, "IdCollection");

            entity.HasIndex(e => e.IdEditor, "IdEditor");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Abstract).HasMaxLength(255);
            entity.Property(e => e.AccessType).HasMaxLength(255);
            entity.Property(e => e.AccompanyingMaterials).HasMaxLength(255);
            entity.Property(e => e.AccompanyingMaterialsNb).HasMaxLength(255);
            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.BlankPages).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.CoverPage).HasMaxLength(255);
            entity.Property(e => e.DocumentType).HasMaxLength(255);
            entity.Property(e => e.DocumentVolumeRefs).HasColumnType("int(11)");
            entity.Property(e => e.Doi).HasMaxLength(255);
            entity.Property(e => e.FileDocument).HasMaxLength(255);
            entity.Property(e => e.FileFormat).HasMaxLength(255);
            entity.Property(e => e.Foreword).HasMaxLength(255);
            entity.Property(e => e.IdCollection).HasColumnType("int(11)");
            entity.Property(e => e.IdEditor).HasColumnType("int(11)");
            entity.Property(e => e.IdOriginal).HasColumnType("int(11)");
            entity.Property(e => e.Keywords).HasMaxLength(255);
            entity.Property(e => e.LanguagesVariants).HasMaxLength(255);
            entity.Property(e => e.MarcFile).HasMaxLength(255);
            entity.Property(e => e.MarcRecordNumber).HasMaxLength(255);
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.OriginalLanguage).HasMaxLength(255);
            entity.Property(e => e.OriginalTitle).HasMaxLength(255);
            entity.Property(e => e.PhysicalDescription).HasMaxLength(255);
            entity.Property(e => e.PublicationDate).HasMaxLength(255);
            entity.Property(e => e.State).HasMaxLength(255);
            entity.Property(e => e.Subtitle).HasMaxLength(255);
            entity.Property(e => e.TitlesVariants).HasMaxLength(255);
            entity.Property(e => e.Translator).HasMaxLength(255);
            entity.Property(e => e.Url).HasMaxLength(255);
            entity.Property(e => e.VolumeNb).HasMaxLength(255);

            entity.HasOne(d => d.IdCollectionNavigation).WithMany(p => p.Documents)
                .HasForeignKey(d => d.IdCollection)
                .HasConstraintName("Document_ibfk_2");

            entity.HasOne(d => d.IdEditorNavigation).WithMany(p => p.Documents)
                .HasForeignKey(d => d.IdEditor)
                .HasConstraintName("Document_ibfk_1");
        });

        modelBuilder.Entity<DocumentFile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.IdDocument, "IdDocument");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.AddedOn).HasColumnType("datetime");
            entity.Property(e => e.EndPage).HasColumnType("int(11)");
            entity.Property(e => e.FileDocument).HasMaxLength(255);
            entity.Property(e => e.FileFormat).HasMaxLength(255);
            entity.Property(e => e.IdDocument).HasColumnType("int(11)");
            entity.Property(e => e.Reference).HasMaxLength(255);
            entity.Property(e => e.StartPage).HasColumnType("int(11)");
            entity.Property(e => e.State).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.DocumentFiles)
                .HasForeignKey(d => d.IdDocument)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DocumentFiles_ibfk_1");
        });

        modelBuilder.Entity<Ebook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Ebook");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Category).HasMaxLength(255);
            entity.Property(e => e.EditionNum).HasMaxLength(255);
            entity.Property(e => e.EditionPlace).HasMaxLength(255);
            entity.Property(e => e.Genre).HasMaxLength(255);
            entity.Property(e => e.Isbn)
                .HasMaxLength(255)
                .HasColumnName("ISBN");
            entity.Property(e => e.NbPages).HasColumnType("int(11)");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Ebook)
                .HasForeignKey<Ebook>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Ebook_ibfk_1");
        });

        modelBuilder.Entity<Editor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Editor");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.About).HasColumnType("text");
            entity.Property(e => e.AddedOn).HasColumnType("datetime");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ArName).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Multiplyer).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.PhotoFileName).HasMaxLength(255);
            entity.Property(e => e.PostalCode).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.WebSite).HasMaxLength(255);
        });

        modelBuilder.Entity<SubTheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("SubTheme");

            entity.HasIndex(e => e.IdCollection, "IdCollection");

            entity.HasIndex(e => e.IdTheme, "IdTheme");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.ArTitle)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IdCollection).HasColumnType("int(11)");
            entity.Property(e => e.IdTheme).HasColumnType("int(11)");
            entity.Property(e => e.ShortTitle)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.IdCollectionNavigation).WithMany(p => p.SubThemes)
                .HasForeignKey(d => d.IdCollection)
                .HasConstraintName("SubTheme_ibfk_2");

            entity.HasOne(d => d.IdThemeNavigation).WithMany(p => p.SubThemes)
                .HasForeignKey(d => d.IdTheme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SubTheme_ibfk_1");
        });

        modelBuilder.Entity<Theme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Theme");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.ArTitle)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ShortTitle)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<TranslationGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("TranslationGroup");

            entity.HasIndex(e => e.IdDocument, "IdDocument").IsUnique();

            entity.HasIndex(e => e.IdGroupRefs, "fk_translationGroup");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdDocument).HasColumnType("int(11)");
            entity.Property(e => e.IdGroupRefs).HasColumnType("int(11)");

            entity.HasOne(d => d.IdDocumentNavigation).WithOne(p => p.TranslationGroup)
                .HasForeignKey<TranslationGroup>(d => d.IdDocument)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_document");

            entity.HasOne(d => d.IdGroupRefsNavigation).WithMany(p => p.InverseIdGroupRefsNavigation)
                .HasForeignKey(d => d.IdGroupRefs)
                .HasConstraintName("fk_translationGroup");
        });

        modelBuilder.Entity<VolumeGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("VolumeGroup");

            entity.HasIndex(e => e.IdDocument, "IdDocument");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdDocument).HasColumnType("int(11)");
            entity.Property(e => e.IdGroupRefs).HasColumnType("int(11)");
            entity.Property(e => e.NumVolume).HasColumnType("int(11)");

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.VolumeGroups)
                .HasForeignKey(d => d.IdDocument)
                .HasConstraintName("VolumeGroup_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
