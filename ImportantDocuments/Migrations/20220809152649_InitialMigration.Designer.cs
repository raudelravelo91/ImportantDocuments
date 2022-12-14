// <auto-generated />
using ImportantDocuments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ImportantDocuments.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220809152649_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ImportantDocuments.Domain.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Documents");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LocationType = 0,
                            Name = "DocumentName1"
                        },
                        new
                        {
                            Id = 2,
                            Location = "C:/physicalPath",
                            LocationType = 1,
                            Name = "DocumentName2"
                        },
                        new
                        {
                            Id = 3,
                            Location = "www.google.ca",
                            LocationType = 2,
                            Name = "DocumentName3"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Some test description",
                            LocationType = 0,
                            Name = "DocumentName3"
                        });
                });

            modelBuilder.Entity("ImportantDocuments.Domain.DocumentTag", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.HasKey("TagId", "DocumentId");

                    b.HasIndex("DocumentId");

                    b.ToTable("DocumentTags");

                    b.HasData(
                        new
                        {
                            TagId = 1,
                            DocumentId = 1
                        });
                });

            modelBuilder.Entity("ImportantDocuments.Domain.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ImportantDocument"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Banking"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Tax"
                        },
                        new
                        {
                            Id = 4,
                            Name = "IdentityDocument"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Work"
                        });
                });

            modelBuilder.Entity("ImportantDocuments.Domain.DocumentTag", b =>
                {
                    b.HasOne("ImportantDocuments.Domain.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ImportantDocuments.Domain.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Tag");
                });
#pragma warning restore 612, 618
        }
    }
}
