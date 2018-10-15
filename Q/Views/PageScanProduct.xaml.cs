using System;
using System.Collections.Generic;
using System.Linq;
using Q.Models;
using Q.Services;
using SQLite;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace Q.Views
{
    public partial class PageScanProduct : ContentPage
    {
        SQLiteConnection conn;
        public Tbl_shop shop;
        public static List<Tbl_Sku> sku = new List<Tbl_Sku>();

        public PageScanProduct(string Chid)
        {
            InitializeComponent();
            shop = new Tbl_shop();
            conn = DependencyService.Get<ISQLite>().GetConnection();
            shop = conn.Table<Tbl_shop>().SingleOrDefault(n=>n.Shop_ID==Chid);
            sku = conn.Table<Tbl_Sku>().Where(n => n.SKU_DeleteFlag == null).ToList();
            Lbl_shopsdt.Text = shop.Shop_PhoneNumber;
            Lbl_shopname.Text = shop.Shop_Name;
            Lbl_shopdiachi.Text = shop.Shop_Adress;
            Ls_sku.ItemsSource = sku;

        }

        void Home_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new PageDashBoard();
        }

        private async void Scan_Clicked(object sender, System.EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    Lbl_shopsdt.Text = result.Text;
                });
            };
        }
    }
}
