using System;
using PetaPoco;

namespace DocExpiryApp.Models
{
    [TableName("Enumeration"),PrimaryKey("Id",AutoIncrement = true)]
    public class Enumeration
    {
        [ResultColumn]
        public int Id { get; set; }
        public string EnumerationName { get; set; }
    }
} 