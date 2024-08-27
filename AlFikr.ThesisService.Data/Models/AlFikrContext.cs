using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace AlFikr.ThesisService.Data.Models;

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

    public virtual DbSet<Documentauthor> Documentauthors { get; set; }

    public virtual DbSet<Documentcatalogue> Documentcatalogues { get; set; }

    public virtual DbSet<Documentfile> Documentfiles { get; set; }

    public virtual DbSet<Documenttheme> Documentthemes { get; set; }

    public virtual DbSet<Ebook> Ebooks { get; set; }

    public virtual DbSet<Editor> Editors { get; set; }

    public virtual DbSet<Individual> Individuals { get; set; }

    public virtual DbSet<Reviewer> Reviewers { get; set; }

    public virtual DbSet<Subtheme> Subthemes { get; set; }

    public virtual DbSet<Supervisor> Supervisors { get; set; }

    public virtual DbSet<Theme> Themes { get; set; }

    public virtual DbSet<Thesis> Theses { get; set; }

    public virtual DbSet<Thesisreviewer> Thesisreviewers { get; set; }

    public virtual DbSet<Thesissupervisor> Thesissupervisors { get; set; }

    public virtual DbSet<Translationgroup> Translationgroups { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Volumegroup> Volumegroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=al-fikr-servicetest;uid=root;password=8520", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("author");

            entity.HasIndex(e => e.IdEditor, "Author_Editor_Id_fk");

            entity.HasIndex(e => new { e.FirstName, e.LastName }, "unique_firstName_lastName").IsUnique();

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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Author_Editor_Id_fk");
        });

        modelBuilder.Entity<Catalogue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("catalogue");

            entity.Property(e => e.ArTitle).HasMaxLength(255);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
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

            entity.ToTable("chapter");

            entity.HasIndex(e => e.IdDocument, "Chapter_ibfk_1");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.State).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.Chapters)
                .HasForeignKey(d => d.IdDocument)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Chapter_ibfk_1");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("collection");

            entity.HasIndex(e => e.IdEditor, "fk_editor");

            entity.Property(e => e.ArTitle).HasMaxLength(255);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ShortTitle).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.IdEditorNavigation).WithMany(p => p.Collections)
                .HasForeignKey(d => d.IdEditor)
                .HasConstraintName("fk_editor");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("document");

            entity.HasIndex(e => e.IdCollection, "IdCollection");

            entity.HasIndex(e => e.IdEditor, "IdEditor");

            entity.Property(e => e.Abstract).HasMaxLength(255);
            entity.Property(e => e.AccessType).HasMaxLength(255);
            entity.Property(e => e.AccompanyingMaterials).HasMaxLength(255);
            entity.Property(e => e.AccompanyingMaterialsNb).HasMaxLength(255);
            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.BlankPages).HasDefaultValueSql("'0'");
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.CoverPage).HasMaxLength(255);
            entity.Property(e => e.DocumentType).HasMaxLength(255);
            entity.Property(e => e.Doi).HasMaxLength(255);
            entity.Property(e => e.Downloadable).HasDefaultValueSql("'0'");
            entity.Property(e => e.FileDocument).HasMaxLength(255);
            entity.Property(e => e.FileFormat).HasMaxLength(255);
            entity.Property(e => e.Foreword).HasMaxLength(255);
            entity.Property(e => e.IsMultiVolume).HasDefaultValueSql("'0'");
            entity.Property(e => e.IsTranslated).HasDefaultValueSql("'0'");
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
                .HasConstraintName("document_ibfk_2");

            entity.HasOne(d => d.IdEditorNavigation).WithMany(p => p.Documents)
                .HasForeignKey(d => d.IdEditor)
                .HasConstraintName("document_ibfk_1");
        });

        modelBuilder.Entity<Documentauthor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("documentauthor");

            entity.HasIndex(e => new { e.IdDocument, e.IdAuthor }, "auth_doc").IsUnique();

            entity.HasIndex(e => e.IdAuthor, "auth_fk");

            entity.Property(e => e.Role).HasMaxLength(255);

            entity.HasOne(d => d.IdAuthorNavigation).WithMany(p => p.Documentauthors)
                .HasForeignKey(d => d.IdAuthor)
                .HasConstraintName("auth_fk");

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.Documentauthors)
                .HasForeignKey(d => d.IdDocument)
                .HasConstraintName("docum_fk");
        });

        modelBuilder.Entity<Documentcatalogue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("documentcatalogue");

            entity.HasIndex(e => new { e.IdDocument, e.IdCatalogue }, "cat_doc").IsUnique();

            entity.HasIndex(e => e.IdCatalogue, "cat_fk");

            entity.HasOne(d => d.IdCatalogueNavigation).WithMany(p => p.Documentcatalogues)
                .HasForeignKey(d => d.IdCatalogue)
                .HasConstraintName("cat_fk");

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.Documentcatalogues)
                .HasForeignKey(d => d.IdDocument)
                .HasConstraintName("docu_fk");
        });

        modelBuilder.Entity<Documentfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("documentfiles");

            entity.HasIndex(e => e.IdDocument, "DocumentFiles_ibfk_1");

            entity.Property(e => e.AddedOn).HasColumnType("datetime");
            entity.Property(e => e.FileDocument).HasMaxLength(255);
            entity.Property(e => e.FileFormat).HasMaxLength(255);
            entity.Property(e => e.Reference).HasMaxLength(255);
            entity.Property(e => e.State).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.Documentfiles)
                .HasForeignKey(d => d.IdDocument)
                .HasConstraintName("DocumentFiles_ibfk_1");
        });

        modelBuilder.Entity<Documenttheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("documenttheme");

            entity.HasIndex(e => new { e.IdDocument, e.IdTheme }, "th_doc").IsUnique();

            entity.HasIndex(e => e.IdTheme, "th_fk");

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.Documentthemes)
                .HasForeignKey(d => d.IdDocument)
                .HasConstraintName("doc_fk");

            entity.HasOne(d => d.IdThemeNavigation).WithMany(p => p.Documentthemes)
                .HasForeignKey(d => d.IdTheme)
                .HasConstraintName("th_fk");
        });

        modelBuilder.Entity<Ebook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ebook");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Category).HasMaxLength(255);
            entity.Property(e => e.EditionNum).HasMaxLength(255);
            entity.Property(e => e.EditionPlace).HasMaxLength(255);
            entity.Property(e => e.Genre).HasMaxLength(255);
            entity.Property(e => e.Isbn)
                .HasMaxLength(255)
                .HasColumnName("ISBN");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Ebook)
                .HasForeignKey<Ebook>(d => d.Id)
                .HasConstraintName("Ebook_ibfk_1");
        });

        modelBuilder.Entity<Editor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("editor");

            entity.Property(e => e.About).HasColumnType("text");
            entity.Property(e => e.AddedOn).HasColumnType("datetime");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ArName).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.PhotoFileName).HasMaxLength(255);
            entity.Property(e => e.PostalCode).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.WebSite).HasMaxLength(255);
        });

        modelBuilder.Entity<Individual>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("individuals");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ArName).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Orcid)
                .HasMaxLength(255)
                .HasColumnName("ORCID");
            entity.Property(e => e.Organization).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.PhotoFileName).HasMaxLength(255);
            entity.Property(e => e.PostalCode).HasMaxLength(255);
            entity.Property(e => e.Profession).HasMaxLength(255);
        });

        modelBuilder.Entity<Reviewer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reviewer");

            entity.HasIndex(e => e.IdEditor, "IdEditor");

            entity.Property(e => e.AddedOn).HasColumnType("datetime");
            entity.Property(e => e.ReviewerArName)
                .HasMaxLength(255)
                .HasColumnName("reviewerArName")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ReviewerName)
                .HasMaxLength(255)
                .HasColumnName("reviewerName")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.IdEditorNavigation).WithMany(p => p.Reviewers)
                .HasForeignKey(d => d.IdEditor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reviewer_ibfk_1");
        });

        modelBuilder.Entity<Subtheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("subtheme");

            entity.HasIndex(e => e.IdTheme, "SubTheme_ibfk_1");

            entity.HasIndex(e => e.IdCollection, "SubTheme_ibfk_2");

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

            entity.HasOne(d => d.IdCollectionNavigation).WithMany(p => p.Subthemes)
                .HasForeignKey(d => d.IdCollection)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SubTheme_ibfk_2");

            entity.HasOne(d => d.IdThemeNavigation).WithMany(p => p.Subthemes)
                .HasForeignKey(d => d.IdTheme)
                .HasConstraintName("SubTheme_ibfk_1");
        });

        modelBuilder.Entity<Supervisor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("supervisor");

            entity.Property(e => e.AddedOn).HasColumnType("datetime");
            entity.Property(e => e.SupervisorArName)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.SupervisorName)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.SupervisorTitle)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Theme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("theme");

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

        modelBuilder.Entity<Thesis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("thesis");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DefenceDate).HasColumnType("datetime");
            entity.Property(e => e.Discipline).HasMaxLength(255);
            entity.Property(e => e.Institution).HasMaxLength(255);
            entity.Property(e => e.Reviewer)
                .HasMaxLength(255)
                .HasColumnName("reviewer");
            entity.Property(e => e.Speciality).HasMaxLength(255);
            entity.Property(e => e.Supervisor).HasMaxLength(255);
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
            entity.Property(e => e.University).HasMaxLength(255);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Thesis)
                .HasForeignKey<Thesis>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Thesis_ibfk_1");
        });

        modelBuilder.Entity<Thesisreviewer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("thesisreviewer");

            entity.HasIndex(e => e.ReviewerId, "fk_thesisReviwer_reviewer");

            entity.HasIndex(e => e.ThesisId, "fk_thesisReviwer_thesis");

            entity.Property(e => e.Role)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.Reviewer).WithMany(p => p.Thesisreviewers)
                .HasForeignKey(d => d.ReviewerId)
                .HasConstraintName("fk_thesisReviwer_reviewer");

            entity.HasOne(d => d.Thesis).WithMany(p => p.Thesisreviewers)
                .HasForeignKey(d => d.ThesisId)
                .HasConstraintName("fk_thesisReviwer_thesis");
        });

        modelBuilder.Entity<Thesissupervisor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("thesissupervisor");

            entity.HasIndex(e => e.SupervisorId, "fk_supervisor");

            entity.HasIndex(e => e.ThesisId, "fk_thesis");

            entity.Property(e => e.Role)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.Thesissupervisors)
                .HasForeignKey(d => d.SupervisorId)
                .HasConstraintName("fk_supervisor");

            entity.HasOne(d => d.Thesis).WithMany(p => p.Thesissupervisors)
                .HasForeignKey(d => d.ThesisId)
                .HasConstraintName("fk_thesis");
        });

        modelBuilder.Entity<Translationgroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("translationgroup");

            entity.HasIndex(e => e.IdDocument, "IdDocument").IsUnique();

            entity.HasIndex(e => e.IdGroupRefs, "fk_translationGroup");

            entity.HasOne(d => d.IdDocumentNavigation).WithOne(p => p.Translationgroup)
                .HasForeignKey<Translationgroup>(d => d.IdDocument)
                .HasConstraintName("fk_document");

            entity.HasOne(d => d.IdGroupRefsNavigation).WithMany(p => p.InverseIdGroupRefsNavigation)
                .HasForeignKey(d => d.IdGroupRefs)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_translationGroup");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.AccountType).HasMaxLength(255);
            entity.Property(e => e.AddedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Code).HasMaxLength(255);
            entity.Property(e => e.CodeExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.EmailConfirmationCode).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.RoleInAffiliation).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(255);
        });

        modelBuilder.Entity<Volumegroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("volumegroup");

            entity.HasIndex(e => e.IdDocument, "VolumeGroup_ibfk_1");

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.Volumegroups)
                .HasForeignKey(d => d.IdDocument)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("VolumeGroup_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
