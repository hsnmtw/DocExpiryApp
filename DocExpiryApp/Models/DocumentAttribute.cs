using System;
using PetaPoco;

namespace DocExpiryApp.Models
{
    [TableName("DocumentAttribute"),PrimaryKey("Id",AutoIncrement = true)]
    public class DocumentAttribute
    {
        [ResultColumn]
        public int Id { get; set; }
        public string DocumentAttributeName { get; set; }
    }
} 