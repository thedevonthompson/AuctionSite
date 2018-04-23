namespace AuctionSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuctionItemImagesAndCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.ItemImages",
                c => new
                    {
                        ItemImageID = c.Int(nullable: false, identity: true),
                        Data = c.Binary(),
                        AuctionItem_AuctionItemID = c.Int(),
                    })
                .PrimaryKey(t => t.ItemImageID)
                .ForeignKey("dbo.AuctionItems", t => t.AuctionItem_AuctionItemID)
                .Index(t => t.AuctionItem_AuctionItemID);
            
            AddColumn("dbo.AuctionItems", "Category_CategoryID", c => c.Int());
            CreateIndex("dbo.AuctionItems", "Category_CategoryID");
            AddForeignKey("dbo.AuctionItems", "Category_CategoryID", "dbo.Categories", "CategoryID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemImages", "AuctionItem_AuctionItemID", "dbo.AuctionItems");
            DropForeignKey("dbo.AuctionItems", "Category_CategoryID", "dbo.Categories");
            DropIndex("dbo.ItemImages", new[] { "AuctionItem_AuctionItemID" });
            DropIndex("dbo.AuctionItems", new[] { "Category_CategoryID" });
            DropColumn("dbo.AuctionItems", "Category_CategoryID");
            DropTable("dbo.ItemImages");
            DropTable("dbo.Categories");
        }
    }
}
