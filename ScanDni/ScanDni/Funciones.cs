using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace ScanDni
{
    public static class Funciones
    {
        #region Toast
        public static SnackBarOptions Toast(string text, bool esError, int segundos = 3)
        {
            SnackBarOptions options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Foreground = Color.White,
                    Message = text
                },
                BackgroundColor = Color.FromHex(esError ? "#E74C3C" : "#28B463"),
                Duration = TimeSpan.FromSeconds(segundos),
                //Actions = new[] { action }
            };
            return options;
        }
        #endregion
    }
}
