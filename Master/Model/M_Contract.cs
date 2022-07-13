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
    public class M_Contract
    {
        public int contract_id { get; set; }
        public string contract_name { get; set; }
        public string contract_desc { get; set; }
        public string attachment { get; set; }
        public DateTime period_start { get; set; }
        public DateTime period_end { get; set; }
        public int quota { get; set; }
        public int vendor_id { get; set; }

        public bool isactive { get; set; }
        public bool isdelete { get; set; }
        public int createby { get; set; }
        public int updateby { get; set; }
        public int deleteby { get; set; }
        public string createdate { get; set; }
        public string updatedate { get; set; }
        public string deletedate { get; set; }

        public static M_Contract Insert(M_Contract group, OracleConnection connection, OracleTransaction transaction)
        {
            Database.querySQL("UPDATE m_contract SET isactive=0 WHERE vendor_id=:vendor_id", 
                connection, transaction,
                new OracleParameter(":vendor_id", group.vendor_id)
                );

            DataTable dt = Database.queryScalar(@"
                INSERT INTO m_contract(contract_name, contract_desc, period_start, period_end, quota, vendor_id, attachment, createby, createdate)
                VALUES(:contract_name, :contract_desc, :period_start, :period_end, :quota, :vendor_id, :attachment, :createby, SYSDATE)", 
                connection, transaction,
                new OracleParameter(":contract_name", group.contract_name),
                new OracleParameter(":contract_desc", group.contract_desc),
                new OracleParameter(":period_start", group.period_start),
                new OracleParameter(":period_end", group.period_end),
                new OracleParameter(":quota", group.quota),
                new OracleParameter(":vendor_id", group.vendor_id),
                new OracleParameter(":attachment", group.attachment),
                new OracleParameter(":createby", M_User.getUserId())
                );

            return group;
        }

        public static M_Contract Update(M_Contract group, OracleConnection connection, OracleTransaction transaction)
        {
            Database.querySQL(@"
                UPDATE m_contract 
                SET contract_name=:contract_name, 
                contract_desc=:contract_desc,
                period_start=:period_start,
                period_end=:period_end,
                quota=:quota,
                vendor_id=:vendor_id,
                attachment=:attachment,
                updateby=:updateby,
                updatedate=SYSDATE
                WHERE contract_id=:contract_id",
                connection, transaction,
                new OracleParameter(":contract_id", group.contract_id),
                new OracleParameter(":contract_name", group.contract_name),
                new OracleParameter(":contract_desc", group.contract_desc),
                new OracleParameter(":period_start", group.period_start),
                new OracleParameter(":period_end", group.period_end),
                new OracleParameter(":quota", group.quota),
                new OracleParameter(":vendor_id", group.vendor_id),
                new OracleParameter(":attachment", group.attachment),
                new OracleParameter(":updateby", M_User.getUserId())
                );

            return group;
        }

        public static M_Contract Delete(M_Contract data, OracleConnection connection, OracleTransaction transaction)
        {
            Database.querySQL(@"
                UPDATE m_contract 
                SET 
                isdelete=:isdelete, 
                deletedate=SYSDATE,
                deleteby=:deleteby
                WHERE contract_id=:contract_id",
                connection, transaction,
                new OracleParameter(":isdelete", true.ToInteger()),
                new OracleParameter(":contract_id", data.contract_id),
                new OracleParameter(":deleteby", M_User.getUserId())
                );
            return data;
        }

        public static M_Contract Disable(M_Contract data, OracleConnection connection, OracleTransaction transaction)
        {
            string sql = string.Format(@"UPDATE m_contract
                SET 
                isactive=:isactive, 
                updatedate=SYSDATE,
                updateby=:updateby
                WHERE contract_id=:contract_id");

            Database.querySQL(sql, connection, transaction,
                new OracleParameter(":isactive", false.ToInteger()),
                new OracleParameter(":contract_id", data.contract_id),
                new OracleParameter(":updateby", M_User.getUserId())
                );
            return data;
        }

        public static M_Contract Enable(M_Contract data, OracleConnection connection, OracleTransaction transaction)
        {
            Database.querySQL("UPDATE m_contract SET isactive=0 WHERE vendor_id=:vendor_id",
                connection, transaction,
                new OracleParameter(":vendor_id", data.vendor_id)
                );

            Database.querySQL(@"
                UPDATE m_contract 
                SET 
                isactive=:isactive, 
                updatedate=SYSDATE,
                updateby=:updateby
                WHERE contract_id=:contract_id",
                connection, transaction,
                new OracleParameter(":isactive", true.ToInteger()),
                new OracleParameter(":contract_id", data.contract_id),
                new OracleParameter(":updateby", M_User.getUserId())
                );
            return data;
        }

        public static bool IsExist(M_Contract data)
        {
            DataTable dt = Database.getDataTable("SELECT contract_name FROM m_contract where contract_name=:contract_name",
                new OracleParameter(":contract_name", data.contract_name));

            if (dt.Rows.Count > 0) return true;

            return false;
        }

        public static DataTable SelectAll()
        {
            string sql = string.Format(@"
                    SELECT * FROM m_contract WHERE isdelete=0 ORDER BY isactive DESC,contract_id DESC", M_User.getDatabaseName());
            DataTable dt = Database.getDataTable(sql);

            List<M_Contract> data = new List<M_Contract>();
            foreach (DataRow row in dt.Rows)
            {
                data.Add(
                    new M_Contract()
                    {
                        contract_id = row["contract_id"].ToInteger(),
                        contract_name = row["contract_name"].ToString(),
                        contract_desc = row["contract_desc"].ToString(),

                        period_start = Convert.ToDateTime( row["period_start"].ToString() ),
                        period_end = Convert.ToDateTime( row["period_end"].ToString() ),
                        quota = row["quota"].ToInteger(),
                        attachment= row["isactive"].ToString(),
                        vendor_id = row["vendor_id"].ToInteger(),

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
                    SELECT * FROM m_contract WHERE isdelete=0 ORDER BY isactive DESC,contract_id DESC", M_User.getDatabaseName());
                DataTable dt = Database.getDataTable(sql);

                List<object> data = new List<object>();
                foreach (DataRow row in dt.Rows)
                {
                    data.Add(
                        new
                        {
                            CONTRACT_ID = row["contract_id"].ToInteger(),
                            CONTRACT_NAME = row["contract_name"].ToString(),
                            CONTRACT_DESC = row["contract_desc"].ToString(),

                            PERIOD_START = Convert.ToDateTime(row["period_start"].ToString()),
                            PERIOD_END = Convert.ToDateTime(row["period_end"].ToString()),
                            QUOTA = row["quota"].ToInteger(),
                            ATTACHMENT = row["isactive"].ToString(),
                            VENDOR_ID = row["vendor_id"].ToInteger(),

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
                    SELECT * FROM m_contract WHERE isdelete=0 AND isactive=1 ", M_User.getDatabaseName());
            DataTable dt = Database.getDataTable(sql);

            List<M_Contract> data = new List<M_Contract>();
            foreach (DataRow row in dt.Rows)
            {
                data.Add(
                    new M_Contract()
                    {
                        contract_id = row["contract_id"].ToInteger(),
                        contract_name = row["contract_name"].ToString(),
                        contract_desc = row["contract_desc"].ToString(),

                        period_start = Convert.ToDateTime(row["period_start"].ToString()),
                        period_end = Convert.ToDateTime(row["period_end"].ToString()),
                        quota = row["quota"].ToInteger(),
                        attachment = row["isactive"].ToString(),
                        vendor_id = row["vendor_id"].ToInteger(),

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
                    SELECT * FROM m_contract WHERE isdelete=0 ", M_User.getDatabaseName());
            DataTable dt = Database.getDataTable(sql);

            List<M_Contract> data = new List<M_Contract>();
            foreach (DataRow row in dt.Rows)
            {
                data.Add(
                    new M_Contract()
                    {
                        contract_id = row["contract_id"].ToInteger(),
                        contract_name = row["contract_name"].ToString(),
                        contract_desc = row["contract_desc"].ToString(),

                        period_start = Convert.ToDateTime(row["period_start"].ToString()),
                        period_end = Convert.ToDateTime(row["period_end"].ToString()),
                        quota = row["quota"].ToInteger(),
                        attachment = row["isactive"].ToString(),
                        vendor_id = row["vendor_id"].ToInteger(),

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

        public static M_Contract Select(int contract_id)
        {
            string sql = string.Format(@"
                    SELECT * FROM m_contract WHERE contract_id=:contract_id AND isdelete=0", M_User.getDatabaseName());
            DataTable dt = Database.getDataTable(sql, new OracleParameter(":contract_id", contract_id));

            M_Contract data = new M_Contract();
            if (dt.Rows.Count == 1)
            {
                data.contract_id = dt.Rows[0]["contract_id"].ToInteger();
                data.contract_name = dt.Rows[0]["contract_name"].ToString();
                data.contract_desc = dt.Rows[0]["contract_desc"].ToString();

                data.period_start = Convert.ToDateTime(dt.Rows[0]["period_start"].ToString());
                data.period_end = Convert.ToDateTime(dt.Rows[0]["period_end"].ToString());
                data.quota = dt.Rows[0]["quota"].ToInteger();
                data.attachment = dt.Rows[0]["isactive"].ToString();
                data.vendor_id = dt.Rows[0]["vendor_id"].ToInteger();

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

        public static M_Contract Select(string column, string value)
        {
            string sql = string.Format(@"
                    SELECT * FROM m_contract WHERE {0}='{1}' AND isdelete=0 AND isactive=1 AND (period_end + interval '23' hour + interval '59' minute + interval '59' second) >= SYSDATE", column, value);
            DataTable dt = Database.getDataTable(sql);

            M_Contract data = null;
            if (dt.Rows.Count == 1)
            {
                data = new M_Contract();
                data.contract_id = dt.Rows[0]["contract_id"].ToInteger();
                data.contract_name = dt.Rows[0]["contract_name"].ToString();
                data.contract_desc = dt.Rows[0]["contract_desc"].ToString();

                data.period_start = Convert.ToDateTime(dt.Rows[0]["period_start"].ToString());
                data.period_end = Convert.ToDateTime(dt.Rows[0]["period_end"].ToString());
                data.quota = dt.Rows[0]["quota"].ToInteger();
                data.attachment = dt.Rows[0]["isactive"].ToString();
                data.vendor_id = dt.Rows[0]["vendor_id"].ToInteger();

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
                    SELECT * FROM m_contract WHERE contract_id IN {0} AND isdelete=0", id.ToSelectIN());
            DataTable dt = Database.getDataTable(sql);

            List<M_Contract> data = new List<M_Contract>();
            foreach (DataRow row in dt.Rows)
            {
                data.Add(
                    new M_Contract()
                    {
                        contract_id = row["contract_id"].ToInteger(),
                        contract_name = row["contract_name"].ToString(),
                        contract_desc = row["contract_desc"].ToString(),

                        period_start = Convert.ToDateTime(row["period_start"].ToString()),
                        period_end = Convert.ToDateTime(row["period_end"].ToString()),
                        quota = row["quota"].ToInteger(),
                        attachment = row["isactive"].ToString(),
                        vendor_id = row["vendor_id"].ToInteger(),

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