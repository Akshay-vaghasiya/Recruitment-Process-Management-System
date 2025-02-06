using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCandidateStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "application_status",
                columns: table => new
                {
                    pk_application_status_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__applicat__1B80B3DA4A1806EA", x => x.pk_application_status_id);
                });

            migrationBuilder.CreateTable(
                name: "candidate",
                columns: table => new
                {
                    pk_candidate_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    resume_url = table.Column<string>(type: "text", nullable: true),
                    years_of_experience = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__candidat__D43BFB2A752C6A10", x => x.pk_candidate_id);
                });

            migrationBuilder.CreateTable(
                name: "document_status",
                columns: table => new
                {
                    pk_document_status_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__document__2BFB62BD36F8EBB1", x => x.pk_document_status_id);
                });

            migrationBuilder.CreateTable(
                name: "document_type",
                columns: table => new
                {
                    pk_document_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__document__123FA8468734685F", x => x.pk_document_type_id);
                });

            migrationBuilder.CreateTable(
                name: "interview_round",
                columns: table => new
                {
                    pk_interview_round_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__intervie__FF56A1D577728A50", x => x.pk_interview_round_id);
                });

            migrationBuilder.CreateTable(
                name: "interview_status",
                columns: table => new
                {
                    pk_interview_status_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__intervie__247A960E45B6F140", x => x.pk_interview_status_id);
                });

            migrationBuilder.CreateTable(
                name: "job_status",
                columns: table => new
                {
                    pk_job_status_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__job_stat__0C7DACDF736C5695", x => x.pk_job_status_id);
                });

            migrationBuilder.CreateTable(
                name: "report_type",
                columns: table => new
                {
                    pk_report_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__report_t__852F07E0D9CAB15F", x => x.pk_report_type_id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    pk_role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__roles__12E769BBED1D1DA1", x => x.pk_role_id);
                });

            migrationBuilder.CreateTable(
                name: "skill",
                columns: table => new
                {
                    pk_skill_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__skill__BE38287969434363", x => x.pk_skill_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    pk_user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    joining_date = table.Column<DateOnly>(type: "date", nullable: true),
                    leaving_date = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user__2F416313F3F7F9F4", x => x.pk_user_id);
                });

            migrationBuilder.CreateTable(
                name: "document",
                columns: table => new
                {
                    pk_document_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_candidate_id = table.Column<int>(type: "int", nullable: true),
                    fk_document_type_id = table.Column<int>(type: "int", nullable: true),
                    document_url = table.Column<string>(type: "text", nullable: true),
                    fk_status_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__document__0467181A1D43A66E", x => x.pk_document_id);
                    table.ForeignKey(
                        name: "FK__document__fk_can__5629CD9C",
                        column: x => x.fk_candidate_id,
                        principalTable: "candidate",
                        principalColumn: "pk_candidate_id");
                    table.ForeignKey(
                        name: "FK__document__fk_doc__571DF1D5",
                        column: x => x.fk_document_type_id,
                        principalTable: "document_type",
                        principalColumn: "pk_document_type_id");
                    table.ForeignKey(
                        name: "FK__document__fk_sta__5812160E",
                        column: x => x.fk_status_id,
                        principalTable: "document_status",
                        principalColumn: "pk_document_status_id");
                });

            migrationBuilder.CreateTable(
                name: "job_position",
                columns: table => new
                {
                    pk_job_position_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    fk_status_id = table.Column<int>(type: "int", nullable: true),
                    closure_reason = table.Column<string>(type: "text", nullable: true),
                    fk_selected_candidate_id = table.Column<int>(type: "int", nullable: true),
                    fk_reviewer_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__job_posi__839B68E8D3381D1A", x => x.pk_job_position_id);
                    table.ForeignKey(
                        name: "FK__job_posit__fk_st__267ABA7A",
                        column: x => x.fk_status_id,
                        principalTable: "job_status",
                        principalColumn: "pk_job_status_id");
                });

            migrationBuilder.CreateTable(
                name: "candidate_skill",
                columns: table => new
                {
                    pk_candidate_skill_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_candidate_id = table.Column<int>(type: "int", nullable: true),
                    fk_skill_id = table.Column<int>(type: "int", nullable: true),
                    years_of_experience = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__candidat__634633AC0E7AA1B1", x => x.pk_candidate_skill_id);
                    table.ForeignKey(
                        name: "FK__candidate__fk_ca__3F466844",
                        column: x => x.fk_candidate_id,
                        principalTable: "candidate",
                        principalColumn: "pk_candidate_id");
                    table.ForeignKey(
                        name: "FK__candidate__fk_sk__403A8C7D",
                        column: x => x.fk_skill_id,
                        principalTable: "skill",
                        principalColumn: "pk_skill_id");
                });

            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    pk_notification_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_user_id = table.Column<int>(type: "int", nullable: true),
                    message = table.Column<string>(type: "text", nullable: true),
                    is_read = table.Column<bool>(type: "bit", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__notifica__E2D15614C8AC6AD6", x => x.pk_notification_id);
                    table.ForeignKey(
                        name: "FK__notificat__fk_us__6D0D32F4",
                        column: x => x.fk_user_id,
                        principalTable: "user",
                        principalColumn: "pk_user_id");
                });

            migrationBuilder.CreateTable(
                name: "report",
                columns: table => new
                {
                    pk_report_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_report_type_id = table.Column<int>(type: "int", nullable: true),
                    fk_generated_by = table.Column<int>(type: "int", nullable: true),
                    report_data = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__report__28BD8B5C92A88C60", x => x.pk_report_id);
                    table.ForeignKey(
                        name: "FK__report__fk_gener__73BA3083",
                        column: x => x.fk_generated_by,
                        principalTable: "user",
                        principalColumn: "pk_user_id");
                    table.ForeignKey(
                        name: "FK__report__fk_repor__72C60C4A",
                        column: x => x.fk_report_type_id,
                        principalTable: "report_type",
                        principalColumn: "pk_report_type_id");
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    pk_user_role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_user_id = table.Column<int>(type: "int", nullable: true),
                    fk_role_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_rol__4BA152D8B279EAD4", x => x.pk_user_role_id);
                    table.ForeignKey(
                        name: "FK__user_role__fk_ro__619B8048",
                        column: x => x.fk_role_id,
                        principalTable: "roles",
                        principalColumn: "pk_role_id");
                    table.ForeignKey(
                        name: "FK__user_role__fk_us__60A75C0F",
                        column: x => x.fk_user_id,
                        principalTable: "user",
                        principalColumn: "pk_user_id");
                });

            migrationBuilder.CreateTable(
                name: "interview",
                columns: table => new
                {
                    pk_interview_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_candidate_id = table.Column<int>(type: "int", nullable: true),
                    fk_job_position_id = table.Column<int>(type: "int", nullable: true),
                    fk_interview_round_id = table.Column<int>(type: "int", nullable: true),
                    round_number = table.Column<int>(type: "int", nullable: true),
                    scheduled_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    fk_status_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__intervie__4EF41A8FC3728FA5", x => x.pk_interview_id);
                    table.ForeignKey(
                        name: "FK__interview__fk_ca__4BAC3F29",
                        column: x => x.fk_candidate_id,
                        principalTable: "candidate",
                        principalColumn: "pk_candidate_id");
                    table.ForeignKey(
                        name: "FK__interview__fk_in__4D94879B",
                        column: x => x.fk_interview_round_id,
                        principalTable: "interview_round",
                        principalColumn: "pk_interview_round_id");
                    table.ForeignKey(
                        name: "FK__interview__fk_jo__4CA06362",
                        column: x => x.fk_job_position_id,
                        principalTable: "job_position",
                        principalColumn: "pk_job_position_id");
                    table.ForeignKey(
                        name: "FK__interview__fk_st__4E88ABD4",
                        column: x => x.fk_status_id,
                        principalTable: "interview_status",
                        principalColumn: "pk_interview_status_id");
                });

            migrationBuilder.CreateTable(
                name: "job_applications",
                columns: table => new
                {
                    pk_job_application_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_job_position_id = table.Column<int>(type: "int", nullable: true),
                    fk_candidate_id = table.Column<int>(type: "int", nullable: true),
                    fk_status_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__job_appl__DB67DEC16367F743", x => x.pk_job_application_id);
                    table.ForeignKey(
                        name: "FK__job_appli__fk_ca__3B75D760",
                        column: x => x.fk_candidate_id,
                        principalTable: "candidate",
                        principalColumn: "pk_candidate_id");
                    table.ForeignKey(
                        name: "FK__job_appli__fk_jo__3A81B327",
                        column: x => x.fk_job_position_id,
                        principalTable: "job_position",
                        principalColumn: "pk_job_position_id");
                    table.ForeignKey(
                        name: "FK__job_appli__fk_st__3C69FB99",
                        column: x => x.fk_status_id,
                        principalTable: "application_status",
                        principalColumn: "pk_application_status_id");
                });

            migrationBuilder.CreateTable(
                name: "job_skill",
                columns: table => new
                {
                    pk_job_skill_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_job_position_id = table.Column<int>(type: "int", nullable: true),
                    fk_skill_id = table.Column<int>(type: "int", nullable: true),
                    is_required = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__job_skil__B60C9BF6D647915D", x => x.pk_job_skill_id);
                    table.ForeignKey(
                        name: "FK__job_skill__fk_jo__2D27B809",
                        column: x => x.fk_job_position_id,
                        principalTable: "job_position",
                        principalColumn: "pk_job_position_id");
                    table.ForeignKey(
                        name: "FK__job_skill__fk_sk__2E1BDC42",
                        column: x => x.fk_skill_id,
                        principalTable: "skill",
                        principalColumn: "pk_skill_id");
                });

            migrationBuilder.CreateTable(
                name: "resume_review",
                columns: table => new
                {
                    pk_resume_review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_candidate_id = table.Column<int>(type: "int", nullable: true),
                    fk_job_position_id = table.Column<int>(type: "int", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__resume_r__CA50FD1519F29FA9", x => x.pk_resume_review_id);
                    table.ForeignKey(
                        name: "FK__resume_re__fk_ca__4316F928",
                        column: x => x.fk_candidate_id,
                        principalTable: "candidate",
                        principalColumn: "pk_candidate_id");
                    table.ForeignKey(
                        name: "FK__resume_re__fk_jo__440B1D61",
                        column: x => x.fk_job_position_id,
                        principalTable: "job_position",
                        principalColumn: "pk_job_position_id");
                });

            migrationBuilder.CreateTable(
                name: "interview_feedback",
                columns: table => new
                {
                    pk_interview_feedback_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_interview_id = table.Column<int>(type: "int", nullable: true),
                    fk_interviewer_id = table.Column<int>(type: "int", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__intervie__4139B8F6915BB39C", x => x.pk_interview_feedback_id);
                    table.ForeignKey(
                        name: "FK__interview__fk_in__68487DD7",
                        column: x => x.fk_interview_id,
                        principalTable: "interview",
                        principalColumn: "pk_interview_id");
                    table.ForeignKey(
                        name: "FK__interview__fk_in__693CA210",
                        column: x => x.fk_interviewer_id,
                        principalTable: "user",
                        principalColumn: "pk_user_id");
                });

            migrationBuilder.CreateTable(
                name: "interview_panel",
                columns: table => new
                {
                    pk_interview_panel_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_interview_id = table.Column<int>(type: "int", nullable: true),
                    fk_interviewer_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__intervie__1295035212BE1CB7", x => x.pk_interview_panel_id);
                    table.ForeignKey(
                        name: "FK__interview__fk_in__6477ECF3",
                        column: x => x.fk_interview_id,
                        principalTable: "interview",
                        principalColumn: "pk_interview_id");
                    table.ForeignKey(
                        name: "FK__interview__fk_in__656C112C",
                        column: x => x.fk_interviewer_id,
                        principalTable: "user",
                        principalColumn: "pk_user_id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__candidat__AB6E61649995AF39",
                table: "candidate",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_candidate_skill_fk_candidate_id",
                table: "candidate_skill",
                column: "fk_candidate_id");

            migrationBuilder.CreateIndex(
                name: "IX_candidate_skill_fk_skill_id",
                table: "candidate_skill",
                column: "fk_skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_document_fk_candidate_id",
                table: "document",
                column: "fk_candidate_id");

            migrationBuilder.CreateIndex(
                name: "IX_document_fk_document_type_id",
                table: "document",
                column: "fk_document_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_document_fk_status_id",
                table: "document",
                column: "fk_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_interview_fk_candidate_id",
                table: "interview",
                column: "fk_candidate_id");

            migrationBuilder.CreateIndex(
                name: "IX_interview_fk_interview_round_id",
                table: "interview",
                column: "fk_interview_round_id");

            migrationBuilder.CreateIndex(
                name: "IX_interview_fk_job_position_id",
                table: "interview",
                column: "fk_job_position_id");

            migrationBuilder.CreateIndex(
                name: "IX_interview_fk_status_id",
                table: "interview",
                column: "fk_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_interview_feedback_fk_interview_id",
                table: "interview_feedback",
                column: "fk_interview_id");

            migrationBuilder.CreateIndex(
                name: "IX_interview_feedback_fk_interviewer_id",
                table: "interview_feedback",
                column: "fk_interviewer_id");

            migrationBuilder.CreateIndex(
                name: "IX_interview_panel_fk_interview_id",
                table: "interview_panel",
                column: "fk_interview_id");

            migrationBuilder.CreateIndex(
                name: "IX_interview_panel_fk_interviewer_id",
                table: "interview_panel",
                column: "fk_interviewer_id");

            migrationBuilder.CreateIndex(
                name: "IX_job_applications_fk_candidate_id",
                table: "job_applications",
                column: "fk_candidate_id");

            migrationBuilder.CreateIndex(
                name: "IX_job_applications_fk_job_position_id",
                table: "job_applications",
                column: "fk_job_position_id");

            migrationBuilder.CreateIndex(
                name: "IX_job_applications_fk_status_id",
                table: "job_applications",
                column: "fk_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_job_position_fk_status_id",
                table: "job_position",
                column: "fk_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_job_skill_fk_job_position_id",
                table: "job_skill",
                column: "fk_job_position_id");

            migrationBuilder.CreateIndex(
                name: "IX_job_skill_fk_skill_id",
                table: "job_skill",
                column: "fk_skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_notification_fk_user_id",
                table: "notification",
                column: "fk_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_report_fk_generated_by",
                table: "report",
                column: "fk_generated_by");

            migrationBuilder.CreateIndex(
                name: "IX_report_fk_report_type_id",
                table: "report",
                column: "fk_report_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_resume_review_fk_candidate_id",
                table: "resume_review",
                column: "fk_candidate_id");

            migrationBuilder.CreateIndex(
                name: "IX_resume_review_fk_job_position_id",
                table: "resume_review",
                column: "fk_job_position_id");

            migrationBuilder.CreateIndex(
                name: "UQ__user__AB6E6164410F3DC6",
                table: "user",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_fk_role_id",
                table: "user_role",
                column: "fk_role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_fk_user_id",
                table: "user_role",
                column: "fk_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "candidate_skill");

            migrationBuilder.DropTable(
                name: "document");

            migrationBuilder.DropTable(
                name: "interview_feedback");

            migrationBuilder.DropTable(
                name: "interview_panel");

            migrationBuilder.DropTable(
                name: "job_applications");

            migrationBuilder.DropTable(
                name: "job_skill");

            migrationBuilder.DropTable(
                name: "notification");

            migrationBuilder.DropTable(
                name: "report");

            migrationBuilder.DropTable(
                name: "resume_review");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "document_type");

            migrationBuilder.DropTable(
                name: "document_status");

            migrationBuilder.DropTable(
                name: "interview");

            migrationBuilder.DropTable(
                name: "application_status");

            migrationBuilder.DropTable(
                name: "skill");

            migrationBuilder.DropTable(
                name: "report_type");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "candidate");

            migrationBuilder.DropTable(
                name: "interview_round");

            migrationBuilder.DropTable(
                name: "job_position");

            migrationBuilder.DropTable(
                name: "interview_status");

            migrationBuilder.DropTable(
                name: "job_status");
        }
    }
}
