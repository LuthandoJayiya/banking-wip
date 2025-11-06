using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MELLBankRestAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c540573e-9612-46e0-ac8a-ec2ebe13810f", null, "Customer", "CUSTOMER" },
                    { "e87ba540-d20d-4916-937f-0b4e1e6f2e76", null, "BankStaff", "BANKSTAFF" },
                    { "ea312f94-4564-4287-a7d9-98db3707be47", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "48a8aac2-0dee-409d-a813-a166389e87a1", 0, "67b7d524-2994-4fce-8efb-bd5c035d35b7", "ApplicationUser", "thuli.mthembu@gmail.com", true, "Thuli", "Mthembu", false, null, null, "THULIMTHEMBU", "AQAAAAEAACcQAAAAEH81rxTJnKjMA9gNN1Qxiejcep8hPHjZgsL5mu3cGc0F3/OzMNBcAlMNd8QyQ5VdmQ==", null, false, "7a45efcf-a313-4b03-a8ad-6509834b9ef6", false, "ThuliMthembu" },
                    { "5a33c336-a580-48ca-969b-fc8689b1ae86", 0, "4c84c1ff-876d-4cce-87f4-7e771875d48d", "ApplicationUser", "sizwe.dlamini@gmail.com", true, "Sizwe", "Dlamini", false, null, null, "SIZWEDLAMINI", "AQAAAAEAACcQAAAAEB20p4pXpzMT7SWWCnF0xMHou2wvrpZcs6PfuPpovlCt++TjoJaLHLhxiWQfeuJprQ==", null, false, "410c9a6e-c8cc-4a61-8fc5-d3a2ece75f11", false, "SizweDlamini" },
                    { "6d2d0381-fc05-4f14-98e3-a0ec9436b50f", 0, "a31f9dc8-b3f0-4960-8d79-3f2e35c688b0", "ApplicationUser", "thando.maseko@gmail.com", true, "Thando", "Maseko", false, null, null, "THANDOMASEKO", "AQAAAAEAACcQAAAAECl0vs3wkJdrUFvsi2ihX49PezdVYU0gfMLGQhTmp1NxalBRDYYeBfAEj5RwXHc9cw==", null, false, "82ac5d9d-acc0-48cd-9fc3-ea9ac9893ddb", false, "ThandoMaseko" },
                    { "74d6af27-4e2d-4f97-ae1a-821f7b37036d", 0, "78b3d0a5-2e37-48cb-88c4-f388b8b565a2", "ApplicationUser", "Sipho.Zwane@mellbank.com", true, "Sipho", "Zwane", false, null, null, "SIPHOADMIN", "AQAAAAEAACcQAAAAEIBDfGztU83AzKqXA1QcL0DSP6SugGQ1ZPQGKWs/TCrp+3BNZQFtxPIfXj4kWaAgNQ==", null, false, "600b941e-aea5-4277-a978-51d18045f0c2", false, "SiphoAdmin" },
                    { "78fb5460-40ae-4997-b229-cf12b1e38aa3", 0, "7da75726-2d80-43b0-8430-2637497103d8", "ApplicationUser", "siyabonga.nkosi@gmail.com", true, "Siyabonga", "Nkosi", false, null, null, "SIYABONGANKOSI", "AQAAAAEAACcQAAAAECJXFv13FJBAaPoF15RB11zYw0MUybUBwvUOP+E3U02TGOxPnHS0HDQoXrvigQl+xw==", null, false, "0b591a0e-143e-4555-a295-fe1c776874d0", false, "SiyabongaNkosi" },
                    { "797876a8-cf4c-4b79-a45b-f7b3d150ab4d", 0, "de5f41cd-fa49-46ae-bce7-c4808c360c5a", "ApplicationUser", "sibongile.khumalo@gmail.com", true, "Sibongile", "Khumalo", false, null, null, "SIBONGILEKHUMALO", "AQAAAAEAACcQAAAAEIUN0ZwWdxWBKTlYgN1WDjHRqlqt+RJNVN3Xlr55wXmTkJMY+OvPi1mQckG3C93Z6w==", null, false, "bfe17858-577d-4c8a-bbdb-f5f790ec8903", false, "SibongileKhumalo" },
                    { "b2a18891-ce77-4515-9647-f1fbcfd33e2c", 0, "f2b58f3e-f0ef-489f-ab40-0b8af4ccaa0a", "ApplicationUser", "nomsa.mkhize@gmail.com", true, "Nomsa", "Mkhize", false, null, null, "NOMSAMKHIZE", "AQAAAAEAACcQAAAAEKiNMssg+bO4FcqPS+1YWmSbNh+IO1xWpf3InQODox+dqW0wWRL1+ANooYpU580ONA==", null, false, "b6c4b92b-4acb-458a-b080-9b78faf14c3a", false, "NomsaMkhize" },
                    { "cb9e949e-e618-45c9-af3c-ece1c2a5b84a", 0, "a038bb72-7183-4bb7-a35a-ead1bc8d17f4", "ApplicationUser", "mandla.zulu@gmail.com", true, "Mandla", "Zulu", false, null, null, "MANDLAZULU", "AQAAAAEAACcQAAAAEPLO8xIxRwU8iBZCrUuwHEHrYLXuUGCmrbFRRIFL2Vf/gPVi8pj+OYCnSjp/8wcbew==", null, false, "7a8255af-a070-45af-9f23-83a4f4401646", false, "MandlaZulu" },
                    { "dc178b2a-dcb9-4671-b3bd-4da8801ba79b", 0, "75255313-eb90-4b4e-8774-9ce1e755cfdd", "ApplicationUser", "Sindi.Ntuli@mellbank.com", true, "Sindi", "Ntuli", false, null, null, "SINDIBANKSTAFF", "AQAAAAEAACcQAAAAEByU8MSY5izq7Uao6Fx6Kinnd88E6eXPx+qcHQQM3aEmHuLxffpdIfjyRsfwz56gHA==", null, false, "80f00b3e-dd34-4c78-9ef5-24a8e6f855f0", false, "SindiBankStaff" },
                    { "e22ac6a3-a09b-4371-97b7-bc8bcc2575e3", 0, "0caa3cef-5bb1-46d4-a9a6-e72b9088e361", "ApplicationUser", "mpho.ngwenya@gmail.com", true, "Mpho", "Khumalo", false, null, null, "MPHOKHUMALO", "AQAAAAEAACcQAAAAEJ/+xeZEXPw1T2VaIvS4Jeanjvysk5dRePWKviI6LCvo3Y9kDlivLizHBDXOhdHP7Q==", null, false, "ba0f40e5-ed81-4003-9858-55ccccf41c79", false, "MphoKhumalo" },
                    { "e95b9184-fd44-466b-886c-5fd1791d5c7b", 0, "c29c1965-7c74-449f-8518-0790edb09c06", "ApplicationUser", "Jabu.Nkosi@gmail.com", true, "Jabu", "Nkosi", false, null, null, "JABUNKOSI", "AQAAAAEAACcQAAAAECWGCJQl3wbZuazPDqiI/H/wxYD9VByHqfyB9OShPqnlHRHE7tr81yIbgg2wlbm99Q==", null, false, "91285407-b2fe-4757-b1fc-c37f50f14ffa", false, "JabuNkosi" },
                    { "e9f50875-d7e2-4195-942e-37d68479e80e", 0, "c33bf689-c5eb-4a0f-b017-bad310737a12", "ApplicationUser", "lebo.mokoena@gmail.com", true, "Lebo", "Mokoena", false, null, null, "LEBOMOKOENA", "AQAAAAEAACcQAAAAEK80i7NnNLbrExq1ktIliBi/Ay8ABXXbWvKsnxtn6+rLAsOkitKoO+4Sf7Fo5/W5Xg==", null, false, "d7a6905d-d8e8-4bf0-9f02-91440b9a2460", false, "LeboMokoena" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "c540573e-9612-46e0-ac8a-ec2ebe13810f", "48a8aac2-0dee-409d-a813-a166389e87a1" },
                    { "c540573e-9612-46e0-ac8a-ec2ebe13810f", "5a33c336-a580-48ca-969b-fc8689b1ae86" },
                    { "c540573e-9612-46e0-ac8a-ec2ebe13810f", "6d2d0381-fc05-4f14-98e3-a0ec9436b50f" },
                    { "ea312f94-4564-4287-a7d9-98db3707be47", "74d6af27-4e2d-4f97-ae1a-821f7b37036d" },
                    { "c540573e-9612-46e0-ac8a-ec2ebe13810f", "78fb5460-40ae-4997-b229-cf12b1e38aa3" },
                    { "c540573e-9612-46e0-ac8a-ec2ebe13810f", "797876a8-cf4c-4b79-a45b-f7b3d150ab4d" },
                    { "c540573e-9612-46e0-ac8a-ec2ebe13810f", "b2a18891-ce77-4515-9647-f1fbcfd33e2c" },
                    { "c540573e-9612-46e0-ac8a-ec2ebe13810f", "cb9e949e-e618-45c9-af3c-ece1c2a5b84a" },
                    { "e87ba540-d20d-4916-937f-0b4e1e6f2e76", "dc178b2a-dcb9-4671-b3bd-4da8801ba79b" },
                    { "c540573e-9612-46e0-ac8a-ec2ebe13810f", "e22ac6a3-a09b-4371-97b7-bc8bcc2575e3" },
                    { "c540573e-9612-46e0-ac8a-ec2ebe13810f", "e95b9184-fd44-466b-886c-5fd1791d5c7b" },
                    { "c540573e-9612-46e0-ac8a-ec2ebe13810f", "e9f50875-d7e2-4195-942e-37d68479e80e" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
