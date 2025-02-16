﻿using SnapObjects.Data;
using System.ComponentModel.DataAnnotations;

namespace Appeon.ModelStoreDemo.Oracle.Models
{
    [FromTable("Address", Schema = "Person")]
    [JoinTable(name: "StateProvince", Schema = "Person", JoinType = SqlJoinType.Left, 
               OnRaw = "Address.StateProvinceID = StateProvince.StateProvinceID")]
    [SqlOrderBy("Address.AddressID")]
    public class DdAddress
    {
        [Key]
        [Identity]
        [SqlColumn("Address", "AddressID")]
        public int AddressID { get; set; }

        [SqlColumn("Address", "AddressLine1")]
        public string AddressLine1 { get; set; }

        [SqlColumn("Address", "AddressLine2")]
        public string AddressLine2 { get; set; }

        [SqlColumn("Address", "City")]
        public string City { get; set; }

        [SqlColumn("Address", "StateProvinceID")]
        public int StateProvinceID { get; set; }

        [SqlColumn("StateProvince", "Name")]
        public string StateProvince_Name { get; set; }

    }
}
