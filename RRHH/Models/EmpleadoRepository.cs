using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using RRHH.Models;

namespace RRHH.Models
{
    public class EmpleadoRepository
    {

        /***********************AGREGAR EMPLEADOS*****************************/

        public void AgregarEmpleado(Empleado empleado)
        {
            if (empleado == null)
            {
                throw new ArgumentNullException(nameof(empleado), "El empleado no puede ser nulo");
            }

            var context = new ValidationContext(empleado, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(empleado, context, results, true))
            {
                string errors = string.Join(", ", results.Select(r => r.ErrorMessage));
                throw new ArgumentException($"Errores de validación: {errors}");
            }

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_agregarEmpleado", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DNI", empleado.DNI);
                cmd.Parameters.AddWithValue("@NombreCompleto", empleado.NombreCompleto);
                cmd.Parameters.AddWithValue("@Direccion", empleado.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", empleado.Telefono);
                cmd.Parameters.AddWithValue("@Correo", empleado.Correo);
                cmd.Parameters.AddWithValue("@NumeroDeHijos", empleado.NumeroDeHijos);
                cmd.Parameters.AddWithValue("@EstadoCivil", empleado.EstadoCivil);
                cmd.Parameters.AddWithValue("@PuestoDeTrabajo", empleado.PuestoDeTrabajo);
                cmd.Parameters.AddWithValue("@Salario", empleado.Salario);

                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }


        /*********ELIMINAR EMPLEADO**********************/

        public void EliminarEmpleadoPorDNI(string DNI)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_eliminarEmpleadoPorDNI", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DNI", DNI);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    
                    throw new Exception("Error al intentar eliminar el empleado por DNI.", ex);
                }
                finally
                {
                    cn.Close();
                }
            }


        }
    }
}