namespace Pocos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.current_accounts",
                c => new
                    {
                        current_id = c.String(nullable: false, maxLength: 128),
                        account_number = c.String(),
                        creation_date = c.DateTime(nullable: false),
                        balance = c.Double(nullable: false),
                        branch_code = c.String(),
                        overdraft_amount = c.Double(nullable: false),
                        overdraft_rate = c.Double(nullable: false),
                        close_date = c.DateTime(),
                        customer_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.current_id)
                .ForeignKey("dbo.customers", t => t.customer_id, cascadeDelete: true)
                .Index(t => t.customer_id);
            
            CreateTable(
                "dbo.customers",
                c => new
                    {
                        customer_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(nullable: false),
                        last_name = c.String(nullable: false),
                        username = c.String(nullable: false),
                        email = c.String(),
                        phone_number = c.String(maxLength: 20),
                        street_address = c.String(maxLength: 100),
                        city = c.String(maxLength: 100),
                        province = c.String(maxLength: 100),
                        postal_code = c.String(),
                    })
                .PrimaryKey(t => t.customer_id);
            
            CreateTable(
                "dbo.messages",
                c => new
                    {
                        message_id = c.Int(nullable: false, identity: true),
                        email_message = c.String(),
                        log_message = c.String(),
                        customer_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.message_id)
                .ForeignKey("dbo.customers", t => t.customer_id, cascadeDelete: true)
                .Index(t => t.customer_id);
            
            CreateTable(
                "dbo.savings_accounts",
                c => new
                    {
                        savings_id = c.String(nullable: false, maxLength: 128),
                        account_number = c.String(),
                        creation_date = c.DateTime(nullable: false),
                        balance = c.Double(nullable: false),
                        branch_code = c.String(),
                        close_date = c.DateTime(),
                        interest_rate_id = c.Int(nullable: false),
                        customer_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.savings_id)
                .ForeignKey("dbo.customers", t => t.customer_id, cascadeDelete: true)
                .ForeignKey("dbo.interest_rates", t => t.interest_rate_id, cascadeDelete: true)
                .Index(t => t.interest_rate_id)
                .Index(t => t.customer_id);
            
            CreateTable(
                "dbo.interest_rates",
                c => new
                    {
                        interest_rate_id = c.Int(nullable: false, identity: true),
                        interest = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.interest_rate_id);
            
            CreateTable(
                "dbo.transaction_details",
                c => new
                    {
                        trans_detail_id = c.Int(nullable: false, identity: true),
                        transaction_date = c.DateTime(nullable: false),
                        amount = c.Double(nullable: false),
                        transaction_type = c.String(nullable: false),
                        description = c.String(),
                        transaction_id = c.Int(nullable: false),
                        account_id = c.String(nullable: false, maxLength: 128),
                        customer_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.trans_detail_id)
                .ForeignKey("dbo.transactions", t => new { t.transaction_id, t.account_id, t.customer_id }, cascadeDelete: true)
                .Index(t => new { t.transaction_id, t.account_id, t.customer_id });
            
            CreateTable(
                "dbo.transactions",
                c => new
                    {
                        transaction_id = c.Int(nullable: false),
                        account_id = c.String(nullable: false, maxLength: 128),
                        customer_id = c.Int(nullable: false),
                        account_type = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.transaction_id, t.account_id, t.customer_id });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.transaction_details", new[] { "transaction_id", "account_id", "customer_id" }, "dbo.transactions");
            DropForeignKey("dbo.current_accounts", "customer_id", "dbo.customers");
            DropForeignKey("dbo.savings_accounts", "interest_rate_id", "dbo.interest_rates");
            DropForeignKey("dbo.savings_accounts", "customer_id", "dbo.customers");
            DropForeignKey("dbo.messages", "customer_id", "dbo.customers");
            DropIndex("dbo.transaction_details", new[] { "transaction_id", "account_id", "customer_id" });
            DropIndex("dbo.savings_accounts", new[] { "customer_id" });
            DropIndex("dbo.savings_accounts", new[] { "interest_rate_id" });
            DropIndex("dbo.messages", new[] { "customer_id" });
            DropIndex("dbo.current_accounts", new[] { "customer_id" });
            DropTable("dbo.transactions");
            DropTable("dbo.transaction_details");
            DropTable("dbo.interest_rates");
            DropTable("dbo.savings_accounts");
            DropTable("dbo.messages");
            DropTable("dbo.customers");
            DropTable("dbo.current_accounts");
        }
    }
}
