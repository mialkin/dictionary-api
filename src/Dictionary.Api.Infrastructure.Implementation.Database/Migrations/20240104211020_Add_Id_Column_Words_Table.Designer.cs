﻿// <auto-generated />
using System;
using Dictionary.Api.Infrastructure.Implementation.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dictionary.Api.Infrastructure.Implementation.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240104211020_Add_Id_Column_Words_Table")]
    partial class Add_Id_Column_Words_Table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dictionary.Api.Domain.Entities.Word", b =>
                {
                    b.Property<int>("LanguageId")
                        .HasColumnType("integer")
                        .HasColumnName("language_id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Transcription")
                        .HasColumnType("text")
                        .HasColumnName("transcription");

                    b.Property<string>("Translation")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("translation");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("LanguageId", "Name")
                        .HasName("pk_words");

                    b.ToTable("words", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
