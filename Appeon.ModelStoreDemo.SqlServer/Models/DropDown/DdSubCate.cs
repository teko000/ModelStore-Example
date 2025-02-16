﻿using SnapObjects.Data;
using System.ComponentModel.DataAnnotations;

namespace Appeon.ModelStoreDemo.SqlServer.Models
{
    [FromTable("productsubcategory", Schema = "production")]
    public class DdSubCate
    {
        [Key]
        [Identity]
        public int Productsubcategoryid { get; set; }

        [ConcurrencyCheck]
        public int Productcategoryid { get; set; }

        [ConcurrencyCheck]
        public string Name { get; set; }
    }
}
