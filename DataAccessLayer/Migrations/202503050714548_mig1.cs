namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Alans", "KitapSayisi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Alans", "KitapSayisi", c => c.Int(nullable: false));
        }
    }
}
