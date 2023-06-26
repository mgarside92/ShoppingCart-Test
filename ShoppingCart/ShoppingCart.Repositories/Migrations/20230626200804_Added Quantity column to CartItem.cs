﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddedQuantitycolumntoCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Cart");
        }
    }
}
