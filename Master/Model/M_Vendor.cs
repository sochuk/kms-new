using KMS.Helper;
using KMS.Logs.Model;
using KMS.Management.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace KMS.Master.Model
{
    public class M_Vendor
    {
        public int vendor_id { get; set; }
        public string vendor_name { get; set; }
        public string vendor_desc { get; set; }
        public string ip_address { get; set; }
        public string persosite { get; set; }
        public int server_id { get; set; }
        public string color { get; set; }
        public bool isactive { get; set; }
        public bool isdelete { get; set; }
        public int createby { get; set; }
        public int updateby { get; set; }
        public int deleteby { get; set; }
        public string createdate { get; set; }
        public string updatedate { get; set; }
        public string deletedate { get; set; }

        public enum Color
        {
            [Description("Color Primary")]
            Primary,
            [Description("Color Success")]
            Success,
            [Description("Color Danger")]
            Danger,
            [Description("Color Info")]
            Info,
            [Description("Color Warning")]
            Warning,
            [Description("Color Light")]
            Light,
            [Description("Color Dark")]
            Dark
        }

        public static string GetColorFromInt(int color)
        {
            string colour = "";
            switch (color)
            {
                case 0:
                    colour = "#004085";
                    break;
                case 1:
                    colour = "#00b67a";
                    break;
                case 2:
                    colour = "#ff420f";
                    break;
                case 3:
                    colour = "#00bbdd";
                    break;
                case 4:
                    colour = "#ffc107";
                    break;
                case 5:
                    colour = "#f0f1f1";
                    break;
                case 6:
                    colour = "#3e4b5b";
                    break;

                default:
                    colour = "#004085";
                    break;
            }

            return colour;
        }

        public static string GetColor(int color)
        {
            string colorname = "";
            switch (color)
            {
                case 0:
                    colorname =  "--primary";
                    break;
                case 1:
                    colorname = "--success";
                    break;
                case 2:
                    colorname = "--danger";
                    break;
                case 3:
                    colorname = "--info";
                    break;
                case 4:
                    colorname = "--warning";
                    break;
                case 5:
                    colorname = "--light";
                    break;
                case 6:
                    colorname = "--dark";
                    break;
            }
            return colorname;
        }

        public static string GetColorEx(int color)
        {
            string colorname = "";
            switch (color)
            {
                case 0:
                    colorname = "primary";
                    break;
                case 1:
                    colorname = "success";
                    break;
                case 2:
                    colorname = "danger";
                    break;
                case 3:
                    colorname = "info";
                    break;
                case 4:
                    colorname = "warning";
                    break;
                case 5:
                    colorname = "light";
                    break;
                case 6:
                    colorname = "dark";
                    break;
            }
            return colorname;
        }

        public static M_Vendor Insert(M_Vendor group, OracleConnection connection, OracleTransaction transaction)
        {
            DataTable dt = Database.queryScalar(@"
                INSERT INTO m_vendor(vendor_name, vendor_desc, color, server_id, createby, createdate, ip_address, persosite)
                VALUES(:vendor_name, :vendor_desc, :color, :server_id, :createby, SYSDATE, :ip_address, :persosite)", connection, transaction,
                new OracleParameter(":vendor_name", group.vendor_name),
                new OracleParameter(":vendor_desc", group.vendor_desc),
                new OracleParameter(":color", group.color),
                new OracleParameter(":createby", M_User.getUserId()),
                new OracleParameter(":ip_address", group.ip_address),
                new OracleParameter(":server_id", group.server_id),
                new OracleParameter(":persosite", group.persosite)
                );

            return group;
        }

        public static M_Vendor Update(M_Vendor group, OracleConnection connection, OracleTransaction transaction)
        {
            Database.querySQL(@"
                UPDATE m_vendor 
                SET vendor_name=:vendor_name, 
                vendor_desc=:vendor_desc,
                ip_address=:ip_address,
                color=:color,
                persosite=:persosite,
                server_id=:server_id,
                updateby=:updateby,
                updatedate=SYSDATE
                WHERE vendor_id=:vendor_id",
                connection, transaction,
                new OracleParameter(":vendor_id", group.vendor_id),
                new OracleParameter(":vendor_name", group.vendor_name),
                new OracleParameter(":vendor_desc", group.vendor_desc),
                new OracleParameter(":color", group.color),
                new OracleParameter(":updateby", M_User.getUserId()),
                new OracleParameter(":ip_address", group.ip_address),
                new OracleParameter(":server_id", group.server_id),
                new OracleParameter(":persosite", group.persosite)
                );

            return group;
        }

        public static M_Vendor Delete(M_Vendor data, OracleConnection connection, OracleTransaction transaction)
        {
            Database.querySQL(@"
                UPDATE m_vendor 
                SET 
                isdelete=:isdelete, 
                deletedate=SYSDATE,
                deleteby=:deleteby
                WHERE vendor_id=:vendor_id",
                connection, transaction,
                new OracleParameter(":isdelete", true.ToInteger()),
                new OracleParameter(":vendor_id", data.vendor_id),
                new OracleParameter(":deleteby", M_User.getUserId())
                );
            return data;
        }

        public static M_Vendor Disable(M_Vendor data, OracleConnection connection, OracleTransaction transaction)
        {
            string sql = string.Format(@"UPDATE m_vendor
                SET 
                isactive=:isactive, 
                updatedate=SYSDATE,
                updateby=:updateby
                WHERE vendor_id=:vendor_id");

            Database.querySQL(sql, connection, transaction,
                new OracleParameter(":isactive", false.ToInteger()),
                new OracleParameter(":vendor_id", data.vendor_id),
                new OracleParameter(":updateby", M_User.getUserId())
                );
            return data;
        }

        public static M_Vendor Enable(M_Vendor data, OracleConnection connection, OracleTransaction transaction)
        {
            Database.querySQL(@"
                UPDATE m_vendor 
                SET 
                isactive=:isactive, 
                updatedate=SYSDATE,
                updateby=:updateby
                WHERE vendor_id=:vendor_id",
                connection, transaction,
                new OracleParameter(":isactive", true.ToInteger()),
                new OracleParameter(":vendor_id", data.vendor_id),
                new OracleParameter(":updateby", M_User.getUserId())
                );
            return data;
        }

        public static bool IsExist(string column_name, string value)
        {
            string sql = string.Format("SELECT * FROM m_vendor where {0}='{1}'", column_name, value);
            DataTable dt = Database.getDataTable(sql);

            if (dt.Rows.Count > 0) return true;
            return false;
        }

        public static DataTable SelectAll()
        {
            string sql = string.Format(@"
                    SELECT * FROM m_vendor WHERE isdelete=0 ORDER BY isactive DESC,vendor_id DESC", M_User.getDatabaseName());
            DataTable dt = Database.getDataTable(sql);

            List<M_Vendor> data = new List<M_Vendor>();
            foreach (DataRow row in dt.Rows)
            {
                data.Add(
                    new M_Vendor()
                    {
                        vendor_id = row["vendor_id"].ToInteger(),
                        vendor_name = row["vendor_name"].ToString(),
                        vendor_desc = row["vendor_desc"].ToString(),
                        ip_address = row["ip_address"].ToString(),
                        persosite = row["persosite"].ToString(),
                        server_id = row["server_id"].ToInteger(),
                        color = row["color"].ToString(),
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

        public static DataTable SelectList()
        {
            string sql = string.Format(@"
                    SELECT * FROM m_vendor WHERE isdelete=0 AND isactive=1", M_User.getDatabaseName());
            DataTable dt = Database.getDataTable(sql);

            List<M_Vendor> data = new List<M_Vendor>();
            foreach (DataRow row in dt.Rows)
            {
                data.Add(
                    new M_Vendor()
                    {
                        vendor_id = row["vendor_id"].ToInteger(),
                        vendor_name = row["vendor_name"].ToString(),
                        vendor_desc = row["vendor_desc"].ToString(),
                        ip_address = row["ip_address"].ToString(),
                        persosite = row["persosite"].ToString(),
                        server_id = row["server_id"].ToInteger(),
                        color = row["color"].ToString(),
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

        public static DataTable SelectAllList()
        {
            string sql = string.Format(@"
                    SELECT * FROM m_vendor WHERE isdelete=0", M_User.getDatabaseName());
            DataTable dt = Database.getDataTable(sql);

            List<M_Vendor> data = new List<M_Vendor>();
            foreach (DataRow row in dt.Rows)
            {
                data.Add(
                    new M_Vendor()
                    {
                        vendor_id = row["vendor_id"].ToInteger(),
                        vendor_name = row["vendor_name"].ToString(),
                        vendor_desc = row["vendor_desc"].ToString(),
                        ip_address = row["ip_address"].ToString(),
                        persosite = row["persosite"].ToString(),
                        server_id = row["server_id"].ToInteger(),
                        color = row["color"].ToString(),
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

        public static DataTable SelectListWithContract()
        {
            string sql = string.Format(@"
                    SELECT V.*, 
                    C.CONTRACT_ID, C.CONTRACT_NAME, C.PERIOD_START, C.PERIOD_END, C.QUOTA, P.CURRENT_HIT
                    FROM M_VENDOR V
                    LEFT JOIN M_CONTRACT C ON V.VENDOR_ID=C.VENDOR_ID
                    LEFT JOIN LOG_PERSO_COUNT P ON C.CONTRACT_ID=P.CONTRACT_ID
                    WHERE V.ISDELETE=0 AND V.ISACTIVE=1 AND C.CONTRACT_ID IS NOT NULL AND C.ISACTIVE=1 AND C.ISDELETE=0",
                    M_User.getDatabaseName());

            DataTable dt = Database.getDataTable(sql);
            return dt;
        }

        public static DataTable SelectListWithParam(List<object> vendorIds, List<object> contractIds, List<object> statusIds)
        {

            string sql = string.Format(@"
                    SELECT V.*, 
                    C.CONTRACT_ID, C.CONTRACT_NAME, C.PERIOD_START, C.PERIOD_END, C.QUOTA, P.CURRENT_HIT
                    FROM M_VENDOR V 
                    LEFT JOIN M_CONTRACT C ON V.VENDOR_ID=C.VENDOR_ID 
                    LEFT JOIN LOG_PERSO_COUNT P ON C.CONTRACT_ID=P.CONTRACT_ID WHERE V.ISDELETE=0 AND 
                    V.ISACTIVE=1 AND C.CONTRACT_ID IS NOT NULL AND C.ISDELETE=0 
                    AND V.VENDOR_ID IN {0}
                    AND C.CONTRACT_ID IN {1}
                    AND C.ISACTIVE IN {2}",
                    vendorIds.ToSelectIN(),
                    contractIds.ToSelectIN(),
                    statusIds.ToSelectIN(),
                    M_User.getDatabaseName());
          

            DataTable dt = Database.getDataTable(sql);
            return dt;
        }

        public static DataTable SelectListWithContractNotCurrentDate()
        {
            string sql = string.Format(@"
                    SELECT V.*, 
                    C.CONTRACT_ID, C.CONTRACT_NAME, C.PERIOD_START, C.PERIOD_END, C.QUOTA, P.CURRENT_HIT
                    FROM M_VENDOR V
                    LEFT JOIN M_CONTRACT C ON V.VENDOR_ID=C.VENDOR_ID
                    LEFT JOIN LOG_PERSO_COUNT P ON C.CONTRACT_ID=P.CONTRACT_ID
                    WHERE V.ISDELETE=0 AND V.ISACTIVE=1 AND C.CONTRACT_ID IS NOT NULL AND C.ISDELETE=0",
                    M_User.getDatabaseName());

            DataTable dt = Database.getDataTable(sql);
            return dt;
        }

        public static DataTable SelectListWithAllContract()
        {
            string sql = string.Format(@"
                    SELECT V.*, 
                    C.CONTRACT_ID, C.CONTRACT_NAME, C.PERIOD_START, C.PERIOD_END, C.QUOTA, P.CURRENT_HIT
                    FROM M_VENDOR V
                    LEFT JOIN M_CONTRACT C ON V.VENDOR_ID=C.VENDOR_ID
                    LEFT JOIN LOG_PERSO_COUNT P ON C.CONTRACT_ID=P.CONTRACT_ID
                    WHERE V.ISDELETE=0 AND V.ISACTIVE=1 AND C.CONTRACT_ID IS NOT NULL",
                    M_User.getDatabaseName());

            DataTable dt = Database.getDataTable(sql);
            return dt;
        }

        public static DataTable SelectListWithContract(int vendor_id)
        {
            string sql = string.Format(@"
                    SELECT V.*, 
                    C.CONTRACT_ID, C.CONTRACT_NAME, C.PERIOD_START, C.PERIOD_END, C.QUOTA
                    FROM m_vendor V
                    LEFT JOIN m_contract C ON V.vendor_id=C.vendor_id
                    WHERE V.isdelete=0 AND V.isactive=1 AND C.contract_id IS NOT NULL AND C.isactive=1 AND V.vendor_id={0} ",
                    vendor_id);

            DataTable dt = Database.getDataTable(sql);
            return dt;
        }

        public static class API
        {
            public static List<object> GetAll()
            {
                string sql = string.Format(@"
                    SELECT * FROM m_vendor WHERE isdelete=0", M_User.getDatabaseName());
                DataTable dt = Database.getDataTable(sql);

                List<object> data = new List<object>();
                foreach (DataRow row in dt.Rows)
                {
                    data.Add(
                        new
                        {
                            VENDOR_ID = row["vendor_id"].ToInteger(),
                            VENDOR_NAME = row["vendor_name"].ToString(),
                            VENDOR_DESC = row["vendor_desc"].ToString(),
                            IP_ADDRESS = row["ip_address"].ToString(),
                            PERSOSITE = row["persosite"].ToString(),
                            SERVER_ID = row["server_id"].ToInteger(),
                            COLOR = row["color"].ToString(),
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

        public static DataTable SelectIn(List<object> id)
        {
            string sql = string.Format(@"
                    SELECT * FROM m_vendor WHERE vendor_id IN {0} AND isdelete=0", id.ToSelectIN());
            DataTable dt = Database.getDataTable(sql);

            List<M_Vendor> data = new List<M_Vendor>();
            foreach (DataRow row in dt.Rows)
            {
                data.Add(
                    new M_Vendor()
                    {
                        vendor_id = row["vendor_id"].ToInteger(),
                        vendor_name = row["vendor_name"].ToString(),
                        vendor_desc = row["vendor_desc"].ToString(),                        
                        ip_address = row["ip_address"].ToString(),
                        persosite = row["persosite"].ToString(),
                        server_id = row["server_id"].ToInteger(),
                        color = row["color"].ToString(),
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

        public static M_Vendor Select(int id)
        {
            string sql = string.Format(@"SELECT * FROM m_vendor WHERE vendor_id=:vendor_id AND isdelete=0");
            DataTable dt = Database.getDataTable(sql, new OracleParameter(":vendor_id", id));

            M_Vendor data = new M_Vendor();
            if (dt.Rows.Count == 1)
            {
                data.vendor_id = dt.Rows[0]["vendor_id"].ToInteger();
                data.vendor_name = dt.Rows[0]["vendor_name"].ToString();
                data.vendor_desc = dt.Rows[0]["vendor_desc"].ToString();
                data.persosite = dt.Rows[0]["persosite"].ToString();
                data.server_id = dt.Rows[0]["server_id"].ToInteger();
                data.color = dt.Rows[0]["color"].ToString();
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

        public static DataTable SelectData(int id)
        {
            string sql = string.Format(@"SELECT * FROM m_vendor WHERE vendor_id=:vendor_id AND isdelete=0");
            DataTable dt = Database.getDataTable(sql, new OracleParameter(":vendor_id", id));
            return dt;
        }

        public static List<M_Vendor> GetAll()
        {
            string sql = string.Format(@"SELECT * FROM m_vendor WHERE isdelete=0");
            DataTable dt = Database.getDataTable(sql);

            List<M_Vendor> data = new List<M_Vendor>();
            foreach (DataRow row in dt.Rows)
            {
                data.Add(
                    new M_Vendor()
                    {
                        vendor_id = row["vendor_id"].ToInteger(),
                        vendor_name = row["vendor_name"].ToString(),
                        vendor_desc = row["vendor_desc"].ToString(),                        
                        color = row["color"].ToString(),
                        server_id = row["server_id"].ToInteger(),
                        persosite = row["persosite"].ToString(),
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

            return data;
        }

        public static int GetContractId()
        {
            string sql = string.Format(@"SELECT COALESCE(CR.contract_id, 0)contract_id FROM m_vendor V
            LEFT JOIN m_contract CR ON V.vendor_id=CR.vendor_id AND CR.isactive=1 
            WHERE V.ip_address='{0}' AND V.isdelete=0", M_Vendor.GetClientIPAddress());

            DataTable dt = Database.getDataTable(sql);
            return dt.Rows[0]["contract_id"].ToInteger();
        }

        public static string GetClientIPAddress()
        {
            return Application.GetIPAddress();
        }

        public static long GetAccess(DateTime date, string uid, string ipAddress, string controlNumber, string persoSite, string manufactureCode)
        {
            int result = (int) AccessResult.NOT_FOUND;

            string sql = string.Format(@"
            SELECT COALESCE(V.VENDOR_ID,0) VENDOR_ID, COALESCE(CR.CONTRACT_ID, 0) CONTRACT_ID, 
            CR.PERIOD_START, CR.PERIOD_END, CR.QUOTA, COALESCE(LPS.CURRENT_HIT,0) CURRENT_HIT
            FROM M_VENDOR V
            LEFT JOIN M_CONTRACT CR ON V.vendor_id=CR.vendor_id AND CR.isactive=1 
            LEFT JOIN LOG_PERSO_COUNT LPS ON V.VENDOR_ID=LPS.VENDOR_ID AND CR.CONTRACT_ID=LPS.CONTRACT_ID
            WHERE V.ip_address='{0}' AND V.isdelete=0 AND V.isactive=1", ipAddress);

            DataTable dt = Database.getDataTable(sql);
            if (dt.Rows.Count == 1)
            {
                int contract_id = dt.Rows[0]["CONTRACT_ID"].ToInteger();
                int vendor_id = dt.Rows[0]["VENDOR_ID"].ToInteger();

                if (vendor_id == 0) return (int)AccessResult.VENDOR_EXCEPTION;
                if (contract_id == 0) return (int)AccessResult.CONTRACT_EXEPTION;

                DateTime period_start = Convert.ToDateTime(dt.Rows[0]["PERIOD_START"]);
                bool isStarted = DateTime.Compare(period_start, DateTime.Now).ToBoolean();
                if (isStarted)
                {
                    return (int)AccessResult.PERIOD_NOT_STARTED;
                }

                DateTime period_end = Convert.ToDateTime(dt.Rows[0]["PERIOD_END"]);
                bool isExpired = DateTime.Compare(DateTime.Now, period_end).ToBoolean();
                if (isExpired)
                {
                    return (int)AccessResult.PERIOD_EXPIRED;
                }

                int quota = dt.Rows[0]["QUOTA"].ToInteger();
                int current_hit = dt.Rows[0]["CURRENT_HIT"].ToInteger();
                bool isQuotaExceed = current_hit > quota;
                if (isQuotaExceed)
                {
                    return (int)AccessResult.QUOTA_EXCEED;
                }

                Log_Perso perso = new Log_Perso();
                perso.CARDUID = uid;
                perso.CONTROLNUMBER = controlNumber;
                perso.PERSOSITE = persoSite;
                perso.MANUFACTURERCODE = manufactureCode;
                perso.PERSO_DATE = date;
                perso.VENDOR_ID = dt.Rows[0]["VENDOR_ID"].ToInteger();
                perso.CONTRACT_ID = dt.Rows[0]["CONTRACT_ID"].ToInteger();

                using (OracleConnection connection = new OracleConnection(Database.getConnectionString("Default")))
                {
                    connection.Open();
                    connection.CreateCommand();
                    using (OracleTransaction transaction = connection.BeginTransaction())
                    {
                        perso = Log_Perso.Insert(perso, connection, transaction);                        
                        transaction.Commit();
                    }
                }
                        

                return perso.LOG_ID;

            }
            else if (dt.Rows.Count > 1)
            {
                result = (int)AccessResult.MORETHANROWS;
            }
            else
            {
                result = (int)AccessResult.NOT_FOUND;
            }
            return result;
        }

        public static Log_Perso CommitAccess(int id, string error_code)
        {
            using (OracleConnection connection = new OracleConnection(Database.getConnectionString("Default")))
            {
                connection.Open();
                connection.CreateCommand();
                Log_Perso perso = Log_Perso.Get(id);
                using (OracleTransaction transaction = connection.BeginTransaction())
                {                    
                    if(perso != null)
                    {
                        string sql = string.Format(@"UPDATE LOG_PERSO SET ERROR_CODE=:ERROR_CODE, IS_COMMIT=1 WHERE LOG_ID=:LOG_ID");
                        Database.queryScalar(
                            sql,
                            connection,
                            transaction,
                            new OracleParameter(":ERROR_CODE", error_code),
                            new OracleParameter(":LOG_ID", id));

                        Log_Perso.Hit(perso, connection, transaction);
                        transaction.Commit();
                    }
                }
                return perso;
            }            
        }

        public static DataTable SelectListWithContractByDateRange(string startDate, string endDate)
        {
            string sql = string.Format(@"
                    SELECT V.*, 
                    C.CONTRACT_ID, C.CONTRACT_NAME, C.PERIOD_START, C.PERIOD_END, C.QUOTA, P.CURRENT_HIT
                    FROM M_VENDOR V
                    LEFT JOIN M_CONTRACT C ON V.VENDOR_ID=C.VENDOR_ID
                    LEFT JOIN LOG_PERSO_COUNT P ON C.CONTRACT_ID=P.CONTRACT_ID
                    WHERE V.ISDELETE=0 AND V.ISACTIVE=1 AND C.CONTRACT_ID IS NOT NULL AND C.ISACTIVE=1 
                    AND C.ISDELETE=0 
                    AND V.UPDATEDATE BETWEEN TO_DATE('{0}', 'dd/mm/yyyy') AND TO_DATE('{1}', 'dd/mm/yyyy')",
                    startDate,endDate,
                    M_User.getDatabaseName());
            DataTable dt = Database.getDataTable(sql);
            return dt;
        }

        public enum AccessResult
        {
            NOT_FOUND = 0,
            ALLOWED = -1,
            QUOTA_EXCEED = -2,
            PERIOD_EXPIRED = -3,
            PERIOD_NOT_STARTED = -4,
            FORBIDDEN = -5,
            MORETHANROWS = -6,
            VENDOR_EXCEPTION = -7,
            CONTRACT_EXEPTION = -8,
        }
    }
}