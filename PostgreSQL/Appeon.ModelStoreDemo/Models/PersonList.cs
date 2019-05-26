﻿using SnapObjects.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appeon.ModelStoreDemo.Models
{
    [SqlParameter("personType", typeof(string))]
    [Table("Person", Schema = "Person")]
    [SqlWhere("Persontype = :personType")]
    public class PersonList
    {
        [Key]
        public int Businessentityid { get; set; }

        public string Persontype { get; set; }

        public bool Namestyle { get; set; }

        public string Title { get; set; }

        public string Firstname { get; set; }

        public string Middlename { get; set; }

        public string Lastname { get; set; }

        public string Suffix { get; set; }

        public int Emailpromotion { get; set; }

        public DateTime Modifieddate { get; set; }
    }
}
