﻿using SnapObjects.Data;
using System.ComponentModel.DataAnnotations;

namespace Appeon.ModelStoreDemo.SqlServer.Models
{
    [FromTable("AddressType",Schema = "Person")]
    [SqlOrderBy("Addresstypeid")]
    public class DdAddressType
    {
        [Key]
        [Identity]
        public int Addresstypeid { get; set; }

        [ConcurrencyCheck]
        public string Name { get; set; }
    }
}
