using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("FA5Regular.otf", Alias = "FontAwesomeRegular")]
[assembly: ExportFont("FA5Solid.otf", Alias = "FontAwesomeSolid")]

namespace ScanDni
{
    public partial class App : Application
    {
        
        public static bool vibrar;
        public static bool guardado;
        public static bool copia;

        public static Database database;
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database =
                        new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "docs.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
