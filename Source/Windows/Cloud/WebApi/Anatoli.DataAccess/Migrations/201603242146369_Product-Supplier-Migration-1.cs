namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductSupplierMigration1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductSupliers", newName: "ProductSuppliers");
            RenameColumn(table: "dbo.ProductSuppliers", name: "SuplierID", newName: "SupplierID");
            RenameIndex(table: "dbo.ProductSuppliers", name: "IX_SuplierID", newName: "IX_SupplierID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProductSuppliers", name: "IX_SupplierID", newName: "IX_SuplierID");
            RenameColumn(table: "dbo.ProductSuppliers", name: "SupplierID", newName: "SuplierID");
            RenameTable(name: "dbo.ProductSuppliers", newName: "ProductSupliers");
        }
    }
}
