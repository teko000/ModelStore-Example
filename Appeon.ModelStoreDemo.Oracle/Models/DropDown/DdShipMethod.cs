﻿using SnapObjects.Data;
using System.ComponentModel.DataAnnotations;

namespace Appeon.ModelStoreDemo.Oracle.Models
{
    [FromTable("ShipMethod", Schema = "Purchasing")]
    public class DdShipMethod
    {
        [Key]
        [Identity]
        public int Shipmethodid { get; set; }

        public string Name { get; set; }

        public decimal Shipbase { get; set; }

        public decimal Shiprate { get; set; }
    }
}
