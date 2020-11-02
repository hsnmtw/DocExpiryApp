using System;
using System.Collections.Generic;
using DocExpiryApp.Models;

namespace DocExpiryApp.Controllers
{
    public class FeatureController : BaseController
    {
        public List<Feature> SelectAll()
        {
            try
            {
                return this.database.Fetch<Feature>(ConfigController.Instance["sql.features.query"]);
            }
            catch (Exception ex)
            {
                LogController.Error(ex.Message);
            }
            return new List<Feature>();
        }

        public bool Delete(Feature Feature)
        {
            return database.Delete(Feature) > 0;
        }

        public bool Save(Feature Feature)
        {
            try
            {
                if(Feature.Id == 0)
                {
                    return int.Parse( this.database.Insert(Feature).ToString() ) > 0;
                }
                else
                {
                    return int.Parse( this.database.Update(Feature).ToString() ) > 0;
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