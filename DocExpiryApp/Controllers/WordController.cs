using System;
using System.Collections.Generic;
using DocExpiryApp.Models;

namespace DocExpiryApp.Controllers
{
    public class WordController : BaseController
    {
        public static List<Word> Data {get;set;}

        public List<Word> SelectAll()
        {
            try
            {
                return this.database.Fetch<Word>(ConfigController.Instance["sql.words.query"]);
            }
            catch (Exception ex)
            {
                LogController.Error(ex.Message);
            }
            return new List<Word>();
        }

        public bool Delete(Word Word)
        {
            return database.Delete(Word) > 0;
        }

        public bool Save(Word Word)
        {
            try
            {
                if(Word.Id == 0)
                {
                    return int.Parse( this.database.Insert(Word).ToString() ) > 0;
                }
                else
                {
                    return int.Parse( this.database.Update(Word).ToString() ) > 0;
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