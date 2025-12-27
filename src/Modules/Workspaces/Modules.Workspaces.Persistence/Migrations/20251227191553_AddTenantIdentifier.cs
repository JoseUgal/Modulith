using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Workspaces.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantIdentifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "workspaces",
                table: "workspaces",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_workspaces_tenant_id",
                schema: "workspaces",
                table: "workspaces",
                column: "tenant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_workspaces_tenant_id",
                schema: "workspaces",
                table: "workspaces");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "workspaces",
                table: "workspaces");
        }
    }
}
