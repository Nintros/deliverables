namespace Deliverables.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserCompanyId = c.Int(),
                        FirstName = c.String(maxLength: 256),
                        LastName = c.String(maxLength: 256),
                        Title = c.String(maxLength: 256),
                        Notes = c.String(),
                        PasswordChanged = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        SendNotificationsByEmail = c.Boolean(nullable: false),
                        IsFirmAdmin = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Deliverable",
                c => new
                    {
                        DeliverableID = c.Int(nullable: false, identity: true),
                        TypeId = c.Int(nullable: false),
                        Desciption = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DeliverableID);
            
            CreateTable(
                "dbo.TeamMember",
                c => new
                    {
                        TeamMemberId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDay = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Deliverable_DeliverableID = c.Int(),
                    })
                .PrimaryKey(t => t.TeamMemberId)
                .ForeignKey("dbo.Deliverable", t => t.Deliverable_DeliverableID)
                .Index(t => t.Deliverable_DeliverableID);
            
            CreateTable(
                "dbo.TechnicalSkill",
                c => new
                    {
                        TechnicalSkillId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LevelId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TechnicalSkillId);
            
            CreateTable(
                "dbo.TechnicalSkillDeliverable",
                c => new
                    {
                        TechnicalSkill_TechnicalSkillId = c.Int(nullable: false),
                        Deliverable_DeliverableID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TechnicalSkill_TechnicalSkillId, t.Deliverable_DeliverableID })
                .ForeignKey("dbo.TechnicalSkill", t => t.TechnicalSkill_TechnicalSkillId)
                .ForeignKey("dbo.Deliverable", t => t.Deliverable_DeliverableID)
                .Index(t => t.TechnicalSkill_TechnicalSkillId)
                .Index(t => t.Deliverable_DeliverableID);
            
            CreateTable(
                "dbo.TechnicalSkillTeamMember",
                c => new
                    {
                        TechnicalSkill_TechnicalSkillId = c.Int(nullable: false),
                        TeamMember_TeamMemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TechnicalSkill_TechnicalSkillId, t.TeamMember_TeamMemberId })
                .ForeignKey("dbo.TechnicalSkill", t => t.TechnicalSkill_TechnicalSkillId)
                .ForeignKey("dbo.TeamMember", t => t.TeamMember_TeamMemberId)
                .Index(t => t.TechnicalSkill_TechnicalSkillId)
                .Index(t => t.TeamMember_TeamMemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamMember", "Deliverable_DeliverableID", "dbo.Deliverable");
            DropForeignKey("dbo.TechnicalSkillTeamMember", "TeamMember_TeamMemberId", "dbo.TeamMember");
            DropForeignKey("dbo.TechnicalSkillTeamMember", "TechnicalSkill_TechnicalSkillId", "dbo.TechnicalSkill");
            DropForeignKey("dbo.TechnicalSkillDeliverable", "Deliverable_DeliverableID", "dbo.Deliverable");
            DropForeignKey("dbo.TechnicalSkillDeliverable", "TechnicalSkill_TechnicalSkillId", "dbo.TechnicalSkill");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.TechnicalSkillTeamMember", new[] { "TeamMember_TeamMemberId" });
            DropIndex("dbo.TechnicalSkillTeamMember", new[] { "TechnicalSkill_TechnicalSkillId" });
            DropIndex("dbo.TechnicalSkillDeliverable", new[] { "Deliverable_DeliverableID" });
            DropIndex("dbo.TechnicalSkillDeliverable", new[] { "TechnicalSkill_TechnicalSkillId" });
            DropIndex("dbo.TeamMember", new[] { "Deliverable_DeliverableID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.TechnicalSkillTeamMember");
            DropTable("dbo.TechnicalSkillDeliverable");
            DropTable("dbo.TechnicalSkill");
            DropTable("dbo.TeamMember");
            DropTable("dbo.Deliverable");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
