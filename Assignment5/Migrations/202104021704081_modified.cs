namespace Assignment5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modified : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtistAlbums", "Artist_Id", "dbo.Artist");
            DropForeignKey("dbo.ArtistAlbums", "Album_Id", "dbo.Album");
            DropIndex("dbo.ArtistAlbums", new[] { "Artist_Id" });
            DropIndex("dbo.ArtistAlbums", new[] { "Album_Id" });
            AddColumn("dbo.Artist", "AlbumsNum", c => c.Int(nullable: false));
            AddColumn("dbo.Artist", "Album_Id", c => c.Int());
            CreateIndex("dbo.Artist", "Album_Id");
            AddForeignKey("dbo.Artist", "Album_Id", "dbo.Album", "Id");
            DropTable("dbo.ArtistAlbums");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ArtistAlbums",
                c => new
                    {
                        Artist_Id = c.Int(nullable: false),
                        Album_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Artist_Id, t.Album_Id });
            
            DropForeignKey("dbo.Artist", "Album_Id", "dbo.Album");
            DropIndex("dbo.Artist", new[] { "Album_Id" });
            DropColumn("dbo.Artist", "Album_Id");
            DropColumn("dbo.Artist", "AlbumsNum");
            CreateIndex("dbo.ArtistAlbums", "Album_Id");
            CreateIndex("dbo.ArtistAlbums", "Artist_Id");
            AddForeignKey("dbo.ArtistAlbums", "Album_Id", "dbo.Album", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ArtistAlbums", "Artist_Id", "dbo.Artist", "Id", cascadeDelete: true);
        }
    }
}
