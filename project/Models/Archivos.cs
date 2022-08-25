﻿using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Archivo
    {
        public Archivo()
        {
            this.Id = Guid.NewGuid();
            this.Creado = DateTime.Now;
        }

        public Lecciones lecciones { get; set; }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTime Creado { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(4)]
        [Required]
        public string Extension { get; set; }

        [Required]
        public string Tipo { get; set; }

        // PROPIEDADES PRIVADAS
        //public string PathRelativo
        //{
        //    get
        //    {
        //        return ConfigurationManager.AppSettings["PathArchivos"] +
        //                                    this.Id.ToString() + "." +
        //                                    this.Extension;
        //    }
        //}

        //public string PathCompleto
        //{
        //    get
        //    {
        //        var _PathAplicacion = HttpContext.Current.Request.PhysicalApplicationPath;
        //        _PathAplicacion = _PathAplicacion.Replace("\\", "/");
        //        return Path.Combine(_PathAplicacion, this.PathRelativo);
        //    }
        //}

    //    // MÉTODOS PÚBLICOS
    //    public void SubirArchivo(byte[] archivo)
    //    {
    //        File.WriteAllBytes(this.PathCompleto, archivo);
    //    }

    //    public byte[] DescargarArchivo()
    //    {
    //        return File.ReadAllBytes(this.PathCompleto);
    //    }

    //    public void EliminarArchivo()
    //    {
    //        File.Delete(this.PathCompleto);
    //    }
    //
    }
}
