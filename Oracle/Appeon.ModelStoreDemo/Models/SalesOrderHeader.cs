﻿using SnapObjects.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appeon.ModelStoreDemo.Models
{

    [SqlParameter("saleId", typeof(int))]
    [Table("SalesOrderHeader", Schema = "Sales")]
    [SqlWhere("SalesOrderID=:saleId")]
    public class SalesOrderHeader
    {
        [Key]
        [Identity]
        [SqlColumn("SalesOrderID")]
        public int SalesOrderID { get; set; }

        [SqlColumn("RevisionNumber")]
        public int? RevisionNumber { get; set; }

        [SqlColumn("OrderDate")]
        public DateTime? OrderDate { get; set; }

        [SqlColumn("DueDate")]
        public DateTime? DueDate { get; set; }

        [SqlColumn("ShipDate")]
        public DateTime? ShipDate { get; set; }

        [SqlColumn("Status")]
        public int? Status { get; set; }

        [SqlColumn("OnlineOrderFlag")]
        public int? OnlineOrderFlag { get; set; }

        [SqlColumn("SalesOrderNumber")]
        //[SqlCompute("(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***'))")]
        public string SalesOrderNumber { get; set; }

        [SqlColumn("PurchaseOrderNumber")]
        public string PurchaseOrderNumber { get; set; }

        [SqlColumn("AccountNumber")]
        public string AccountNumber { get; set; }

        [SqlColumn("CustomerID")]
        public int? CustomerID { get; set; }

        [SqlColumn("SalesPersonID")]
        public int? SalesPersonID { get; set; }

        [SqlColumn("TerritoryID")]
        public int? TerritoryID { get; set; }

        [SqlColumn("BillToAddressID")]
        public int? BillToAddressID { get; set; }

        [SqlColumn("ShipToAddressID")]
        public int? ShipToAddressID { get; set; }

        [SqlColumn("ShipMethodID")]
        public int? ShipMethodID { get; set; }

        [SqlColumn("CreditCardID")]
        public int? CreditCardID { get; set; }

        [SqlColumn("CreditCardApprovalCode")]
        public string CreditCardApprovalCode { get; set; }

        [SqlColumn("CurrencyRateID")]
        public int? CurrencyRateID { get; set; }

        [SqlColumn("SubTotal")]
        public decimal? SubTotal { get; set; }

        [SqlColumn("TaxAmt")]
        public decimal? TaxAmt { get; set; }

        [SqlColumn("Freight")]
        public decimal? Freight { get; set; }

        [SqlColumn("TotalDue")]
        //[SqlCompute("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))")]
        public decimal? TotalDue { get; set; }

        [SqlColumn("\"COMMENT\"")]
        public string Comment { get; set; }

        [SqlColumn("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }

    }
}
