namespace AuctionSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuctionItemEndTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuctionItems", "EndDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuctionItems", "EndDateTime");
        }
    }
}
