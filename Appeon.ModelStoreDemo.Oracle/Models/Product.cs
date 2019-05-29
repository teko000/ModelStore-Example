﻿using SnapObjects.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appeon.ModelStoreDemo.Oracle.Models
{
    [SqlParameter("prodid", typeof(int))]
    [Table("Product", Schema = "production")]
    [SqlWhere("Productid = :prodid")]
    [SqlOrderBy("Productid ASC")]
    public class Product
    {
        [Key]
        [Identity]
        public int Productid { get; set; }

        public string Name { get; set; }

        public string Productnumber { get; set; }

        public int? Makeflag { get; set; }

        public string Color { get; set; }

        public int Safetystocklevel { get; set; }

        public int Reorderpoint { get; set; }

        public decimal Standardcost { get; set; }

        public decimal Listprice { get; set; }

        [SqlColumn("\"SIZE\"")]
        public string Size { get; set; }

        public string Sizeunitmeasurecode { get; set; }

        public string Weightunitmeasurecode { get; set; }

        public decimal? Weight { get; set; }

        public int Daystomanufacture { get; set; }

        public string Productline { get; set; }

        public string Class { get; set; }

        public string Style { get; set; }

        public int? Productsubcategoryid { get; set; }

        public int? Productmodelid { get; set; }

        public DateTime Sellstartdate { get; set; }

        public DateTime? Sellenddate { get; set; }

        public DateTime Modifieddate { get; set; }

        public int? Finishedgoodsflag { get; set; }

    }
}

