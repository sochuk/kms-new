using KMS.Helper;
using KMS.Management.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KMS.Master.Model
{
    public class M_Server
    {
        public int server_id { get; set; }
        public string server_name { get; set; }
        public string server_desc { get; set; }
        public string ip_address { get; set; }
        public string log_path { get; set; }

        public bool isactive { get; set; }
        public bool isdelete { get; set; }
        public int createby { get; set; }
        public int updateby { get; set; }
        public int deleteby { get; set; }
        public string createdate { get; set; }
        public string updatedate { get; set; }
        public string deletedate { get; set; }

        public static M_Server Insert(M_Server group, OracleConnection connection, OracleTransaction transaction)
        {
            DataTable dt = Database.queryScalar(@"
                INSERT INTO m_server(server_name, server_desc, ip_address, log_path, createby, createdate)
                VALUES(:server_name, :server_desc, :ip_address, :log_path, :createby, SYSDATE)", connection, transaction,
                new OracleParameter(":server_name", group.server_name),
                new OracleParameter(":server_desc", group.server_desc),
                new OracleParameter(":ip_address", group.ip_address),
                new OracleParameter(":log_path", group.log_path),
                new OracleParameter(":createby", M_User.getUserId())
                );

            return group;
        }

        public static M_Server Update(M_Server group, OracleConnection connection, OracleTransaction transaction)
        {
            Database.querySQL(@"
                UPDATE m_server 
                SET server_name=:server_name, 
                server_desc=:server_desc,
                ip_address=:ip_address,
                log_path=:log_path,
                updateby=:updateby,
                updatedate=SYSDATE
                WHERE server_id=:server_id",
                connection, transaction,
                new OracleParameter(":server_id", group.server_id),
                new OracleParameter(":server_name", group.server_name),
                new OracleParameter(":server_desc", group.server_desc),
                new OracleParameter(":ip_address", group.ip_address),
                new OracleParameter(":log_path", group.log_path),
                new OracleParameter(":updateby", M_User.getUserId())
                );

            return group;
        }

        public static M_Server Delete(M_Server data, OracleConnection connection, OracleTransaction transaction)
        {
            Database.querySQL(@"
                UPDATE m_server 
                SET 
                isdelete=:isdelete, 
                deletedate=SYSDATE,
                deleteby=:deleteby
                WHERE server_id=:server_id",
                connection, transaction,
                new OracleParameter(":isdelete", true.ToInteger()),
                new OracleParameter(":server_id", data.server_id),
                new OracleParameter(":deleteby", M_User.getUserId())
                );
            return data;
        }

        public static M_Server Disable(M_Server data, OracleConnection connection, OracleTransaction transaction)
        {
            string sql = string.Format(@"UPDATE m_server
                SET 
                isactive=:isactive, 
                updatedate=SYSDATE,
                updateby=:updateby
                WHERE server_id=:server_id");

            Database.querySQL(sql, connection, transaction,
                new OracleParameter(":isactive", false.ToInteger()),
                new OracleParameter(":server_id", data.server_id),
                new OracleParameter(":updateby", M_User.getUserId())
                );
            return data;
        }

        public static M_Server Enable(M_Server data, OracleConnection connection, OracleTransaction transaction)
        {
            Database.querySQL(@"
                UPDATE m_server 
                SET 
                isactive=:isactive, 
                updatedate=SYSDATE,
                updateby=:updateby
                WHERE server_id=:server_id",
                connection, transaction,
                new OracleParameter(":isactive", true.ToInteger()),
                new OracleParameter(":server_id", data.server_id),
                new OracleParameter(":updateby", M_User.getUserId())
                );
            return data;
        }

        public static bool IsExist(string column_name, string value)
        {
            string sql = string.Format("SELECT * FROM m_server where {0}='{1}'", column_name, value);
            DataTable dt = Database.getDataTable(sql);

            if (dt.Rows.Count > 0) return true;
            return false;
        }

        public static DataTable SelectAll()
        {
            string sql = string.Format(@"
                    SELECT * FROM m_server WHERE isdelete=0", M_User.getDatabaseName());
            DataTable dt = Database.getDataTable(sql);

            List<M_Server> data = new List<M_Server>();
            foreach (DataRow row in dt.Rows)
            {
                data.Add(
                    new M_Server()
                    {
                        server_id = row["server_id"].ToInteger(),
                        server_name = row["server_name"].ToString(),
                        server_desc = row["server_desc"].ToString(),
                        ip_address = row["ip_address"].ToString(),
                        log_path = row["log_path"].ToString(),

                        isactive = row["isactive"].ToBoolean(),
                        isdelete = row["isdelete"].ToBoolean(),
                        createby = row["createby"].ToInteger(),
                        updateby = row["updateby"].ToInteger(),
                        deleteby = row["deleteby"].ToInteger(),
                        createdate = row["createdate"].ToString(),
                        updatedate = row["updatedate"].ToString(),
                        deletedate = row["deletedate"].ToString(),
                    }
                );
            }

            DataTable mdt = data.ToDataTable();

            return mdt;
        }

        public static class API
        {
            public static List<object> GetAll()
            {
                string sql = string.Format(@"
                    SELECT * FROM m_server WHERE isdelete=0", M_User.getDatabaseName());
                DataTable dt = Database.getDataTable(sql);

                List<object> data = new List<object>();
                foreach (DataRow row in dt.Rows)
                {
                    data.Add(
                        new
                        {
                            SERVER_ID = row["server_id"].ToInteger(),
                            SERVER_NAME = row["server_name"].ToString(),
                            SERVER_DESC = row["server_desc"].ToString(),
                            IP_ADDRESS = row["ip_address"].ToString(),
                            LOG_PATH = row["log_path"].ToString(),
                            ISACTIVE = row["isactive"].ToBoolean(),
                            ISDELETE = row["isdelete"].ToBoolean(),
                            CREATEBY = row["createby"].ToInteger(),
                            UPDATEBY = row["updateby"].ToInteger(),
                            DELETEBY = row["deleteby"].ToInteger(),
                            CREATEDATE = row["createdate"].ToString(),
                            UPDATEDATE = row["updatedate"].ToString(),
                            DELETEDATE = row["deletedate"].ToString(),
                        }
                    );
                }

                return data;
            }
        }        

        public static DataTable SelectList()
        {
            string sql = string.Format(@"
                    SELECT * FROM m_server WHERE isdelete=0 AND isactive=1 ORDER BY server_name", M_User.getDatabaseName());
            DataTable dt = Database.getDataTable(sql);

            List<M_Server> data = new List<M_Server>();
            foreach (DataRow row in dt.Rows)
            {
                data.Add(
                    new M_Server()
                    {
                        server_id = row["server_id"].ToInteger(),
                        server_name = row["server_name"].ToString(),
                        server_desc = row["server_desc"].ToString(),
                        ip_address = row["ip_address"].ToString(),
                        log_path = row["log_path"].ToString(),

                        isactive = row["isactive"].ToBoolean(),
                        isdelete = row["isdelete"].ToBoolean(),
                        createby = row["createby"].ToInteger(),
                        updateby = row["updateby"].ToInteger(),
                        deleteby = row["deleteby"].ToInteger(),
                        createdate = row["createdate"].ToString(),
                        updatedate = row["updatedate"].ToString(),
                        deletedate = row["deletedate"].ToString(),
                    }
                );
            }

            DataTable mdt = data.ToDataTable();

            return mdt;
        }

        public static M_Server Select(int id)
        {
            string sql = string.Format(@"
                    SELECT * FROM m_server WHERE server_id=:server_id AND isdelete=0");
            DataTable dt = Database.getDataTable(sql, new OracleParameter(":server_id", id));

            M_Server data = new M_Server();
            if (dt.Rows.Count == 1)
            {
                data.server_id = dt.Rows[0]["server_id"].ToInteger();
                data.server_name = dt.Rows[0]["server_name"].ToString();
                data.server_desc = dt.Rows[0]["server_desc"].ToString();
                data.ip_address = dt.Rows[0]["ip_address"].ToString();
                data.log_path = dt.Rows[0]["log_path"].ToString();

                data.isactive = dt.Rows[0]["isactive"].ToBoolean();
                data.isdelete = dt.Rows[0]["isdelete"].ToBoolean();
                data.createby = dt.Rows[0]["createby"].ToInteger();
                data.updateby = dt.Rows[0]["updateby"].ToInteger();
                data.deleteby = dt.Rows[0]["deleteby"].ToInteger();
                data.createdate = dt.Rows[0]["createdate"].ToString();
                data.updatedate = dt.Rows[0]["updatedate"].ToString();
                data.deletedate = dt.Rows[0]["deletedate"].ToString();

            }          

            return data;
        }

        public static DataTable SelectIn(List<object> id)
        {
            string sql = string.Format(@"
                    SELECT * FROM m_server WHERE server_id IN {0} AND isdelete=0", id.ToSelectIN());
            DataTable dt = Database.getDataTable(sql);

            List<M_Server> data = new List<M_Server>();
            foreach (DataRow row in dt.Rows)
            {
                data.Add(
                    new M_Server()
                    {
                        server_id = row["server_id"].ToInteger(),
                        server_name = row["server_name"].ToString(),
                        server_desc = row["server_desc"].ToString(),
                        ip_address = row["ip_address"].ToString(),
                        log_path = row["log_path"].ToString(),

                        isactive = row["isactive"].ToBoolean(),
                        isdelete = row["isdelete"].ToBoolean(),
                        createby = row["createby"].ToInteger(),
                        updateby = row["updateby"].ToInteger(),
                        deleteby = row["deleteby"].ToInteger(),
                        createdate = row["createdate"].ToString(),
                        updatedate = row["updatedate"].ToString(),
                        deletedate = row["deletedate"].ToString(),
                    }
                );
            }

            DataTable mdt = data.ToDataTable();

            return mdt;
        }
    }
}