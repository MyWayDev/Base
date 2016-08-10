namespace CatEpoch.DataAccess.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class First : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.DimDates",
				c => new
					{
						DateKey = c.Int(nullable: false),
						DateAltKey = c.DateTime(nullable: false),
						Year = c.Int(nullable: false),
						Quarter = c.Int(nullable: false),
						Month = c.Int(nullable: false),
						MonthName = c.String(),
						DayOfMonth = c.Int(nullable: false),
						DayOfWeek = c.Int(nullable: false),
						DayName = c.String(),
						WeekOfMonth = c.Int(nullable: false),
					})
				.PrimaryKey(t => t.DateKey);
			
			CreateTable(
				"dbo.Periods",
				c => new
					{
						PeriodId = c.Int(nullable: false, identity: true),
						PeriodName = c.String(),
						StartDate = c.DateTime(nullable: false),
						EndDate = c.DateTime(nullable: false),
						Active = c.Boolean(nullable: false),
					})
				.PrimaryKey(t => t.PeriodId);
			
			CreateTable(
				"dbo.Promos",
				c => new
					{
						PromoId = c.Int(nullable: false, identity: true),
						promoName = c.String(),
						ProductId = c.String(maxLength: 10),
						PromoGrade = c.Int(nullable: false),
					})
				.PrimaryKey(t => t.PromoId)
				.ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
				.Index(t => t.ProductId);
			
			CreateTable(
				"dbo.Products",
				c => new
					{
						Id = c.String(nullable: false, maxLength: 10),
						ProductName = c.String(),
						BasePrice = c.Double(nullable: false),
						Active = c.Boolean(nullable: false),
						Discontnuied = c.Boolean(nullable: false),
						GroupId = c.Int(nullable: false),
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.ProductGroups", t => t.GroupId, cascadeDelete: true)
				.Index(t => t.GroupId);
			
			CreateTable(
				"dbo.ProductGroups",
				c => new
					{
						Id = c.Int(nullable: false, identity: true),
						GroupName = c.String(),
					})
				.PrimaryKey(t => t.Id);
			
			CreateTable(
				"dbo.ProductTrees",
				c => new
					{
						OrgNode = c.HierarchyId(nullable: false),
						//OrgLevel = c.Int(nullable: false),
						TreeName = c.String(),
						ParentId = c.Int(),
						GroupId = c.Int(nullable: false),
					})
				.PrimaryKey(t => t.OrgNode)
				.ForeignKey("dbo.ProductGroups", t => t.GroupId, cascadeDelete: true)
				.Index(t => t.GroupId);
			Sql("ALTER TABLE dbo.ProductTrees ADD OrgLevel AS (OrgNode.GetLevel())");
			Sql("CREATE UNIQUE INDEX ProductTreesNc1 ON dbo.ProductTrees(OrgLevel, OrgNode)");
			
			CreateTable(
				"dbo.SalesHistories",
				c => new
					{
						ProductId = c.String(nullable: false, maxLength: 10),
						Month = c.String(nullable: false, maxLength: 128),
						Year = c.Int(nullable: false),
						GroupId = c.Int(nullable: false),
						PromoId = c.Int(nullable: false),
						Units = c.Int(nullable: false),
						Value = c.Int(nullable: false),
						Forecast = c.Int(nullable: false),
						CountOutDays = c.Int(nullable: false),
						Booking = c.Boolean(nullable: false),
						Price = c.Single(nullable: false),
					})
				.PrimaryKey(t => new { t.ProductId, t.Month, t.Year })
				.ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
				.ForeignKey("dbo.ProductGroups", t => t.GroupId, cascadeDelete: false) //It dosent work with true
				.ForeignKey("dbo.Promos", t => t.PromoId, cascadeDelete: false) //It dosent work with true
				.Index(t => t.ProductId)
				.Index(t => t.GroupId)
				.Index(t => t.PromoId);
			
			CreateTable(
				"dbo.PromoDetails",
				c => new
					{
						promoDetailId = c.Int(nullable: false, identity: true),
						PromoDefId = c.Int(nullable: false),
						ProductId = c.String(maxLength: 10),
					})
				.PrimaryKey(t => t.promoDetailId)
				.ForeignKey("dbo.PromoDefs", t => t.PromoDefId, cascadeDelete: true)
				.ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
				.Index(t => t.PromoDefId)
				.Index(t => t.ProductId);
			
			CreateTable(
				"dbo.PromoDefs",
				c => new
					{
						PromoDefId = c.Int(nullable: false),
						PromoPrice = c.Double(nullable: false),
						PromoId = c.Int(nullable: false),
					})
				.PrimaryKey(t => t.PromoDefId)
				.ForeignKey("dbo.Promos", t => t.PromoDefId, cascadeDelete: false)  //It dosent work with true
				.Index(t => t.PromoDefId);
			
			CreateTable(
				"dbo.PromoPeriods",
				c => new
					{
						Promo_PromoId = c.Int(nullable: false),
						Period_PeriodId = c.Int(nullable: false),
					})
				.PrimaryKey(t => new { t.Promo_PromoId, t.Period_PeriodId })
				.ForeignKey("dbo.Promos", t => t.Promo_PromoId, cascadeDelete: true)
				.ForeignKey("dbo.Periods", t => t.Period_PeriodId, cascadeDelete: true)
				.Index(t => t.Promo_PromoId)
				.Index(t => t.Period_PeriodId);
			
			CreateStoredProcedure(
				"dbo.FillDimDate",
				p => new
					{
						STARTDATE= p.DateTime()
					},
				body:
					@" DECLARE @ENDDATE DATETIME
 SET @ENDDATE=getdate()
 DECLARE @LOOPDATE DATETIME
 SET @LOOPDATE = @STARTDATE
 WHILE @LOOPDATE <= @ENDDATE
 BEGIN
 
 INSERT [dbo].[DimDates]([DateKey], [DateAltKey], [Year], [Quarter], [Month], [MonthName], [DayOfMonth], [DayOfWeek], [DayName], [WeekOfMonth])
					  VALUES (
					  CAST(CONVERT(VARCHAR(8),@LOOPDATE,112)AS INT),
					  @LOOPDATE,
					  YEAR(@LOOPDATE),
					  Datepart(qq,@LOOPDATE),
					  Month(@LOOPDATE),
					  datename(mm,@LOOPDATE),
					  Day(@LOOPDATE),
					  datepart(dw,@LOOPDATE),
					  datename(dw,@LOOPDATE),
					  DATEPART(WEEK, @LOOPDATE)  - DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,@LOOPDATE), 0))+ 1 
					  )
					  SET @LOOPDATE = DateAdd(dd,1,@LOOPDATE)
					  END"
			);
			
		   
			CreateStoredProcedure(
				"dbo.AddGroup",
			  p => new
			  {
				  grname = p.String(),
				  prntid = p.Int()
			  },
				body:
						   @"  DECLARE @pOrgNode hierarchyid, @lc hierarchyid ,@grpid int
   SELECT @pOrgNode = [OrgNode] 
   FROM [dbo].[ProductTrees] 
   WHERE [GroupId] = @prntid
   SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
   BEGIN TRANSACTION
	  SELECT @lc = max([OrgNode]) 
	  FROM [dbo].[ProductTrees]
	  WHERE [OrgNode].GetAncestor(1) =@pOrgNode ;
	  INSERT [dbo].[ProductTrees] ([TreeName])
	  VALUES(@grname)
	  SELECT @grpid=Id
	  FROM [dbo].[ProductGroups]
	  WHERE [GroupName]=@grname
	  INSERT [dbo].[ProductTrees] ([OrgNode], [GroupId],[ParentId], [TreeName])
	  VALUES(@pOrgNode.GetDescendant(@lc, NULL), @grpid, @prntid, @grname)
	  COMMIT"
			);
			
		  
			
		}
		
		public override void Down()
		{
		   
			DropStoredProcedure("dbo.AddGroup");
		   
			DropStoredProcedure("dbo.FillDimDate");
			DropForeignKey("dbo.PromoDefs", "PromoDefId", "dbo.Promos");
			DropForeignKey("dbo.Promos", "ProductId", "dbo.Products");
			DropForeignKey("dbo.PromoDetails", "ProductId", "dbo.Products");
			DropForeignKey("dbo.PromoDetails", "PromoDefId", "dbo.PromoDefs");
			DropForeignKey("dbo.Products", "GroupId", "dbo.ProductGroups");
			DropForeignKey("dbo.SalesHistories", "PromoId", "dbo.Promos");
			DropForeignKey("dbo.SalesHistories", "GroupId", "dbo.ProductGroups");
			DropForeignKey("dbo.SalesHistories", "ProductId", "dbo.Products");
			DropForeignKey("dbo.ProductTrees", "GroupId", "dbo.ProductGroups");
			DropForeignKey("dbo.PromoPeriods", "Period_PeriodId", "dbo.Periods");
			DropForeignKey("dbo.PromoPeriods", "Promo_PromoId", "dbo.Promos");
			DropIndex("dbo.PromoPeriods", new[] { "Period_PeriodId" });
			DropIndex("dbo.PromoPeriods", new[] { "Promo_PromoId" });
			DropIndex("dbo.PromoDefs", new[] { "PromoDefId" });
			DropIndex("dbo.PromoDetails", new[] { "ProductId" });
			DropIndex("dbo.PromoDetails", new[] { "PromoDefId" });
			DropIndex("dbo.SalesHistories", new[] { "PromoId" });
			DropIndex("dbo.SalesHistories", new[] { "GroupId" });
			DropIndex("dbo.SalesHistories", new[] { "ProductId" });
			DropIndex("dbo.ProductTrees", new[] { "GroupId" });
			DropIndex("dbo.Products", new[] { "GroupId" });
			DropIndex("dbo.Promos", new[] { "ProductId" });
			DropTable("dbo.PromoPeriods");
			DropTable("dbo.PromoDefs");
			DropTable("dbo.PromoDetails");
			DropTable("dbo.SalesHistories");
			DropTable("dbo.ProductTrees");
			DropTable("dbo.ProductGroups");
			DropTable("dbo.Products");
			DropTable("dbo.Promos");
			DropTable("dbo.Periods");
			DropTable("dbo.DimDates");
		}
	}
}
