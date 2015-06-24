namespace OctoBotSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGlobalConfig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GlobalConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrenyChar = c.String(maxLength: 1, fixedLength: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.ItemDefinitions", "IrcRenderChar", c => c.String(maxLength: 1, fixedLength: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ItemDefinitions", "IrcRenderChar", c => c.String(maxLength: 6, fixedLength: true));
            DropTable("dbo.GlobalConfigs");
        }
    }
}
