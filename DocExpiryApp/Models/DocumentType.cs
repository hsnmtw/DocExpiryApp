using System;
using PetaPoco;

namespace DocExpiryApp.Models
{
    [TableName("DocumentType"),PrimaryKey("Id",AutoIncrement = true)]
    public class DocumentType
    {
        [ResultColumn]
        public int Id { get; set; }
        public string DocumentTypeName { get; set; }
    }
} 