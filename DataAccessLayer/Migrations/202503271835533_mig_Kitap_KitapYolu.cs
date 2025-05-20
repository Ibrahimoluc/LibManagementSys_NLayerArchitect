namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_Kitap_KitapYolu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kitaps", "KitapYolu", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kitaps", "KitapYolu");
        }
    }
}
