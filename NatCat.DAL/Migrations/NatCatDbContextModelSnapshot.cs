﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NatCat.DAL;

#nullable disable

namespace NatCat.DAL.Migrations
{
    [DbContext(typeof(NatCatDbContext))]
    partial class NatCatDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApplicationUserBookClub", b =>
                {
                    b.Property<string>("ApplicationUsersId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("BookClubsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ApplicationUsersId", "BookClubsId");

                    b.HasIndex("BookClubsId");

                    b.ToTable("ApplicationUserBookClub");
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes", (string)null);
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.Key", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Algorithm")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DataProtected")
                        .HasColumnType("bit");

                    b.Property<bool>("IsX509Certificate")
                        .HasColumnType("bit");

                    b.Property<string>("Use")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Use");

                    b.ToTable("Keys", (string)null);
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Key");

                    b.HasIndex("ConsumedTime");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.HasIndex("SubjectId", "SessionId", "Type");

                    b.ToTable("PersistedGrants", (string)null);
                });

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

            modelBuilder.Entity("NatCat.DAL.Entity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

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

                    b.Property<string>("ProfileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("NatCat.DAL.Entity.AwardedBadge", b =>
                {
                    b.Property<Guid>("BadgeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateTimeAwarded")
                        .HasColumnType("datetime2");

                    b.HasKey("BadgeId", "ApplicationUserId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("AwardedBadge");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.Badge", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Badge");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.BookClub", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("AverageRating")
                        .HasColumnType("real");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookClubs");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.BookClubJoinRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("BookClubId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateRequested")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RequestingUserProfileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("BookClubId");

                    b.ToTable("BookClubJoinRequests");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.KeyWord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Difficulty")
                        .HasColumnType("int");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.ToTable("KeyWords");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DatePublished")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RatingStars")
                        .HasColumnType("int");

                    b.Property<string>("ReviewText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("StoryId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.Story", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AllowPublicToJoinStory")
                        .HasColumnType("bit");

                    b.Property<string>("AuthorApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("BookClubId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CurrentStoryRound")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCompleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DatePublished")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsBeingWritten")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisibleOnLibrary")
                        .HasColumnType("bit");

                    b.Property<int>("MaxCharLengthPerStoryPart")
                        .HasColumnType("int");

                    b.Property<int>("MaxUsers")
                        .HasColumnType("int");

                    b.Property<int>("MinCharLengthPerStoryPart")
                        .HasColumnType("int");

                    b.Property<Guid>("StoryTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Synopsis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalStoryRounds")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorApplicationUserId");

                    b.HasIndex("BookClubId");

                    b.HasIndex("GenreId");

                    b.HasIndex("StoryTypeId");

                    b.ToTable("Stories");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.StoryJoinRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateRequested")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RequestingUserProfileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("StoryId");

                    b.ToTable("StoryJoinRequests");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.StoryPart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateSubmitted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeadlineTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvisibleTextFromPrevious")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFinalStoryPart")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRhymingRequired")
                        .HasColumnType("bit");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("RhymingSet")
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("RhymingWords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VisibleTextFromPrevious")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WordToRhymeWith")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("StoryId");

                    b.ToTable("StoryParts");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.StoryPartKeyWord", b =>
                {
                    b.Property<Guid>("StoryPartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("KeyWordId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StoryPartId", "KeyWordId");

                    b.HasIndex("KeyWordId");

                    b.ToTable("StoryPartKeyWords");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.StoryType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRhymingRequired")
                        .HasColumnType("bit");

                    b.Property<int>("MaxCharLengthPerStoryPart")
                        .HasColumnType("int");

                    b.Property<int>("MinCharLengthPerStoryPart")
                        .HasColumnType("int");

                    b.Property<string>("RhymingPattern")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RuleDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StoryTypes");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.StoryUser", b =>
                {
                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("StoryId", "ApplicationUserId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("StoryUsers");
                });

            modelBuilder.Entity("ApplicationUserBookClub", b =>
                {
                    b.HasOne("NatCat.DAL.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("ApplicationUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NatCat.DAL.Entity.BookClub", null)
                        .WithMany()
                        .HasForeignKey("BookClubsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.HasOne("NatCat.DAL.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("NatCat.DAL.Entity.ApplicationUser", null)
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

                    b.HasOne("NatCat.DAL.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("NatCat.DAL.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NatCat.DAL.Entity.AwardedBadge", b =>
                {
                    b.HasOne("NatCat.DAL.Entity.ApplicationUser", "ApplicationUser")
                        .WithMany("AwardedBadges")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NatCat.DAL.Entity.Badge", "Badge")
                        .WithMany("AwardedBadges")
                        .HasForeignKey("BadgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Badge");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.BookClubJoinRequest", b =>
                {
                    b.HasOne("NatCat.DAL.Entity.ApplicationUser", "ApplicationUser")
                        .WithMany("BookClubJoinRequests")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("NatCat.DAL.Entity.BookClub", "BookClub")
                        .WithMany("BookClubJoinRequests")
                        .HasForeignKey("BookClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("BookClub");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.KeyWord", b =>
                {
                    b.HasOne("NatCat.DAL.Entity.Genre", "Genre")
                        .WithMany("KeyWords")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.Review", b =>
                {
                    b.HasOne("NatCat.DAL.Entity.ApplicationUser", "ApplicationUser")
                        .WithMany("Reviews")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("NatCat.DAL.Entity.Story", "Story")
                        .WithMany("Reviews")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Story");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.Story", b =>
                {
                    b.HasOne("NatCat.DAL.Entity.ApplicationUser", "AuthorApplicationUser")
                        .WithMany("Stories")
                        .HasForeignKey("AuthorApplicationUserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("NatCat.DAL.Entity.BookClub", "BookClub")
                        .WithMany("Stories")
                        .HasForeignKey("BookClubId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NatCat.DAL.Entity.Genre", "Genre")
                        .WithMany("Stories")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NatCat.DAL.Entity.StoryType", "StoryType")
                        .WithMany("Stories")
                        .HasForeignKey("StoryTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AuthorApplicationUser");

                    b.Navigation("BookClub");

                    b.Navigation("Genre");

                    b.Navigation("StoryType");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.StoryJoinRequest", b =>
                {
                    b.HasOne("NatCat.DAL.Entity.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("NatCat.DAL.Entity.Story", "Story")
                        .WithMany()
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Story");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.StoryPart", b =>
                {
                    b.HasOne("NatCat.DAL.Entity.ApplicationUser", "ApplicationUser")
                        .WithMany("StoryParts")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("NatCat.DAL.Entity.Story", "Story")
                        .WithMany("StoryParts")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Story");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.StoryPartKeyWord", b =>
                {
                    b.HasOne("NatCat.DAL.Entity.KeyWord", "KeyWord")
                        .WithMany("StoryPartKeyWords")
                        .HasForeignKey("KeyWordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NatCat.DAL.Entity.StoryPart", "StoryPart")
                        .WithMany("StoryPartKeyWords")
                        .HasForeignKey("StoryPartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KeyWord");

                    b.Navigation("StoryPart");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.StoryUser", b =>
                {
                    b.HasOne("NatCat.DAL.Entity.ApplicationUser", "ApplicationUser")
                        .WithMany("StoryUsers")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NatCat.DAL.Entity.Story", "Story")
                        .WithMany("StoryUsers")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Story");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.ApplicationUser", b =>
                {
                    b.Navigation("AwardedBadges");

                    b.Navigation("BookClubJoinRequests");

                    b.Navigation("Reviews");

                    b.Navigation("Stories");

                    b.Navigation("StoryParts");

                    b.Navigation("StoryUsers");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.Badge", b =>
                {
                    b.Navigation("AwardedBadges");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.BookClub", b =>
                {
                    b.Navigation("BookClubJoinRequests");

                    b.Navigation("Stories");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.Genre", b =>
                {
                    b.Navigation("KeyWords");

                    b.Navigation("Stories");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.KeyWord", b =>
                {
                    b.Navigation("StoryPartKeyWords");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.Story", b =>
                {
                    b.Navigation("Reviews");

                    b.Navigation("StoryParts");

                    b.Navigation("StoryUsers");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.StoryPart", b =>
                {
                    b.Navigation("StoryPartKeyWords");
                });

            modelBuilder.Entity("NatCat.DAL.Entity.StoryType", b =>
                {
                    b.Navigation("Stories");
                });
#pragma warning restore 612, 618
        }
    }
}
