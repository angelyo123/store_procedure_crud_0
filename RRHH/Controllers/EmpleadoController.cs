using RRHH.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RRHH.Controllers
{

    /***************LISTAR EMPLEADOS*************/
    public class EmpleadoController : Controller
    {

        IEnumerable<Empleado> empleados()
        {
            List<Empleado> empleados = new List<Empleado>();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_listarEmpleados", cn);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    empleados.Add(new Empleado
                    {

                        EmpleadoID = dr.GetInt32(0),
                        DNI = dr.GetString(1),
                        NombreCompleto = dr.GetString(2),
                        Direccion = dr.GetString(3),
                        Telefono = dr.GetString(4),
                        Correo = dr.GetString(5),
                        NumeroDeHijos = dr.GetInt32(6),
                        EstadoCivil = dr.GetString(7),
                        PuestoDeTrabajo = dr.GetString(8),
                        Salario = dr.GetDecimal(9)

                    });
                }
                dr.Close();
                cn.Close();

            }
            return empleados;
        }

        public ActionResult ListaEmpleados()
        {
            return View(empleados());
        }

        /*************AGREGAR EMPLEADO****************/
        private EmpleadoRepository repo = new EmpleadoRepository();

        public ActionResult AgregarEmpleado()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarEmpleado(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repo.AgregarEmpleado(empleado);
                    return RedirectToAction("ListaEmpleados");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(empleado);
        }



        /***************ELIMINAR EMPLEADO*****************************************/

        [HttpPost]
        public ActionResult EliminarEmpleado(string DNI)
        {
            try
            {
                repo.EliminarEmpleadoPorDNI(DNI);
                return RedirectToAction("ListaEmpleados");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al eliminar el empleado: {ex.Message}");
                return RedirectToAction("ListaEmpleados");
            }
        }

    }
    }