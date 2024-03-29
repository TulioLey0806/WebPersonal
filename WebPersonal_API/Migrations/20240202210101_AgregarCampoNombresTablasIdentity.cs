﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPersonal_API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampoNombresTablasIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nombres",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombres",
                table: "AspNetUsers");
        }
    }
}
