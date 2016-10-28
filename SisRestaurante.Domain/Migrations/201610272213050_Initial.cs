namespace SisRestaurante.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prato",
                c => new
                    {
                        PratoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 100, unicode: false),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RestauranteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PratoId)
                .ForeignKey("dbo.Restaurante", t => t.RestauranteId)
                .Index(t => t.RestauranteId);
            
            CreateTable(
                "dbo.Restaurante",
                c => new
                    {
                        RestauranteId = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.RestauranteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prato", "RestauranteId", "dbo.Restaurante");
            DropIndex("dbo.Prato", new[] { "RestauranteId" });
            DropTable("dbo.Restaurante");
            DropTable("dbo.Prato");
        }
    }
}
