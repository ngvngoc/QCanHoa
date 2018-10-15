using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Q.Helpers;
using Q.Models;
using Q.Services;
using SQLite;
using Xamarin.Forms;

namespace Q.Views
{
    public partial class PageLogin : ContentPage
    {
        public Tbl_user userstatic = new Tbl_user();
        SQLiteConnection conn;
        string username;
        List<Tbl_Sku> sku = new List<Tbl_Sku>(); 
        public PageLogin()
        {
            InitializeComponent();

            conn = DependencyService.Get<ISQLite>().GetConnection();
            if (TableExists("Tbl_shop",conn)==false)
            {
                conn.CreateTable<Tbl_shop>();
            }
            if (TableExists("Tbl_user", conn) == false)
            {
                conn.CreateTable<Tbl_user>();
            }

            if (TableExists("Tbl_Sku", conn) == false)
            {
                conn.CreateTable<Tbl_Sku>();
            }
            if (TableExists("Tbl_Error", conn) == false)
            {
                conn.CreateTable<Tbl_Error>();
            }
            if (TableExists("Tbl_CheckSku", conn) == false)
            {
                conn.CreateTable<Tbl_CheckSku>();
            }
            if(ViewExists("VW_QC",conn)==false)
            {
                SQLite.TableMapping map = new TableMapping(typeof(SqlDbType)); // Instead of mapping to a specific table just map the whole database type
                object[] ps = new object[0];
                var tableCount = conn.Query(map, "Create View IF NOT EXISTS VW_QC as select " +
                                            "Tbl_User.US_ID,US_Name,US_Username,US_PassWord,US_Detail,US_StartDate,US_Role,US_Level,US_Status,US_SyncFlag,US_DeleteFlag," +
                                            "Tbl_Sku.SKU_Barcode,SKU_Name,SKU_Detail,SKU_Role,SKU_SyncFlag,SKU_DeleteFlag," +
                                            "Tbl_Error.ER_ID,ER_Name,ER_Parameter,ER_Property,ER_Issue,ER_NhomLoi,ER_LoaiLoi,ER_ChiTietLoi,ER_Image,ER_Status,ER_Role,ER_SyncFlag,ER_DeleteFlag," +
                                            "Tbl_Shop.Shop_ID,Shop_Name,Shop_Adress,Shop_DateVisit,Shop_Latitude,Shop_Longitude,Shop_PhoneNumber,Shop_SyncFlag,Shop_DeleteFlag," +
                                            "Tbl_CheckSku.CS_ID,CS_Warning,CS_ExpiryProduction,CS_ExpiryProductionDate,CS_Image,CS_SyncFlag,CS_DeleteFlag" +
                                            " from Tbl_User,Tbl_Sku,Tbl_Error,Tbl_Shop,Tbl_CheckSku" +
                                            " Where Tbl_User.US_ID = Tbl_Shop.US_ID and Tbl_Shop.Shop_Id = Tbl_CheckSku.Shop_Id and" +
                                            " Tbl_Sku.Sku_Barcode = Tbl_CheckSku.SKU_Barcode and Tbl_Error.ER_ID = Tbl_CheckSku.ER_ID", ps);

            }
            if (Settings.Jsonuser != null)
            {
                var user = JsonConvert.DeserializeObject<Tbl_user>(Settings.Jsonuser);
                if (user != null)
                {
                    txt_username.Text = user.US_Username.ToString();
                    username = user.US_Username;

                }
                Application.Current.MainPage = new PageDashBoard();
            }
            //var shopff = conn.Table<Tbl_shop>();
            //foreach(var item in shopff)
            //{
            //    item.US_ID = "User01";
            //    conn.Update(item);
            //}
            //GetSkuInServer("2018-10-10 01_00_00",username);
            //var Userde = conn.Table<Tbl_user>();
            //var Skude = conn.Table<Tbl_Sku>();
            //var Errde = conn.Table<Tbl_Error>();
            //var Shopde = conn.Table<Tbl_shop>();
            //var Checkde = conn.Table<Tbl_CheckSku>();
            //conn.DeleteAll<Tbl_Sku>();
            //conn.DeleteAll<Tbl_user>();
            //conn.DeleteAll<Tbl_Error>();
            //conn.DeleteAll<Tbl_shop>();
            //conn.DeleteAll<Tbl_CheckSku>();
            //Userde = conn.Table<Tbl_user>();
            //Skude = conn.Table<Tbl_Sku>();
            //Errde = conn.Table<Tbl_Error>();
            //Shopde = conn.Table<Tbl_shop>();
            //Checkde = conn.Table<Tbl_CheckSku>();
        }

        private async void GetSkuInServer(string v,string b)
        {
            HttpClient client = new HttpClient();
            var kq = await client.GetAsync("https://sadt.nvah.net/api/Sku/SyncClient/"+v+"/"+b);
            string responseContent = await kq.Content.ReadAsStringAsync();
            sku = JsonConvert.DeserializeObject<List<Tbl_Sku>>(responseContent);
            var skulocal = conn.Table<Tbl_Sku>();
            foreach (var item in sku)
            {
                if(skulocal.FirstOrDefault(n=>n.SKU_Barcode==item.SKU_Barcode)==null)
                {
                    conn.Insert(sku);
                }
            }

        }

        private async void  BtnLogin_Clicked(object sender, System.EventArgs e)
        {
            userstatic.US_Username = txt_username.Text;
            userstatic.US_PassWord = GetMD5(txt_password.Text);
            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject(userstatic);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var kq = await client.PostAsync("https://sadt.nvah.net/api/user/Login", content);
            if (kq.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (kq.Content != null)
                {
                    string responseContent = await kq.Content.ReadAsStringAsync();
                    userstatic = JsonConvert.DeserializeObject<Tbl_user>(responseContent);
                    Settings.Jsonuser = responseContent;
                    Application.Current.MainPage = new PageDashBoard();
                }
            }
            else
            {
                await DisplayAlert("LOGIN FAIL", "SAI TEN hoặc mật khẩu", "OK");
            }

        }
        public static string GetMD5(string str)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sbHash = new StringBuilder();

            foreach (byte b in bHash)
            {

                sbHash.Append(String.Format("{0:x2}", b));

            }

            return sbHash.ToString();

        }
        public static Boolean TableExists(String tableName, SQLiteConnection connection)
        {
            SQLite.TableMapping map = new TableMapping(typeof(SqlDbType)); // Instead of mapping to a specific table just map the whole database type
            object[] ps = new object[0]; // An empty parameters object since I never worked out how to use it properly! (At least I'm honest)

            Int32 tableCount = connection.Query(map, "SELECT * FROM sqlite_master WHERE type = 'table' AND name = '" + tableName + "'", ps).Count; // Executes the query from which we can count the results

            if (tableCount == 0)
            {
                return false;
            }
            else if (tableCount == 1)
            {

                return true;
            }
            else
            {
                throw new Exception("More than one table by the name of " + tableName + " exists in the database.", null);
            }

        }
        public static Boolean ViewExists(String tableName, SQLiteConnection connection)
        {
            SQLite.TableMapping map = new TableMapping(typeof(SqlDbType)); // Instead of mapping to a specific table just map the whole database type
            object[] ps = new object[0]; // An empty parameters object since I never worked out how to use it properly! (At least I'm honest)

            Int32 tableCount = connection.Query(map, "SELECT * FROM sqlite_master WHERE type = 'View' AND name = '" + tableName + "'", ps).Count; // Executes the query from which we can count the results
            if (tableCount == 0)
            {
                connection.Query(map, "DROP VIEW VW_QC", ps);
                return false;
            }
            else if (tableCount == 1)
            {
                connection.Query(map, "DROP VIEW VW_QC", ps);
                return true;
            }
            else
            {
                throw new Exception("More than one View by the name of " + tableName + " exists in the database.", null);
            }

        }
    }
}
