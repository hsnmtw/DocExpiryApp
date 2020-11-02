using System;
using PetaPoco;

namespace DocExpiryApp.Models
{
    [TableName("Word"),PrimaryKey("Id",AutoIncrement = true)]
    public class Word
    {
        [ResultColumn]
        public int Id { get; set; }
        public string WordEnglish { get; set; }
        public string WordArabic  { get; set; }
    }
} 