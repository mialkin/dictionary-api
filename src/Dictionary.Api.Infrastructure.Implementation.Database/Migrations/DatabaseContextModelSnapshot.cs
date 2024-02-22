﻿// <auto-generated />
using System;
using Dictionary.Api.Infrastructure.Implementation.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dictionary.Api.Infrastructure.Implementation.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dictionary.Api.Domain.Entities.Word", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("GenderId")
                        .HasColumnType("integer")
                        .HasColumnName("gender_id");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer")
                        .HasColumnName("language_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Transcription")
                        .HasColumnType("text")
                        .HasColumnName("transcription");

                    b.Property<string>("Translation")
                        .HasColumnType("text")
                        .HasColumnName("translation");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_words");

                    b.HasIndex("LanguageId", "Name")
                        .HasDatabaseName("ix_words_language_id_name");

                    b.ToTable("words", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
