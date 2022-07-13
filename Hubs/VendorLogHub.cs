using KMS.Helper;
using KMS.Logs.Model;
using KMS.Management.Model;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace KMS.Hubs
{
    public class VendorLogHub : Hub
    {
        private HubUser user = new HubUser();
        private int hourlyInternal = 1;

        public object Join(string IPAddress, string UserAgent)
        {
            user = new HubUser();
            user.connection_id = Context.ConnectionId;
            user.user_id = Crypto.Encode64Byte(M_User.getUserId().ToString());
            user.username = Crypto.Encode64Byte(M_User.getUsername());
            user.fullname = M_User.getFullname();
            user.company_name = HttpContext.Current.User.Identity.Get_CompanyName();
            user.gender = HttpContext.Current.User.Identity.Get_Gender();
            user.photo = M_User.getPhoto();
            user.local_ipaddress = M_User.getLocalIP();
            user.remote_ipaddress = IPAddress;
            user.user_agent = UserAgent;

            if (user != null)
            {
                Clients.Caller.user_id = user.user_id;
                Clients.Caller.connection_id = user.connection_id;
                Clients.Caller.username = user.username;
                Clients.Caller.fullname = user.fullname;
                Clients.Caller.company_name = user.company_name;
                Clients.Caller.gender = user.gender;
                Clients.Caller.photo = user.photo;
                Clients.Caller.local_ipaddress = user.local_ipaddress;
                Clients.Caller.remote_ipaddress = user.remote_ipaddress;
                Clients.Caller.user_agent = user.user_agent;
            }

            return new
            {
                message = "Connected to server"
            };
        }

        public object GetHourlyData(string date, List<object> vendorIds, List<object> contractIds, List<object> statusIds)
        {
            var dateParam = null == date ? DateTime.Now : Convert.ToDateTime(date);
            return Vendor_Log.GetHourlyInterval(hourlyInternal, dateParam, vendorIds, contractIds, statusIds);
        }

        public object GetDailyData(string date, List<object> vendorIds, List<object> contractIds, List<object> statusIds)
        {

            var dateParam = null == date ? DateTime.Now : Convert.ToDateTime(date);
            return Vendor_Log.GetDailyInterval(1, dateParam, vendorIds, contractIds, statusIds);
        }

        public object GetWeeklyData(string date, List<object> vendorIds, List<object> contractIds, List<object> statusIds)
        {
            var dateParam = null == date ? DateTime.Now : Convert.ToDateTime(date);
            return Vendor_Log.GetWeeklyInterval(1, dateParam, vendorIds, contractIds, statusIds);
        }

        public object GetMonthlyData(string date, List<object> vendorIds, List<object> contractIds, List<object> statusIds)
        {
            var dateParam = (null == date) ? DateTime.Now : Convert.ToDateTime(date);
            var result = Vendor_Log.GetMonthlyInterval(dateParam, vendorIds, contractIds, statusIds);
            return result;
        }

        public object GetSemesterData(string date, List<object> vendorIds, List<object> contractIds, List<object> statusIds)
        {
            var dateParam = (null == date) ? DateTime.Now : Convert.ToDateTime(date);
            var result = Vendor_Log.GetSemesterInterval(dateParam, vendorIds, contractIds, statusIds);
            return result;
        }

        public object GetDailySummaryData()
        {
            int vendorId = HttpContext.Current.User.Identity.Get_VendorID().ToInteger();
            return Vendor_Log.GetDailySummary(vendorId);
        }        

        public void GetSummaryData()
        {
            int vendorId = HttpContext.Current.User.Identity.Get_VendorID().ToInteger();
            Clients.User(M_User.getUsername()).updateSummary(Vendor_Log.GetSummaryCounter(vendorId));
        }

        public void GetContractInfoData()
        {
            Clients.User(M_User.getUsername()).getContractInfo(Vendor_Log.GetContractInfo());
        }

        public void ReloadCounter()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string username = M_User.getUsername();
                int vendorId = HttpContext.Current.User.Identity.Get_VendorID().ToInteger();

                Clients.User(username).updateSummary(Vendor_Log.GetSummaryCounter(vendorId));

                DataTable hour = Vendor_Log.GetHourlyInterval(hourlyInternal, DateTime.Now, vendorId);
                Clients.User(username).updateHourly(hour);

                DataTable dailysum = Vendor_Log.GetDailySummary(vendorId);
                Clients.User(username).updateDailySummary(dailysum);
            }

            //System.Threading.Thread.Sleep(1000);
        }

        public void UpdateTotalCounter(int vendorId = 0)
        {
            Vendor_Log.UpdateTotalCounter(vendorId);
        }

        public void ReloadAll()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string username = M_User.getUsername();
                int vendorId = HttpContext.Current.User.Identity.Get_VendorID().ToInteger();
                //Task.Run(() =>
                //{
                //    DataTable summary = Vendor_Log.GetSummary(vendorId);
                //    Clients.User(username).updateSummary(summary);
                //});

                //Task.Run(() =>
                //{
                //    DataTable hour = Vendor_Log.GetHourlyInterval(hourlyInternal, DateTime.Now, vendorId);
                //    Clients.User(username).updateHourly(hour);
                //});

                //Task.Run(() =>
                //{
                //    DataTable dailysum = Vendor_Log.GetDailySummary(vendorId);
                //    Clients.User(username).updateDailySummary(dailysum);
                //});

                //Task.Run(() =>
                //{
                //    DataTable daily = Vendor_Log.GetDailyInterval(1, DateTime.Now, vendorId);
                //    Clients.User(username).updateDaily(daily);
                //});

                //Task.Run(() =>
                //{
                //    DataTable monthly = Vendor_Log.GetMonthlyInterval(DateTime.Now, vendorId);
                //    Clients.User(username).updateMonthly(monthly);
                //});

                DataTable summary = Vendor_Log.GetSummary(vendorId);
                Clients.User(username).updateSummary(summary);

                DataTable hour = Vendor_Log.GetHourlyInterval(hourlyInternal, DateTime.Now, vendorId);
                Clients.User(username).updateHourly(hour);

                DataTable dailysum = Vendor_Log.GetDailySummary(vendorId);
                Clients.User(username).updateDailySummary(dailysum);

                DataTable daily = Vendor_Log.GetDailyInterval(1, DateTime.Now, vendorId);
                Clients.User(username).updateDaily(daily);

                DataTable weekly = Vendor_Log.GetWeeklyInterval(1, DateTime.Now, vendorId);
                Clients.User(username).updateWeekly(weekly);

                DataTable monthly = Vendor_Log.GetMonthlyInterval(DateTime.Now, vendorId);
                Clients.User(username).updateMonthly(monthly);

                DataTable semester = Vendor_Log.GetSemesterInterval(DateTime.Now, vendorId);
                Clients.User(username).updateSemester(semester);
            }

            //System.Threading.Thread.Sleep(1000);
        }

    }
}