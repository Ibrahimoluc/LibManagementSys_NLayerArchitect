namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_Add_KitapStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kitaps", "KitapStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kitaps", "KitapStatus");
        }
    }
}
