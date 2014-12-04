﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using UTP.PortalEmpleabilidad.Modelo;


namespace UTP.PortalEmpleabilidad.Datos
{
    public class ADEmpresa : ADBase
    {
        ADConexion cnn = new ADConexion();
        SqlCommand cmd = new SqlCommand();

        public void Insertar(Empresa empresa)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Empresa_Insertar";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@NombreComercial", SqlDbType.VarChar, 100)).Value = empresa.NombreComercial;
            cmd.Parameters.Add(new SqlParameter("@RazonSocial", SqlDbType.VarChar, 200)).Value = empresa.RazonSocial;
            cmd.Parameters.Add(new SqlParameter("@Pais", SqlDbType.VarChar, 6)).Value = empresa.Pais;
            cmd.Parameters.Add(new SqlParameter("@IdentificadorTributario", SqlDbType.VarChar, 20)).Value = empresa.IdentificadorTributario;
            cmd.Parameters.Add(new SqlParameter("@DescripcionEmpresa", SqlDbType.VarChar, 500)).Value = empresa.DescripcionEmpresa;
            cmd.Parameters.Add(new SqlParameter("@LinkVideo", SqlDbType.VarChar, -1)).Value = empresa.LinkVideo;
            cmd.Parameters.Add(new SqlParameter("@AnoCreacion", SqlDbType.Int)).Value = empresa.AnoCreacion;
            cmd.Parameters.Add(new SqlParameter("@NumeroEmpleados", SqlDbType.VarChar, 6)).Value = empresa.NumeroEmpleados;
            cmd.Parameters.Add(new SqlParameter("@EstadoEmpresa", SqlDbType.VarChar, 6)).Value = empresa.EstadoEmpresa;
            cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial", SqlDbType.VarChar, 6)).Value = empresa.SectorEmpresarial;
            cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial2", SqlDbType.VarChar, 6)).Value = empresa.SectorEmpresarial2;
            cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial3", SqlDbType.VarChar, 6)).Value = empresa.SectorEmpresarial3;
            cmd.Parameters.Add(new SqlParameter("@CreadoPor", SqlDbType.VarChar, 50)).Value = empresa.CreadoPor;            
            cmd.ExecuteNonQuery();            
            cnn.Desconectar();            
        }


        public void Actualizar(Empresa empresa)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Empresa_Actualizar";
                cmd.Connection = conexion;

                conexion.Open();

                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", empresa.IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@NombreComercial", empresa.NombreComercial));
                cmd.Parameters.Add(new SqlParameter("@RazonSocial", empresa.RazonSocial));
                //cmd.Parameters.Add(new SqlParameter("@Pais", SqlDbType.VarChar, 6)).Value = empresa.Pais;
                //cmd.Parameters.Add(new SqlParameter("@IdentificadorTributario", SqlDbType.VarChar, 20)).Value = empresa.IdentificadorTributario;
                //cmd.Parameters.Add(new SqlParameter("@DescripcionEmpresa", SqlDbType.VarChar, 500)).Value = empresa.DescripcionEmpresa;
                //cmd.Parameters.Add(new SqlParameter("@LinkVideo", SqlDbType.VarChar, -1)).Value = empresa.LinkVideo;
                //cmd.Parameters.Add(new SqlParameter("@AnoCreacion", SqlDbType.Int)).Value = empresa.AnoCreacion;
                //cmd.Parameters.Add(new SqlParameter("@NumeroEmpleados", SqlDbType.VarChar, 6)).Value = empresa.NumeroEmpleados;
                //cmd.Parameters.Add(new SqlParameter("@EstadoEmpresa", SqlDbType.VarChar, 6)).Value = empresa.EstadoEmpresa;
                //cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial", SqlDbType.VarChar, 6)).Value = empresa.SectorEmpresarial;
                //cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial2", SqlDbType.VarChar, 6)).Value = empresa.SectorEmpresarial2;
                //cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial3", SqlDbType.VarChar, 6)).Value = empresa.SectorEmpresarial3;
                //cmd.Parameters.Add(new SqlParameter("@ModificadorPor", SqlDbType.VarChar, 50)).Value = empresa.CreadoPor;
                cmd.ExecuteNonQuery();

                conexion.Close();
            }
           
        }

        public DataTable ObtenerCabeceraPorCodigoUsuario(string usuarioEmpresa)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Empresa_ObtenerCabeceraPorCodigoUsuario";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@UsuarioEmpresa", SqlDbType.VarChar, 50)).Value = usuarioEmpresa;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }

        public DataSet ObtenerDatosEmpresaPorId(int idEmpresa)
        {
            DataSet dsResultado = new DataSet();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Empresa_ObtenerPorId";
                cmd.Connection = conexion;
                
                conexion.Open();

                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", idEmpresa));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dsResultado);

                conexion.Close();
            }
         
            return dsResultado;
        }
    }
}
