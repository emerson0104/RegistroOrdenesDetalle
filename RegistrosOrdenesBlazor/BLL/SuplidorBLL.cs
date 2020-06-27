using RegistrosOrdenesBlazor.DAL;
using RegistrosOrdenesBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RegistrosOrdenesBlazor.BLL
{
    public class SuplidorBLL
    {
        public static List<Suplidores> GetList(Expression<Func<Suplidores, bool>> expression)
        {
            List<Suplidores> lista = new List<Suplidores>();
            Contexto db = new Contexto();

            try
            {
                lista = db.Suplidores.Where(expression).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }

            return lista;
        }
    }
}
