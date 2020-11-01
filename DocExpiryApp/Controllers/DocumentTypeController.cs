using System;
using System.Collections.Generic;
using DocExpiryApp.Models;

namespace DocExpiryApp.Controllers
{
    public class DocumentTypeController : BaseController
    {
        public List<DocumentType> SelectAll()
        {
            try
            {
                return this.database.Fetch<DocumentType>(ConfigController.Instance["sql.documenttypes.query"]);
            }
            catch (Exception ex)
            {
                LogController.Error(ex.Message);
            }
            return new List<DocumentType>();
        }

        public bool Save(DocumentType documentType)
        {
            try
            {
                if(documentType.Id == 0)
                {
                    return int.Parse( this.database.Insert(documentType).ToString() ) > 0;
                }
                else
                {
                    return int.Parse( this.database.Update(documentType).ToString() ) > 0;
                }
            }
            catch (Exception ex)
            {
                LogController.Error(ex.Message);
            }
            return false;
        }
            
    }
}