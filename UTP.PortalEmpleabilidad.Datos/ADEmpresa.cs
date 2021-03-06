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
    public class ADEmpresa : ADBase
    {
        ADConexion cnn = new ADConexion();
        SqlCommand cmd = new SqlCommand();



        public DataTable ObtenerListaValor(int idLista)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Lista_ListaValor";
                cmd.Parameters.Add(new SqlParameter("@IdLista", idLista));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }


        public void Insertar(VistaRegistroEmpresa empresa)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                SqlTransaction transaccion;
                transaccion = conexion.BeginTransaction("InsertarRegistroEmpresa");
                cmd.Connection = conexion;
                cmd.Transaction = transaccion;

                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Empresa_Insertar";

                    //Empresa:
                    cmd.Parameters.Add(new SqlParameter("@NombreComercial", SqlDbType.VarChar, 100)).Value = empresa.NombreComercial;
                    cmd.Parameters.Add(new SqlParameter("@RazonSocial", SqlDbType.VarChar, 200)).Value = empresa.RazonSocial;
                    cmd.Parameters.Add(new SqlParameter("@Pais", SqlDbType.VarChar, 6)).Value = empresa.PaisIdListaValor;
                    cmd.Parameters.Add(new SqlParameter("@IdentificadorTributario", SqlDbType.VarChar, 20)).Value = empresa.IdentificadorTributario;
                    //cmd.Parameters.Add(new SqlParameter("@DescripcionEmpresa", SqlDbType.VarChar, 500)).Value = empresa.DescripcionEmpresa;
                    //cmd.Parameters.Add(new SqlParameter("@LinkVideo", SqlDbType.VarChar, -1)).Value = empresa.LinkVideo;
                    //cmd.Parameters.Add(new SqlParameter("@AnoCreacion", SqlDbType.Int)).Value = empresa.AnoCreacion;
                    //cmd.Parameters.Add(new SqlParameter("@NumeroEmpleados", SqlDbType.VarChar, 6)).Value = empresa.NumeroEmpleadosIdListaValor;
                    cmd.Parameters.Add(new SqlParameter("@EstadoEmpresa", SqlDbType.VarChar, 6)).Value = empresa.EstadoIdListaValor;
                    cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial", SqlDbType.VarChar, 6)).Value = empresa.SectorEmpresarial1IdListaValor;
                    //cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial2", SqlDbType.VarChar, 6)).Value = empresa.SectorEmpresarial2IdListaValor;
                    //cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial3", SqlDbType.VarChar, 6)).Value = empresa.SectorEmpresarial3IdListaValor;
                    cmd.Parameters.Add(new SqlParameter("@CreadoPor", SqlDbType.VarChar, 50)).Value = empresa.CreadoPor;

                    object resultado = cmd.ExecuteScalar();
                    int idEmpresa = 0;
                    if (resultado != null) idEmpresa = Convert.ToInt32(resultado);

                    //Locacion:
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "EmpresaLocacion_Insertar";

                    //Parámetros:
                    cmd.Parameters.Add(new SqlParameter("@IdEmpresa", idEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@TipoLocacion", empresa.TipoLocacionIdListaValor));
                    cmd.Parameters.Add(new SqlParameter("@NombreLocacion", empresa.NombreLocacion));
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@TelefonoFijo", DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@Direccion", empresa.DireccionLocacion));
                    cmd.Parameters.Add(new SqlParameter("@DireccionDistrito", empresa.TextDistrito));
                    cmd.Parameters.Add(new SqlParameter("@DireccionCiudad", empresa.TextoCiudad));
                    cmd.Parameters.Add(new SqlParameter("@DireccionDepartamento", empresa.TextoDepartamento));
                    cmd.Parameters.Add(new SqlParameter("@EstadoLocacion", empresa.EstadoLocacionIdListaValor));
                    cmd.Parameters.Add(new SqlParameter("@CreadoPor", empresa.CreadoPor));

                    object resultadoLocacion = cmd.ExecuteScalar();
                    int idEmpresaLocacion = 0;
                    if (resultadoLocacion != null) idEmpresaLocacion = Convert.ToInt32(resultadoLocacion);

                    if (empresa.CuentaUsuario != "" && empresa.CuentaUsuario != null) { 
                        //Usuario:
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "EmpresaUsuario_Insertar";

                        //Parámetros:
                        cmd.Parameters.Add(new SqlParameter("@IdEmpresa", idEmpresa));
                        cmd.Parameters.Add(new SqlParameter("@Usuario", empresa.CuentaUsuario));
                        cmd.Parameters.Add(new SqlParameter("@Nombres", empresa.NombresUsuario));
                        cmd.Parameters.Add(new SqlParameter("@Apellidos", empresa.ApellidosUsuario));
                        cmd.Parameters.Add(new SqlParameter("@Sexo", DBNull.Value));
                        cmd.Parameters.Add(new SqlParameter("@TipoDocumento", empresa.TipoDocumentoIdListaValor));
                        cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", empresa.NumeroDocumento));
                        cmd.Parameters.Add(new SqlParameter("@IdEmpresaLocacion", idEmpresaLocacion));  //IdEmpresaLocacion creado
                        cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", empresa.EmailUsuario));
                        cmd.Parameters.Add(new SqlParameter("@TelefonoFijo", DBNull.Value));
                        cmd.Parameters.Add(new SqlParameter("@TelefonoAnexo", DBNull.Value));
                        cmd.Parameters.Add(new SqlParameter("@TelefonoCelular", empresa.CelularUsuario));
                        cmd.Parameters.Add(new SqlParameter("@Rol", empresa.RolIdListaValor));
                        cmd.Parameters.Add(new SqlParameter("@EstadoUsuario", empresa.EstadoUsuarioIdListaValor));
                        cmd.Parameters.Add(new SqlParameter("@Contrasena", empresa.Contrasena));
                        cmd.Parameters.Add(new SqlParameter("@CreadoPor", empresa.CreadoPor));

                        cmd.ExecuteNonQuery();
                    }
                    transaccion.Commit();

                    conexion.Close();
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    throw ex;
                }                                                                              
            }
           
        }

        public int Registrar(Empresa empresa)
        {
            int IdEmpresa = 0;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Empresa_Registrar";
                cmd.Connection = conexion;

                conexion.Open();

                cmd.Parameters.Add(new SqlParameter("@NombreComercial", empresa.NombreComercial));
                cmd.Parameters.Add(new SqlParameter("@Pais", empresa.PaisIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@DescripcionEmpresa", empresa.DescripcionEmpresa));
                cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial", empresa.SectorEmpresarial1IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial2", empresa.SectorEmpresarial2IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial3", empresa.SectorEmpresarial3IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", empresa.CreadoPor));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa",DbType.Int32)).Direction=ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                IdEmpresa = (int)cmd.Parameters["@IdEmpresa"].Value;
                conexion.Close();
            }
            return IdEmpresa;

        }

        public bool ValidarExistenciaPorNombreComercial(string nombrecomercial)
        {
            bool estado = false;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr = null;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Empresa_ValidarPorNombreComercial";
                cmd.Connection = conexion;

                conexion.Open();

                cmd.Parameters.Add(new SqlParameter("@NombreComercial", nombrecomercial));
                dr = cmd.ExecuteReader();
                estado=dr.HasRows;
               
                dr.Close();
                conexion.Close();
            }
            return estado;

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
                cmd.Parameters.Add(new SqlParameter("@Pais", empresa.PaisIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@IdentificadorTributario", empresa.IdentificadorTributario));
                cmd.Parameters.Add(new SqlParameter("@DescripcionEmpresa", empresa.DescripcionEmpresa));
                cmd.Parameters.Add(new SqlParameter("@LinkVideo", empresa.LinkVideo));
                cmd.Parameters.Add(new SqlParameter("@AnoCreacion", empresa.AnoCreacion));
                cmd.Parameters.Add(new SqlParameter("@NumeroEmpleados", empresa.NumeroEmpleadosIdListaValor));
                //cmd.Parameters.Add(new SqlParameter("@EstadoEmpresa", empresa.EstadoEmpresaIdListaValor)).Value = empresa.EstadoEmpresa;
                cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial1", empresa.SectorEmpresarial1IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial2", empresa.SectorEmpresarial2IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial3", empresa.SectorEmpresarial3IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@SitioWeb", empresa.SitioWeb));
                cmd.Parameters.Add(new SqlParameter("@ModificadoPor", empresa.ModificadoPor));
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

        public DataSet ObtenerDetalleEmpresaPorId(int idEmpresa)
        {
            DataSet dsResultado = new DataSet();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Empresa_DetalleporIdEmpresa";
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

        //public bool Empresa_Insertar_Imagen(Empresa empresa)
        //{
        //    ADConexion cnn = new ADConexion();
        //    SqlCommand cmd = new SqlCommand();

        //    try
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "Empresa_Insertar_Imagen";
        //        cmd.Connection = cnn.cn;

        //        cmd.Parameters.Add(new SqlParameter("@LogoEmpresa", SqlDbType.Binary)).Value = (empresa.LogoEmpresa == null ? new byte[] { } : empresa.LogoEmpresa);

        //        cmd.Parameters.Add(new SqlParameter("@ArchivoNombreOriginal", SqlDbType.VarChar, 100)).Value = (empresa.ArchivoNombreOriginal == null ? "" : empresa.ArchivoNombreOriginal);
        //        cmd.Parameters.Add(new SqlParameter("@ArchivoMimeType", SqlDbType.VarChar, 100)).Value = (empresa.ArchivoMimeType == null ? "" : empresa.ArchivoMimeType);
                              

        //        cnn.Conectar();
        //        cmd.ExecuteNonQuery();
        //        cmd.Parameters.Clear();
        //        cnn.Desconectar();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }


        public bool Empresa_Actualizar_Imagen(Empresa empresa)
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Empresa_Actualizar_imagen";
                cmd.Connection = cnn.cn;

                cmd.Parameters.Add(new SqlParameter("@LogoEmpresa", SqlDbType.Binary)).Value = (empresa.LogoEmpresa == null ? new byte[] { } : empresa.LogoEmpresa);

                cmd.Parameters.Add(new SqlParameter("@ArchivoNombreOriginal", SqlDbType.VarChar, 100)).Value = (empresa.ArchivoNombreOriginal == null ? "" : empresa.ArchivoNombreOriginal);
                cmd.Parameters.Add(new SqlParameter("@ArchivoMimeType", SqlDbType.VarChar, 100)).Value = (empresa.ArchivoMimeType == null ? "" : empresa.ArchivoMimeType);
                //cmd.Parameters.Add(new SqlParameter("@ImagenCambiada", SqlDbType.VarChar, 2)).Value = (empresa.LogoEmpresa == null ? "NO" : "SI");
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", SqlDbType.VarChar, 50)).Value = empresa.IdEmpresa;
                cnn.Conectar();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Desconectar();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataTable Empresa_Elegir_Imagen(int Cod)
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Empresa_Elegir_Imagen";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@IdEmpresa", SqlDbType.Int)).Value = Cod;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();
            return dt;
        }
       
    }
}
