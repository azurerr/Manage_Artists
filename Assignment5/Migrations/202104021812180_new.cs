namespace Assignment5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Artist", "Album_Id", "dbo.Album");
            DropIndex("dbo.Artist", new[] { "Album_Id" });
            CreateTable(
                "dbo.ArtistAlbums",
                c => new
                    {
                        Artist_Id = c.Int(nullable: false),
                        Album_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Artist_Id, t.Album_Id })
                .ForeignKey("dbo.Artist", t => t.Artist_Id, cascadeDelete: true)
                .ForeignKey("dbo.Album", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.Artist_Id)
                .Index(t => t.Album_Id);
            
            DropColumn("dbo.Artist", "Album_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Artist", "Album_Id", c => c.Int());
            DropForeignKey("dbo.ArtistAlbums", "Album_Id", "dbo.Album");
            DropForeignKey("dbo.ArtistAlbums", "Artist_Id", "dbo.Artist");
            DropIndex("dbo.ArtistAlbums", new[] { "Album_Id" });
            DropIndex("dbo.ArtistAlbums", new[] { "Artist_Id" });
            DropTable("dbo.ArtistAlbums");
            CreateIndex("dbo.Artist", "Album_Id");
            AddForeignKey("dbo.Artist", "Album_Id", "dbo.Album", "Id");
        }
    }
}
