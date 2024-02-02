﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebPersonal_API.Datos;

#nullable disable

namespace WebPersonal_API.Migrations
{
    [DbContext(typeof(PersonalDbContext))]
    partial class PersonalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WebPersonal_API.Modelos.CBarrio", b =>
                {
                    b.Property<string>("CodBarrio")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("char(3)")
                        .HasDefaultValue("")
                        .HasColumnName("cod_barrio")
                        .IsFixedLength();

                    b.Property<string>("CodMunici")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("char(2)")
                        .HasDefaultValue("")
                        .HasColumnName("cod_munici")
                        .IsFixedLength();

                    b.Property<string>("CodProvin")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("char(2)")
                        .HasDefaultValue("")
                        .HasColumnName("cod_provin")
                        .IsFixedLength();

                    b.Property<string>("NomBarrio")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasDefaultValue("")
                        .HasColumnName("nom_barrio");

                    b.HasKey("CodBarrio");

                    b.HasIndex("CodProvin", "CodMunici");

                    b.ToTable("c_barrio", null, t =>
                        {
                            t.HasComment("Barrios");
                        });
                });

            modelBuilder.Entity("WebPersonal_API.Modelos.CCatcar", b =>
                {
                    b.Property<string>("CodCatcar")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("char(2)")
                        .HasDefaultValue("")
                        .HasColumnName("cod_catcar")
                        .IsFixedLength();

                    b.Property<string>("NomCatcar")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasDefaultValue("")
                        .HasColumnName("nom_catcar");

                    b.HasKey("CodCatcar");

                    b.ToTable("c_catcar", null, t =>
                        {
                            t.HasTrigger("utr_c_catcar");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("WebPersonal_API.Modelos.CMunici", b =>
                {
                    b.Property<string>("CodProvin")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("char(2)")
                        .HasDefaultValue("")
                        .HasColumnName("cod_provin")
                        .IsFixedLength();

                    b.Property<string>("CodMunici")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("char(2)")
                        .HasDefaultValue("")
                        .HasColumnName("cod_munici")
                        .IsFixedLength();

                    b.Property<string>("NomMunici")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasDefaultValue("")
                        .HasColumnName("nom_munici");

                    b.HasKey("CodProvin", "CodMunici");

                    b.ToTable("c_munici", null, t =>
                        {
                            t.HasTrigger("utr_c_munici");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("WebPersonal_API.Modelos.CProvin", b =>
                {
                    b.Property<string>("CodProvin")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("char(2)")
                        .HasDefaultValue("")
                        .HasColumnName("cod_provin")
                        .IsFixedLength();

                    b.Property<string>("NomProvin")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasDefaultValue("")
                        .HasColumnName("nom_provin");

                    b.HasKey("CodProvin");

                    b.ToTable("c_provin", null, t =>
                        {
                            t.HasTrigger("utr_c_provin");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("WebPersonal_API.Modelos.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("WebPersonal_API.Modelos.UsuarioAplicacion", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Cod_ident")
                        .HasMaxLength(11)
                        .HasColumnType("char");

                    b.Property<string>("Cod_reeup")
                        .HasMaxLength(5)
                        .HasColumnType("char");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nombres")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebPersonal_API.Modelos.UsuarioAplicacion", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebPersonal_API.Modelos.UsuarioAplicacion", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebPersonal_API.Modelos.UsuarioAplicacion", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebPersonal_API.Modelos.UsuarioAplicacion", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebPersonal_API.Modelos.CBarrio", b =>
                {
                    b.HasOne("WebPersonal_API.Modelos.CProvin", "CodProvinNavigation")
                        .WithMany("CBarrios")
                        .HasForeignKey("CodProvin")
                        .IsRequired()
                        .HasConstraintName("FK_c_barrio_cod_provin");

                    b.HasOne("WebPersonal_API.Modelos.CMunici", "CMunici")
                        .WithMany("CBarrios")
                        .HasForeignKey("CodProvin", "CodMunici")
                        .IsRequired()
                        .HasConstraintName("FK_c_barrio_munici");

                    b.Navigation("CMunici");

                    b.Navigation("CodProvinNavigation");
                });

            modelBuilder.Entity("WebPersonal_API.Modelos.CMunici", b =>
                {
                    b.HasOne("WebPersonal_API.Modelos.CProvin", "CodProvinNavigation")
                        .WithMany("CMunicis")
                        .HasForeignKey("CodProvin")
                        .IsRequired()
                        .HasConstraintName("FK_c_munici_cod_provin");

                    b.Navigation("CodProvinNavigation");
                });

            modelBuilder.Entity("WebPersonal_API.Modelos.CMunici", b =>
                {
                    b.Navigation("CBarrios");
                });

            modelBuilder.Entity("WebPersonal_API.Modelos.CProvin", b =>
                {
                    b.Navigation("CBarrios");

                    b.Navigation("CMunicis");
                });
#pragma warning restore 612, 618
        }
    }
}
