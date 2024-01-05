namespace SupplyManagementAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_m_accounts",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        password = c.String(),
                        otp = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        is_used = c.Boolean(nullable: false),
                        expired_time = c.DateTime(nullable: false),
                        role_guid = c.Guid(nullable: false),
                        created_date = c.DateTime(),
                        modified_date = c.DateTime(),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.tb_m_company", t => t.Guid)
                .ForeignKey("dbo.tb_m_roles", t => t.role_guid)
                .Index(t => t.Guid)
                .Index(t => t.role_guid);
            
            CreateTable(
                "dbo.tb_m_company",
                c => new
                    {
                        guid = c.Guid(nullable: false),
                        name = c.String(),
                        address = c.String(),
                        email = c.String(),
                        phone_number = c.String(),
                        foto = c.String(),
                        created_date = c.DateTime(),
                        modified_date = c.DateTime(),
                    })
                .PrimaryKey(t => t.guid);
            
            CreateTable(
                "dbo.tb_m_vendor",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        bidang_usaha = c.String(),
                        jenis_perusahaan = c.String(),
                        status_vendor = c.Int(nullable: false),
                        created_date = c.DateTime(),
                        modified_date = c.DateTime(),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.tb_m_company", t => t.Guid)
                .Index(t => t.Guid);
            
            CreateTable(
                "dbo.tb_m_roles",
                c => new
                    {
                        guid = c.Guid(nullable: false),
                        name = c.String(),
                        created_date = c.DateTime(),
                        modified_date = c.DateTime(),
                    })
                .PrimaryKey(t => t.guid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_m_accounts", "role_guid", "dbo.tb_m_roles");
            DropForeignKey("dbo.tb_m_vendor", "Guid", "dbo.tb_m_company");
            DropForeignKey("dbo.tb_m_accounts", "Guid", "dbo.tb_m_company");
            DropIndex("dbo.tb_m_vendor", new[] { "Guid" });
            DropIndex("dbo.tb_m_accounts", new[] { "role_guid" });
            DropIndex("dbo.tb_m_accounts", new[] { "Guid" });
            DropTable("dbo.tb_m_roles");
            DropTable("dbo.tb_m_vendor");
            DropTable("dbo.tb_m_company");
            DropTable("dbo.tb_m_accounts");
        }
    }
}
