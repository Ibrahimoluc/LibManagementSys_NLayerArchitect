namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_comment1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Content", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Content");
        }
    }
}
