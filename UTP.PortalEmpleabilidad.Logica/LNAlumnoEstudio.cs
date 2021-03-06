﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNAlumnoEstudio
    {
        ADAlumnoEstudio aed = new ADAlumnoEstudio();
        public List<AlumnoEstudio> ObtenerAlumnoEstudioPorIdAlumno(int IdAlumno)
        {
            List<AlumnoEstudio> listaAlumno = new List<AlumnoEstudio>();

            DataTable dtResultado = aed.ObtenerAlumnoEstudioPorIdAlumno(IdAlumno);

            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    AlumnoEstudio alumnoestudio = new AlumnoEstudio();

                    alumnoestudio.IdEstudio = Funciones.ToInt(dtResultado.Rows[i]["IdEstudio"]);
                    alumnoestudio.IdAlumno = Funciones.ToInt(dtResultado.Rows[i]["IdAlumno"]);
                    alumnoestudio.Institucion = Funciones.ToString(dtResultado.Rows[i]["Institucion"]);
                    alumnoestudio.Estudio = Funciones.ToString(dtResultado.Rows[i]["Estudio"]);
                    alumnoestudio.EstadoDelEstudio = Funciones.ToString(dtResultado.Rows[i]["EstadoDelEstudio"]);
                    alumnoestudio.TipoDeEstudio =Funciones.ToString( dtResultado.Rows[i]["TipoDeEstudio"]);
                    alumnoestudio.Observacion = Funciones.ToString(dtResultado.Rows[i]["Observacion"]);
                    alumnoestudio.CicloEquivalente = Funciones.ToInt(dtResultado.Rows[i]["CicloEquivalente"]);
                    alumnoestudio.FechaInicioMes = Funciones.ToInt(dtResultado.Rows[i]["FechaInicioMes"]);
                    alumnoestudio.FechaInicioAno = Funciones.ToInt(dtResultado.Rows[i]["FechaInicioAno"]);
                    alumnoestudio.FechaFinMes = Funciones.ToInt(dtResultado.Rows[i]["FechaFinMes"]);
                    alumnoestudio.FechaFinAno = Funciones.ToInt(dtResultado.Rows[i]["FechaFinAno"]);
                    alumnoestudio.DatoUTP =  Funciones.ToBoolean(dtResultado.Rows[i]["DatoUTP"]);
                    alumnoestudio.IconoTipoDeEstudio = Funciones.ToString(dtResultado.Rows[i]["IconoTipoDeEstudio"]);
                    listaAlumno.Add(alumnoestudio);
                }


            }

            return listaAlumno;
        }
        public void Insertar(AlumnoEstudio alumnoestudio)
        {
            aed.Insertar(alumnoestudio);
        }


        public AlumnoEstudio ObtenerAlumnoEstudioPorId(int IdEstudio)
        {
            DataTable dtResultado = new DataTable();
            dtResultado = aed.ObtenerAlumnoEstudioPorId(IdEstudio);
            AlumnoEstudio alumnoestudio = new AlumnoEstudio();

            if (dtResultado.Rows.Count > 0)
            {
                    alumnoestudio.IdEstudio = Funciones.ToInt(dtResultado.Rows[0]["IdEstudio"]);
                    alumnoestudio.IdAlumno = Funciones.ToInt(dtResultado.Rows[0]["IdAlumno"]);
                    alumnoestudio.Institucion = Funciones.ToString(dtResultado.Rows[0]["Institucion"]);
                    alumnoestudio.Estudio = Funciones.ToString(dtResultado.Rows[0]["Estudio"]);
                    alumnoestudio.EstadoDelEstudio = Funciones.ToString(dtResultado.Rows[0]["EstadoDelEstudio"]);
                    alumnoestudio.TipoDeEstudio = Funciones.ToString(dtResultado.Rows[0]["TipoDeEstudio"]);
                    alumnoestudio.Observacion = Funciones.ToString(dtResultado.Rows[0]["Observacion"]);
                    alumnoestudio.CicloEquivalente = Funciones.ToInt(dtResultado.Rows[0]["CicloEquivalente"]);
                    alumnoestudio.FechaInicioMes = Funciones.ToInt(dtResultado.Rows[0]["FechaInicioMes"]);
                    alumnoestudio.FechaInicioAno = Funciones.ToInt(dtResultado.Rows[0]["FechaInicioAno"]);
                    alumnoestudio.FechaFinMes = Funciones.ToInt(dtResultado.Rows[0]["FechaFinMes"]);
                    alumnoestudio.FechaFinAno = Funciones.ToInt(dtResultado.Rows[0]["FechaFinAno"]);            
            }
            return alumnoestudio;
        }

        public void Update(AlumnoEstudio alumnoestudio)
        {
            aed.Update(alumnoestudio);

        }

        public void Desactivar(int IdEstudio, string Usuario)
        {
            aed.Desactivar(IdEstudio,Usuario);

        }

    }
}
