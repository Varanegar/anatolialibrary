namespace Anatoli.IdentityServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPushNotificationEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Channels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserDeviceTokens",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AppToken = c.String(),
                        Platform = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.UserChannels",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ChannelId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ChannelId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Channels", t => t.ChannelId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ChannelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDeviceTokens", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserDeviceTokens", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.UserChannels", "ChannelId", "dbo.Channels");
            DropForeignKey("dbo.UserChannels", "UserId", "dbo.Users");
            DropIndex("dbo.UserChannels", new[] { "ChannelId" });
            DropIndex("dbo.UserChannels", new[] { "UserId" });
            DropIndex("dbo.UserDeviceTokens", new[] { "ClientId" });
            DropIndex("dbo.UserDeviceTokens", new[] { "UserId" });
            DropTable("dbo.UserChannels");
            DropTable("dbo.UserDeviceTokens");
            DropTable("dbo.Channels");
        }
    }
}
