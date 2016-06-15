namespace SterreFenna.Business.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gallery",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Created = c.DateTime(nullable: false),
                        Published = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GalleryItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Location = c.String(),
                        Created = c.DateTime(nullable: false),
                        Gallery_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gallery", t => t.Gallery_Id)
                .Index(t => t.Gallery_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GalleryItem", "Gallery_Id", "dbo.Gallery");
            DropIndex("dbo.GalleryItem", new[] { "Gallery_Id" });
            DropTable("dbo.GalleryItem");
            DropTable("dbo.Gallery");
        }
    }
}
