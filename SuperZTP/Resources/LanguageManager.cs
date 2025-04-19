using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Reflection;
using System.Resources;
using System.Windows.Markup;

namespace SuperZTP.Resources
{
    public static class LanguageManager
    {
        private static bool _isMetadataOverridden = false;
        public static void ChangeLanguage(string cultureName)
        {
            var culture = new CultureInfo(cultureName);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Ustawienie języka dla elementów UI
            if (!_isMetadataOverridden)
            {
                FrameworkElement.LanguageProperty.OverrideMetadata(
                    typeof(FrameworkElement),
                    new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(culture.IetfLanguageTag)));

                _isMetadataOverridden = true;
            }
        }
    }
}
