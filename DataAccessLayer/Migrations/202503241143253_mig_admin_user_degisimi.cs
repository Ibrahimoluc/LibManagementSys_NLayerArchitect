namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_admin_user_degisimi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserPassword", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "UserRole", c => c.String(maxLength: 1));
            AlterColumn("dbo.Users", "UserName", c => c.String(maxLength: 50));
            DropColumn("dbo.Users", "Password");
            DropTable("dbo.Admins");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        AdminUserName = c.String(maxLength: 50),
                        AdminPassword = c.String(maxLength: 50),
                        AdminRole = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.AdminId);
            
            AddColumn("dbo.Users", "Password", c => c.String(maxLength: 1024));
            AlterColumn("dbo.Users", "UserName", c => c.String(maxLength: 1024));
            DropColumn("dbo.Users", "UserRole");
            DropColumn("dbo.Users", "UserPassword");
        }
    }
}
