namespace AuctionSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuctionItemsAndBids : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuctionItems",
                c => new
                    {
                        AuctionItemID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        MinPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AuctionItemID);
            
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        ApplicationUserID = c.String(nullable: false, maxLength: 128),
                        AuctionItemID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ApplicationUserID, t.AuctionItemID })
                .ForeignKey("dbo.AuctionItems", t => t.AuctionItemID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID, cascadeDelete: true)
                .Index(t => t.ApplicationUserID)
                .Index(t => t.AuctionItemID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bids", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bids", "AuctionItemID", "dbo.AuctionItems");
            DropIndex("dbo.Bids", new[] { "AuctionItemID" });
            DropIndex("dbo.Bids", new[] { "ApplicationUserID" });
            DropTable("dbo.Bids");
            DropTable("dbo.AuctionItems");
        }
    }
}
