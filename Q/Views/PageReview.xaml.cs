using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Q.Helpers;
using Q.Models;
using Q.Services;
using SQLite;
using Xamarin.Forms;

namespace Q.Views
{
    public partial class PageReview : ContentPage
    {
        SQLiteConnection conn;
        public PageReview()
        {
            InitializeComponent();
            conn = DependencyService.Get<ISQLite>().GetConnection();
            var user = JsonConvert.DeserializeObject<Tbl_user>(Settings.Jsonuser);
            var shop = conn.Table<Tbl_shop>().Where(n => n.US_ID == user.US_ID);
            Ls_Shop.ItemsSource = shop;
        }
    }
}
