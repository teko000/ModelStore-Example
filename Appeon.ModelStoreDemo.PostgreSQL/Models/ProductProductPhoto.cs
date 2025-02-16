﻿using SnapObjects.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appeon.ModelStoreDemo.PostgreSQL.Models
{
    [SqlParameter("prodid", typeof(int))]
    [Table("ProductProductPhoto", Schema = "production")]
    [SqlWhere("ProductID = :prodid")]
    public class ProductProductPhoto
    {
        [Key]
        public int ProductID { get; set; }

        public int ProductPhotoID { get; set; }

        [SqlColumn("ProductProductPhoto", "\"primary\"", "Primary")]
        public bool? Primary { get; set; }

        [SqlDefaultValue("(getdate())")]
        public DateTime ModifiedDate { get; set; }
       
    }
}
