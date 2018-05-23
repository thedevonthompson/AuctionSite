namespace AuctionSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageBlobStorage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemImages", "Path", c => c.String());
            DropColumn("dbo.ItemImages", "Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemImages", "Data", c => c.Binary());
            DropColumn("dbo.ItemImages", "Path");
        }
    }
}
