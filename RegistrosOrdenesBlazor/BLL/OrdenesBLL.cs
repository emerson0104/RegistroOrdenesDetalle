using Microsoft.EntityFrameworkCore;
using RegistrosOrdenesBlazor.DAL;
using RegistrosOrdenesBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RegistrosOrdenesBlazor.BLL
{
    public class OrdenesBLL
    {
        public static bool Guardar(Ordenes ordene)
        {
            if (!Existe(ordene.OrdenId))
                return Insertar(ordene);
            else
                return Modificar(ordene);

        }

        private static bool Insertar(Ordenes ordenes)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {

                foreach (var item in ordenes.OrdenDetalles)
                {
                    var Orden = contexto.Productos.Find(item.ProductoId);
                    if (Orden != null)
                    {
                        Orden.Inventario += item.Cantidad;
                    }
                }
                contexto.Ordenes.Add(ordenes);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;

            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }


        private static bool Modificar(Ordenes ordenes)
        {
            bool paso = false;
            var Anterior = Buscar(ordenes.OrdenId);
            Contexto contexto = new Contexto();

            try
            {
                
                foreach (var item in Anterior.OrdenDetalles)
                {
                    var xproduct = contexto.Productos.Find(item.ProductoId);
                    if (!ordenes.OrdenDetalles.Exists(d => d.OrdenDetalleId == item.OrdenDetalleId))
                    {
                        if (xproduct != null)
                        {
                            xproduct.Inventario -= item.Cantidad;
                        }

                        contexto.Entry(item).State = EntityState.Deleted;
                    }

                }

            
                foreach (var item in ordenes.OrdenDetalles)
                {
                    var xproduct = contexto.Productos.Find(item.ProductoId);
                    if (item.OrdenDetalleId == 0)
                    {
                        contexto.Entry(item).State = EntityState.Added;
                        if (xproduct != null)
                        {
                            xproduct.Inventario += item.Cantidad;
                        }

                    }
                    else
                        contexto.Entry(item).State = EntityState.Modified;
                }


                contexto.Entry(ordenes).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            var Anterior = Buscar(id);
            Contexto contexto = new Contexto();

            try
            {
                if (Existe(id))
                {
                    
                    foreach (var item in Anterior.OrdenDetalles)
                    {
                        var auxProducto = contexto.Productos.Find(item.
                            ProductoId);
                        if (auxProducto != null)
                        {
                            auxProducto.Inventario -= item.Cantidad;
                        }
                    }

                    var orden = contexto.Ordenes.Find(id);
                    if (orden != null)
                    {
                        contexto.Ordenes.Remove(orden);
                        paso = contexto.SaveChanges() > 0;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static Ordenes Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Ordenes ordenes;

            try
            {
                ordenes = contexto.Ordenes.Where(o => o.OrdenId == id).Include(d => d.OrdenDetalles).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return ordenes;

        }

        public static List<Ordenes> GetList(Expression<Func<Ordenes, bool>> expression)
        {
            List<Ordenes> lista = new List<Ordenes>();
            Contexto db = new Contexto();

            try
            {
                lista = db.Ordenes.Where(expression).ToList();
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

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool Paso = false;

            try
            {
                Paso = contexto.Ordenes.Any(a => a.OrdenId == id);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return Paso;

        }
    }
}
