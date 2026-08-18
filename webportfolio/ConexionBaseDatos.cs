using System;
using Oracle.ManagedDataAccess.Client;

namespace webportfolio
{
    public class ConexionBaseDatos
    {
        public string ProbarConexion()
        {
            // 1. La nueva ruta exacta de tu Wallet
            //string walletPath = @"C:\OracleWallet";

            // 2. Le indicamos al driver de Oracle dónde encontrar los certificados (.sso)
            //OracleConfiguration.WalletLocation = walletPath;

            // 3. Credenciales de acceso
            string dbUser = "ADMIN";
            string dbPassword = "NewYorkYankees76*";

            // 4. Cadena larga (mTLS) obtenida de la consola de Oracle Cloud
            string cadenaLarga = @"(description= (retry_count=20)(retry_delay=3)(address=(protocol=tcps)(port=1522)(host=adb.mx-queretaro-1.oraclecloud.com))(connect_data=(service_name=g03e389d89f047d_q022yb0tr3fmsvh3_high.adb.oraclecloud.com))(security=(ssl_server_dn_match=yes)))";

            // 5. Cadena de conexión final
            string connectionString = $"User Id={dbUser};Password={dbPassword};Data Source={cadenaLarga};";

            string mensajeResultado = "";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    mensajeResultado += "¡Conexión exitosa a Oracle Cloud usando Wallet desde C:\\OracleWallet!\n";

                    using (OracleCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT SYSDATE FROM DUAL";
                        object resultado = cmd.ExecuteScalar();
                        mensajeResultado += $"La fecha y hora en el servidor Oracle es: {resultado}";
                    }
                }
                catch (Exception ex)
                {
                    mensajeResultado = $"Error al conectar: {ex.Message}";
                }
            }

            return mensajeResultado;
        }
    }
}