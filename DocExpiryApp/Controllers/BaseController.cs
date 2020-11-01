using System;
using System.Collections;
using System.Collections.Generic;
using PetaPoco;
using DocExpiryApp.Models;

namespace DocExpiryApp.Controllers
{
    public class BaseController
    {
        protected IDatabase database;
        public BaseController()
        {
            try{
                this.database = new Database("db");
            }
            catch(Exception ex)
            {
                LogController.Error(ex.Message);
            }
        }

    }
}