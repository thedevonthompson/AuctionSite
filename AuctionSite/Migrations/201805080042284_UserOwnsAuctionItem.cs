namespace AuctionSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserOwnsAuctionItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuctionItems", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AuctionItems", "User_Id");
            AddForeignKey("dbo.AuctionItems", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuctionItems", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AuctionItems", new[] { "User_Id" });
            DropColumn("dbo.AuctionItems", "User_Id");
        }
    }
}
