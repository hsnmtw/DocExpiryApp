using System;
using PetaPoco;

namespace DocExpiryApp.Models
{
    [TableName("Document"),PrimaryKey("Id",AutoIncrement = true)]
    public class Document
    {
        [ResultColumn]
        public int Id { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public DateTime ExpiryDate { get; set; }
        [Ignore]
        public int RemainingDays 
        {
            get
            {
                if(ExpiryDate == null) return Int32.MinValue;
                return (DateTime.Today - ExpiryDate).Days;
            }
        }
    }
} 