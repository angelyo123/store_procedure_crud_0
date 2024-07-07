using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class Empleado
    {

        public int EmpleadoID { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio")]
        [StringLength(8, ErrorMessage = "El DNI debe tener 9 caracteres")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre completo no debe exceder los 100 caracteres")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200, ErrorMessage = "La dirección no debe exceder los 200 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "El teléfono no tiene un formato válido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        public string Correo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El número de hijos no puede ser negativo")]
        public int NumeroDeHijos { get; set; }

        [Required(ErrorMessage = "El estado civil es obligatorio")]
        [StringLength(50, ErrorMessage = "El estado civil no debe exceder los 50 caracteres")]
        public string EstadoCivil { get; set; }

        [Required(ErrorMessage = "El puesto de trabajo es obligatorio")]
        [StringLength(100, ErrorMessage = "El puesto de trabajo no debe exceder los 100 caracteres")]
        public string PuestoDeTrabajo { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El salario no puede ser negativo")]
        public decimal Salario { get; set; }

    }
}