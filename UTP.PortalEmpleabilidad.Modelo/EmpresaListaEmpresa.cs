﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
  public   class EmpresaListaEmpresa
    {
        public string id { get; set; }
        public string NombreComercial { get; set; }
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string Estado { get; set; }
        public string SectorEmpresarial { get; set; }
        public string Ofertas { get; set; }
    }
}
