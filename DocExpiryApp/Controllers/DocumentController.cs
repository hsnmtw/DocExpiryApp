using System;
using System.Collections.Generic;
using DocExpiryApp.Models;

namespace DocExpiryApp.Controllers
{
    public class DocumentController : BaseController
    {
        public List<Document> SelectAll()
        {
            try
            {
                return this.database.Fetch<Document>(ConfigController.Instance["sql.documents.query"]);
            }
            catch (Exception ex)
            {
                LogController.Error(ex.Message);
            }
            return new List<Document>();
        }

            
    }
}