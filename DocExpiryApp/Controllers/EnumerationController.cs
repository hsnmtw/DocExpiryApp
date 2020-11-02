using System;
using System.Collections.Generic;
using DocExpiryApp.Models;

namespace DocExpiryApp.Controllers
{
    public class EnumerationController : BaseController
    {
        public List<Enumeration> SelectAll()
        {
            try
            {
                return this.database.Fetch<Enumeration>(ConfigController.Instance["sql.enumerations.query"]);
            }
            catch (Exception ex)
            {
                LogController.Error(ex.Message);
            }
            return new List<Enumeration>();
        }

        public bool Delete(Enumeration Enumeration)
        {
            return database.Delete(Enumeration) > 0;
        }

        public bool Save(Enumeration Enumeration)
        {
            try
            {
                if(Enumeration.Id == 0)
                {
                    return int.Parse( this.database.Insert(Enumeration).ToString() ) > 0;
                }
                else
                {
                    return int.Parse( this.database.Update(Enumeration).ToString() ) > 0;
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