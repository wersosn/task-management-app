using DocumentFormat.OpenXml.Vml.Office;
using SuperZTP.Command;
using SuperZTP.Model;
using SuperZTP.Proxy;
using SuperZTP.TemplateMethod;
using SuperZTP.Views;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DocumentFormat.OpenXml.Drawing;
using SuperZTP.ViewModels;
using System.Windows.Media.Animation;

namespace SuperZTP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Zdarzenie, które uruchamia animację zaciemniania
        public void StartFadeOutAnimation()
        {
            var fadeOutStoryboard = (Storyboard)Application.Current.Resources["FadeOutStoryboard"];
            fadeOutStoryboard?.Begin(this);
        }

        // Zdarzenie, które uruchamia animację powrotu do normalnego stanu
        public void StartFadeInAnimation()
        {
            var fadeInStoryboard = (Storyboard)Application.Current.Resources["FadeInStoryboard"];
            fadeInStoryboard?.Begin(this);
        }

        // Metoda do otwierania okien modalnych
        public void OpenModalWindow(Window modalWindow)
        {
            StartFadeOutAnimation();
            modalWindow.ShowDialog();
            StartFadeInAnimation();
        }
    }
}
