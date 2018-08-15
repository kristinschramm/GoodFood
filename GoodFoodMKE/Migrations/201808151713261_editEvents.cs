namespace GoodFoodMKE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editEvents : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "HostFarm_Id", "dbo.Farms");
            DropForeignKey("dbo.Events", "HostMarket_Id", "dbo.Markets");
            DropIndex("dbo.Events", new[] { "HostFarm_Id" });
            DropIndex("dbo.Events", new[] { "HostMarket_Id" });
            AddColumn("dbo.Events", "DateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Farms", "Event_EventId", c => c.Int());
            AddColumn("dbo.Markets", "Event_EventId", c => c.Int());
            CreateIndex("dbo.Farms", "Event_EventId");
            CreateIndex("dbo.Markets", "Event_EventId");
            AddForeignKey("dbo.Farms", "Event_EventId", "dbo.Events", "EventId");
            AddForeignKey("dbo.Markets", "Event_EventId", "dbo.Events", "EventId");
            DropColumn("dbo.Events", "Recurring");
            DropColumn("dbo.Events", "RecurringType");
            DropColumn("dbo.Events", "HostFarm_Id");
            DropColumn("dbo.Events", "HostMarket_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "HostMarket_Id", c => c.Int());
            AddColumn("dbo.Events", "HostFarm_Id", c => c.Int());
            AddColumn("dbo.Events", "RecurringType", c => c.String());
            AddColumn("dbo.Events", "Recurring", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Markets", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.Farms", "Event_EventId", "dbo.Events");
            DropIndex("dbo.Markets", new[] { "Event_EventId" });
            DropIndex("dbo.Farms", new[] { "Event_EventId" });
            DropColumn("dbo.Markets", "Event_EventId");
            DropColumn("dbo.Farms", "Event_EventId");
            DropColumn("dbo.Events", "DateTime");
            CreateIndex("dbo.Events", "HostMarket_Id");
            CreateIndex("dbo.Events", "HostFarm_Id");
            AddForeignKey("dbo.Events", "HostMarket_Id", "dbo.Markets", "Id");
            AddForeignKey("dbo.Events", "HostFarm_Id", "dbo.Farms", "Id");
        }
    }
}
