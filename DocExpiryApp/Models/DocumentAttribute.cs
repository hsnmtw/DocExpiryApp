using System;
using PetaPoco;

namespace DocExpiryApp.Models
{
    [TableName("Document"),PrimaryKey("Id",AutoIncrement = true)]
    public class DocumentAttribute
    {
        [ResultColumn]
        public int Id { get; set; }
        public string DocumentAttributeName { get; set; }
    }
} 