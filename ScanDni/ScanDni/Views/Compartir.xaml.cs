using Newtonsoft.Json;
using ScanDni.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace ScanDni.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Compartir : Popup
    {
        public Compartir()
        {
            InitializeComponent();
        }

        private async void BtExel_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                ExcelService excelService = new ExcelService();
                string fileName = $"Datos-{DateTime.Now:yyyyMMdd}.xlsx";
                string filepath = excelService.GenerateExcel(fileName);

                ExcelStructure data = new ExcelStructure
                {
                    Headers = new List<string>() { "Dni", "Apellido", "Nombre", "Género", "Fecha Nacimiento" }
                };


                List<Item> docs = await App.Database.GetAllDniAsync();
                foreach (Item item in docs)
                {
                    data.Values.Add(new List<string>() { item.Numero, item.Apellido, item.Nombre, item.GeneroDesc, item.FechaNacimiento });
                }

                excelService.InsertDataIntoSheet(filepath, "Datos", data);

                //await Launcher.OpenAsync(new OpenFileRequest()
                //{
                //    File = new ReadOnlyFile(filepath)
                //});

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Datos",
                    File = new ShareFile(filepath)
                });
            }
            catch (Exception)
            {
                Dismiss(true);
            }
        }

        private async void BtJson_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<Item> docs = await App.Database.GetAllDniAsync();
                string jsonString = JsonConvert.SerializeObject(docs);

                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "datos.json");
                File.WriteAllText(fileName, jsonString);

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Datos",
                    File = new ShareFile(fileName)
                });
            }
            catch (Exception)
            {
                Dismiss(true);
            }
        }

        private async void BtTxt_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<Item> docs = await App.Database.GetAllDniAsync();

                string datos = "";
                string formato = "Dni: {0} Apellido: {1} Nombre: {2} Género: {3} Fecha Nacimiento: {4}";
                foreach (Item dni in docs)
                {
                    string mensaje = string.Format(formato, dni.Numero, dni.Apellido, dni.Nombre, dni.GeneroDesc, dni.FechaNacimiento);

                    if (App.copia)
                    {
                        formato = mensaje + " Trámite N°: {0} Ejemplar: {1} Fecha Emisión: {2}";
                        mensaje = string.Format(formato, dni.Tramite, dni.Ejemplar, dni.FechaEmision);
                    }

                    datos += mensaje + Environment.NewLine;
                }

                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "datos.txt");
                File.WriteAllText(fileName, datos);

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Datos",
                    File = new ShareFile(fileName)
                });
            }
            catch (Exception)
            {
                Dismiss(true);
            }
        }

        protected override object GetLightDismissResult()
        {
            return false;
        }
    }
}