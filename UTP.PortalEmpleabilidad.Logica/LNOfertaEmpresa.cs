﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Datos;
using System.Data;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNOfertaEmpresa
    {
        ADOferta adOferta = new ADOferta();
                
        public int Insertar(Oferta oferta)
        {
            try
            {
                if (oferta.Funciones == null) oferta.Funciones = string.Empty;
                if (oferta.Competencias == null) oferta.Competencias = string.Empty;
                if (oferta.AreaEmpresa == null) oferta.AreaEmpresa = string.Empty;
                if (oferta.TipoCargoIdListaValor == null) oferta.TipoCargoIdListaValor = string.Empty;
                if (oferta.TipoContratoIdListaValor == null) oferta.TipoContratoIdListaValor = string.Empty;
                if (oferta.Horario == null) oferta.Horario = string.Empty;

                #region Se llena una lista con todas las fases de la oferta. Se marca las que van de manera obligatoria

                LNGeneral lnGeneral = new LNGeneral();
                var listaFasesDeLaOferta = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_FASE_OFERTA).OrderBy(m => m.Peso);

                List<OfertaFase> listaOfertaFase = new List<OfertaFase>();

                foreach (var item in listaFasesDeLaOferta)
                {
                    OfertaFase nuevaOfertaFase = new OfertaFase();
                    nuevaOfertaFase.IdListaValor = item.IdListaValor;
                    nuevaOfertaFase.IdOferta = 0;

                    //Al realizar las inserciones existen 3 fases que van obligatoriamente.
                    if (item.IdListaValor == "OFFAPR" || item.IdListaValor == "OFFACV" || item.IdListaValor == "OFFAFI")
                    {
                        nuevaOfertaFase.Incluir = true;
                    }
                    else
                    {
                        nuevaOfertaFase.Incluir = false;
                    }

                    nuevaOfertaFase.CreadoPor = oferta.CreadoPor;

                    listaOfertaFase.Add(nuevaOfertaFase);
                }

                #endregion

                return adOferta.Insertar(oferta, listaOfertaFase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public bool Actualizar(Oferta oferta)
        {
            try
            {
                if (oferta.Funciones == null) oferta.Funciones = string.Empty;
                if (oferta.Competencias == null) oferta.Competencias = string.Empty;
                if (oferta.AreaEmpresa == null) oferta.AreaEmpresa = string.Empty;
                if (oferta.TipoCargoIdListaValor == null) oferta.TipoCargoIdListaValor = string.Empty;
                if (oferta.TipoContratoIdListaValor == null) oferta.TipoContratoIdListaValor = string.Empty;
                if (oferta.Horario == null) oferta.Horario = string.Empty;

                return adOferta.Actualizar(oferta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
