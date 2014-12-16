﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;

namespace UTP.PortalEmpleabilidad.Datos
{
    public class ADUsuario : ADBase
    {
        public bool ValidarNombreDeUsuario(string nombreUsuario)
        {
            bool existe = false;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Usuario_ValidarNombreDeUsuario";
                cmd.Connection = conexion;

                conexion.Open();

                cmd.Parameters.Add(new SqlParameter("@NombreUsuario", nombreUsuario));
               
                object resultado = cmd.ExecuteScalar();

                if (resultado != null) existe = Convert.ToBoolean(resultado);

                conexion.Close();
            }

            return existe;
        }

        public DataTable ObtenerUsuariosPorTipo(string tipoUsuario)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Usuario_ObtenerPorTipo";
                cmd.Parameters.Add(new SqlParameter("@TipoUsuario", tipoUsuario));          

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }
    }
}
