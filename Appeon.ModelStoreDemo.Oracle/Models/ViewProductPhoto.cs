﻿using SnapObjects.Data;

namespace Appeon.ModelStoreDemo.Oracle.Models
{
    [Top(1)]
    [SqlParameter("proid",typeof(int))]
    [FromTable("ProductPhoto",Schema = "Production")]
    [FromTable("ProductProductPhoto",Schema = "Production")]
    [SqlWhere("(ProductPhoto.ProductPhotoID = ProductProductPhoto.ProductPhotoID) "+
         " and (ProductProductPhoto.ProductID = :proid) and (ProductProductPhoto.Primary = 1)")]    
    [SqlOrderBy("ProductPhoto.ModifiedDate desc")]
    public class ViewProductPhoto
    {
        [SqlColumn("ProductPhoto", "LargePhotoFileName")]
        public string LargePhotoFileName { get; set; }

        [SqlColumn("ProductPhoto", "LargePhoto")]
        public byte[] LargePhoto { get; set; }
    }
}
