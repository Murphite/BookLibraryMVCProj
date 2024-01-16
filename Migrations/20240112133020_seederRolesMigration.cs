using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrarySoln.Migrations
{
    /// <inheritdoc />
    public partial class seederRolesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp)
                                 VALUES('1', 'regular', 'REGULAR', '01/01/0001 00:00:00'),
                                       ('2', 'admin', 'ADMIN', '01/01/0001 00:00:00')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
