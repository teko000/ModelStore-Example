using SnapObjects.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appeon.ModelStoreDemo.Oracle.Models
{
    [Table("BusinessEntity", Schema = "Person")]
    public class BusinessEntity
    {
        [Key]
        [Identity]
        public Int32 BusinessEntityID { get; set; }

        [SqlDefaultValue("(newid())")]
        public string Rowguid { get; set; }

        [SqlDefaultValue("(getdate())")]
        public DateTime ModifiedDate { get; set; }

    }
}
