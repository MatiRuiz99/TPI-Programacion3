﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Producto
    {
        public Producto()
        {
            SalesHistoryProduct = new HashSet<SalesHistory>();
            SalesHistoryUser = new HashSet<SalesHistory>();
        }

        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<SalesHistory> SalesHistoryProduct { get; set; }
        public virtual ICollection<SalesHistory> SalesHistoryUser { get; set; }
    }
}