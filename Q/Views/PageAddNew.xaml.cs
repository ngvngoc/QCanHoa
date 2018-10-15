using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Q.Helpers;
using Q.Models;
using Q.Services;
using SQLite;
using Xamarin.Forms;

namespace Q.Views
{
    public partial class PageAddNew : ContentPage
    {
        SQLiteConnection conn;
        public Tbl_shop shop;
        public PageAddNew()
        {
            InitializeComponent();
            conn = DependencyService.Get<ISQLite>().GetConnection();
        }

        void Btn_Themcuahang_Clicked(object sender, System.EventArgs e)
        {
            if(Txt_tench.Text==null || Txt_tench.Text.Length==0)
            {
                Txt_tench.Focus();
                DisplayAlert("Du Lieu Nhap", "Khong duoc de trong ten cua hang", "ok");
            }
            if (Txt_diachi.Text==null || Txt_diachi.Text.Length == 0)
            {
                Txt_diachi.Focus();
                DisplayAlert("Du Lieu Nhap", "Khong duoc de trong dia chi", "ok");
            }
            string sdt="";
            if(Txt_sdt.Text!= null && Txt_sdt.Text.Length>0)
            {
                sdt = Txt_sdt.Text;
            }
            var user = JsonConvert.DeserializeObject<Tbl_user>(Settings.Jsonuser);
            shop = new Tbl_shop();
            shop.Shop_ID = user.US_ID+ Txt_tench.Text.Replace(" ","") + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            shop.Shop_Name = Txt_tench.Text;
            shop.Shop_Adress = Txt_diachi.Text;
            shop.Shop_PhoneNumber = sdt;
            shop.US_ID = user.US_ID;
            conn.Insert(shop);
            Txt_sdt.Text = "";
            Txt_tench.Text = "";
            Txt_diachi.Text = "";
            DisplayAlert("Them cua hang", "Cua hang da them thanh cong", "ok");
            Application.Current.MainPage = new NavigationPage(new PageScanProduct(shop.Shop_ID));
        }

        void Btn_show_Clicked(object sender, System.EventArgs e)
        {
            var user = JsonConvert.DeserializeObject<Tbl_user>(Settings.Jsonuser);
            var data = conn.Table<Tbl_shop>().Where(n=>n.US_ID == user.US_ID);
            //var Vw_qc = conn.Table<VW_QC>();
            Ls_shop.ItemsSource = data;
        }
    }
}
