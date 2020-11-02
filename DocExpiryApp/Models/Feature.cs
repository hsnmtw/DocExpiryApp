using System;
using PetaPoco;

namespace DocExpiryApp.Models
{
    [TableName("Feature"),PrimaryKey("Id",AutoIncrement = true)]
    public class Feature
    {
        [ResultColumn]
        public int Id { get; set; }
        public string FeatureName { get; set; }
    }
} 