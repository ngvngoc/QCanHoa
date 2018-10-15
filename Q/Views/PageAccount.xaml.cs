using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Q.Helpers;
using Q.Models;
using Q.Services;
using SQLite;
using Xamarin.Forms;

namespace Q.Views
{
    public partial class PageAccount : ContentPage
    {
        public Tbl_user userstatic = new Tbl_user();
        SQLiteConnection conn;
        static string username;
        List<Tbl_Sku> sku = new List<Tbl_Sku>();
        List<Tbl_user> user = new List<Tbl_user>();
        List<Tbl_Error> errorss = new List<Tbl_Error>();
        List<Tbl_shop> shop = new List<Tbl_shop>();
        List<Tbl_CheckSku> checksku = new List<Tbl_CheckSku>();
        public PageAccount()
        {
            InitializeComponent();
            conn = DependencyService.Get<ISQLite>().GetConnection();
            if(Settings.Jsonuser != null)
            {
                var usern = JsonConvert.DeserializeObject<Tbl_user>(Settings.Jsonuser);
                username = usern.US_Username;
            }
        }

        private async void Sync_Clicked(object sender, System.EventArgs e)
        {
            string settingdate = Settings.StringDateSync;
            if(settingdate==null)
            {
                settingdate = "2018-10-10 01_00_00";
                Settings.StringDateSync = settingdate;
            }
            HttpClient client = new HttpClient();
            var kqsku = await client.GetAsync("https://sadt.nvah.net/api/Sku/SyncClient/" + settingdate + "/" + username);
            string responseContentsku = await kqsku.Content.ReadAsStringAsync();
            sku = JsonConvert.DeserializeObject<List<Tbl_Sku>>(responseContentsku);
            var skulocal = conn.Table<Tbl_Sku>();
            foreach (var item in sku)
            {
                if (skulocal.FirstOrDefault(n => n.SKU_Barcode == item.SKU_Barcode) == null)
                {
                    conn.Insert(item);
                }
                else
                {
                    conn.Update(item);
                }
            }
            var kquser = await client.GetAsync("https://sadt.nvah.net/api/User/SyncClient/" + settingdate + "/" + username);
            string responseContentuser = await kquser.Content.ReadAsStringAsync();
            user = JsonConvert.DeserializeObject<List<Tbl_user>>(responseContentuser);
            var userlocal = conn.Table<Tbl_user>();
            foreach (var item in user)
            {
                if (userlocal.FirstOrDefault(n => n.US_ID == item.US_ID) == null)
                {
                    conn.Insert(item);
                }
                else
                {
                    conn.Update(item);
                }
            }
            var kqerror = await client.GetAsync("https://sadt.nvah.net/api/Error/SyncClient/" + settingdate + "/" + username);
            string responseContenterros = await kqerror.Content.ReadAsStringAsync();
            errorss = JsonConvert.DeserializeObject<List<Tbl_Error>>(responseContenterros);
            var errorlocal = conn.Table<Tbl_Error>();
            foreach (var item in errorss)
            {
                if (errorlocal.FirstOrDefault(n=>n.ER_ID==item.ER_ID)==null)
                {
                    conn.Insert(item);
                }
                else
                {
                    conn.Update(item);
                }
            }
            var kqshop = await client.GetAsync("https://sadt.nvah.net/api/Shop/SyncClient/" + settingdate + "/" + username);
            string responseContentshop = await kqshop.Content.ReadAsStringAsync();
            shop = JsonConvert.DeserializeObject<List<Tbl_shop>>(responseContentshop);
            var shoplocal = conn.Table<Tbl_shop>();
            foreach (var item in shop)
            {
                if (shoplocal.FirstOrDefault(n => n.Shop_ID == item.Shop_ID) == null)
                {
                    conn.Insert(item);
                }
                else
                {
                    conn.Update(item);
                }
            }
            var kqchecksku = await client.GetAsync("https://sadt.nvah.net/api/CheckSku/SyncClient/" + settingdate + "/" + username);
            string responseContentchecksku = await kqchecksku.Content.ReadAsStringAsync();
            checksku = JsonConvert.DeserializeObject<List<Tbl_CheckSku>>(responseContentchecksku);
            var checkskulocal = conn.Table<Tbl_CheckSku>();
            foreach (var item in checksku)
            {
                if (checkskulocal.FirstOrDefault(n => n.CS_ID == item.CS_ID) == null)
                {
                    conn.Insert(item);
                }
                else
                {
                    conn.Update(item);
                }
            }
            //==============Up to server Start=================
            DateTime timesyncold = Convert.ToDateTime(Settings.StringDateSync.Replace("_", ":"));
            var userupserver = conn.Table<Tbl_user>().Where(n => n.US_SyncFlag == null || n.US_DeleteFlag > timesyncold || n.US_SyncFlag > timesyncold);
            var shopupserver = conn.Table<Tbl_shop>().Where(n => n.Shop_SyncFlag == null || n.Shop_DeleteFlag > timesyncold || n.Shop_SyncFlag > timesyncold);
            var checkskuupserver = conn.Table<Tbl_CheckSku>().Where(n => n.CS_SyncFlag == null || n.CS_DeleteFlag>timesyncold ||n.CS_SyncFlag>timesyncold);

            string jsonuser = JsonConvert.SerializeObject(userupserver);
            var contentuserupserver = new StringContent(jsonuser, Encoding.UTF8, "application/json");
            var kquserupserver = await client.PostAsync("https://sadt.nvah.net/api/user/SyncServer", contentuserupserver);
            if (kquserupserver.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (kquserupserver.Content != null)
                {
                    foreach (var item in userupserver)
                    {
                        item.US_SyncFlag = DateTime.Now;
                        conn.Update(item);
                    }
                }
            }

            string jsonshop = JsonConvert.SerializeObject(shopupserver);
            var contentshopupserver = new StringContent(jsonshop, Encoding.UTF8, "application/json");
            var kqshopupserver = await client.PostAsync("https://sadt.nvah.net/api/Shop/SyncServer", contentshopupserver);
            if (kqshopupserver.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (kqshopupserver.Content != null)
                {
                    foreach(var item in shopupserver)
                    {
                        item.Shop_SyncFlag = DateTime.Now;
                        conn.Update(item);
                    }
                }
            }

            string jsonchecksku = JsonConvert.SerializeObject(checkskuupserver);
            var contentcheckskuupserver = new StringContent(jsonchecksku, Encoding.UTF8, "application/json");
            var kqcheckskuupserver = await client.PostAsync("https://sadt.nvah.net/api/CheckSku/SyncServer", contentcheckskuupserver);
            if (kqcheckskuupserver.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (kqcheckskuupserver.Content != null)
                {
                    foreach (var item in checkskuupserver)
                    {
                        item.CS_SyncFlag = DateTime.Now;
                        conn.Update(item);
                    }
                }
            }
            Settings.StringDateSync = DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss");
            await DisplayAlert("SYNC", "Sync Hoan Tat", "OK");
            //==============Up to server End=================

        }

        void Logout_Clicked(object sender, System.EventArgs e)
        {
            Settings.Jsonuser = null;
            Settings.Jsonshop = null;
            Settings.Jsonluser = null;
            Settings.Jsonlshop = null;
            Application.Current.MainPage = new PageLogin();
        }
    }
}
