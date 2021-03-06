﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTPPrototipo.Models.ViewModels.Empresa
{
    public class EmpresaVista
    {
          public int IdEmpresa { get; set; }
        
        [Required(ErrorMessage="Este campo es obligatorio.")]
        [StringLength(100)]
        public string NombreComercial { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(200)]
        public string RazonSocial { get; set; }
        public ListaValor Pais { get; set; }
        
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(11, MinimumLength=11, ErrorMessage="Este campo sólo acepta 11 números.")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Este campo sólo acepta números.")]
        public string IdentificadorTributario { get; set; }

        [StringLength(500, ErrorMessage="Este campo sólo acepta 500 caracteres.")]
        [DataType(DataType.MultilineText)]
        public string DescripcionEmpresa { get; set; }
        
        [Obsolete]
        public string PresentacionEmpresa { get; set; }
        public string LinkVideo { get; set; }
        public int AnoCreacion { get; set; }
        public ListaValor NumeroEmpleados { get; set; }        
        public ListaValor EstadoEmpresa { get; set; }
        public ListaValor SectorEmpresarial { get; set; }
        public ListaValor SectorEmpresarial2 { get; set; }
        public ListaValor SectorEmpresarial3 { get; set; }
        public string CreadoPor { get; set; }
        public string UsuarioEC { get; set; }

        public List<EmpresaLocacion> Locaciones { get; set; }
        public List<EmpresaUsuario> Usuarios { get; set; }

        public string PaisIdListaValor { get; set; }
        public string NumeroEmpleadosIdListaValor { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]      
        public string SectorEmpresarial1IdListaValor { get; set; }
        public string SectorEmpresarial2IdListaValor { get; set; }
        public string SectorEmpresarial3IdListaValor { get; set; }
        public string ModificadoPor { get; set; }
        public string EstadoIdListaValor { get; set; }

        public EmpresaVista()
        {
            Pais = new ListaValor();
            SectorEmpresarial = new ListaValor();
            SectorEmpresarial2 = new ListaValor();
            SectorEmpresarial3 = new ListaValor();
            NumeroEmpleados = new ListaValor();
            EstadoEmpresa = new ListaValor();
            Locaciones = new List<EmpresaLocacion>();
            Usuarios = new List<EmpresaUsuario>();
            PaisIdListaValor = string.Empty;
            NumeroEmpleadosIdListaValor = string.Empty;
            SectorEmpresarial2IdListaValor = string.Empty;
            SectorEmpresarial3IdListaValor = string.Empty;
        }

            public byte[] LogoEmpresa { get; set; }
            public HttpPostedFileBase LogoEmpresaHtml { get; set; }
            public string ArchivoNombreOriginal { get; set; }
            public string ArchivoMimeType { get; set; }

          
          
    }
}