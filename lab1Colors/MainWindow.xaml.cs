using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab1Colors
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int rgb = 0;
        int hsv = 0;
        int cmy = 0;

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {   
            if (rgb != 0)
            {
                Color color = Color.FromRgb((byte)sliderRed.Value, (byte)sliderGreen.Value, (byte)sliderBlue.Value);
                this.Background = new SolidColorBrush(color);
                #region Writing in texbox
                textBoxRed.Text = sliderRed.Value.ToString();
                textBoxGreen.Text = sliderGreen.Value.ToString();
                textBoxBlue.Text = sliderBlue.Value.ToString();
                #endregion
                #region Writing in hsv and cmy sliders
                double cyan = 1 - sliderRed.Value/255;
                double magenta = 1 - sliderGreen.Value/255;
                double yellow = 1 - sliderBlue.Value/255;
                double[] colorsCmy = {sliderCyan.Value, sliderMagenta.Value, sliderYellow.Value};
                sliderKey.Value = colorsCmy.Min();
                sliderCyan.Value = cyan;
                sliderMagenta.Value = magenta;
                sliderYellow.Value = yellow;

                double r = sliderRed.Value/255;
                double g = sliderGreen.Value/255;
                double b = sliderBlue.Value/255;
                double[] mas = {r, g, b};
                double cmax = mas.Max();
                double cmin = mas.Min();
                double delta = cmax - cmin;
                double hue = 0;
                
                if (cmax == b)
                    hue = 60 * ((r - g) / delta) + 240;
                if (cmax == g)
                    hue = 60 * ((b - r) / delta) + 120;
                if (cmax == r)
                    hue = (60*((g - b)/delta) + 360)%360;
                if (delta == 0)
                    hue = 0;
                double satur = 0;
                if (Math.Abs(cmax) > 0.000001)
                    satur = delta/cmax;
                double value = cmax;

                sliderHue.Value = hue;
                sliderSaturation.Value = satur;
                sliderValue.Value = value;
                #endregion
            }
        }

        private void sliderCMYK_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (cmy == 1)
            {
                double[] colorsCmy = {sliderCyan.Value, sliderMagenta.Value, sliderYellow.Value};
                sliderKey.Value = colorsCmy.Min();
                #region Writing in rgb and hsv sliders
                double red = 255*(1 - sliderCyan.Value)*(1 - sliderKey.Value);
                double green = 255*(1 - sliderMagenta.Value)*(1 - sliderKey.Value);
                double blue = 255*(1 - sliderYellow.Value)*(1 - sliderKey.Value);
                sliderRed.Value = red;
                sliderGreen.Value = green;
                sliderBlue.Value = blue;

                double r = sliderRed.Value / 255;
                double g = sliderGreen.Value / 255;
                double b = sliderBlue.Value / 255;
                double[] mas = { r, g, b };
                double cmax = mas.Max();
                double cmin = mas.Min();
                double delta = cmax - cmin;
                double hue = 0;

                if (cmax == r)
                    hue = (60*((g - b)/delta) + 360)%360;
                if (cmax == g)
                    hue = 60 * ((b - r) / delta) + 120;
                if (cmax == b)
                    hue = 60 * ((r - g) / delta) + 240;
                
                double satur = 0;
                if (Math.Abs(cmax) > 0.000001)
                    satur = delta / cmax;
                double value = cmax;

                sliderHue.Value = hue;
                sliderSaturation.Value = satur;
                sliderValue.Value = value;
                #endregion
                Color color = Color.FromRgb((byte) red, (byte) green, (byte) blue);
                #region Writing in texbox
                textBoxRed.Text = red.ToString();
                textBoxGreen.Text = green.ToString();
                textBoxBlue.Text = blue.ToString();
                #endregion
                this.Background = new SolidColorBrush(color);
            }
        }

        private void sliderHSV_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (hsv == 1)
            {
                double c = sliderValue.Value*sliderSaturation.Value;
                double x = c*(1 - Math.Abs((sliderHue.Value/60)%2 - 1));
                double m = sliderValue.Value - c;

                double redS = 0, greenS = 0, blueS = 0;
                double h = sliderHue.Value;

                #region writing in rgb and cmy sliders
                if (h >= 0 && h < 60)
                {
                    redS = c;
                    greenS = x;
                    blueS = 0;
                }
                if (h >= 60 && h < 120)
                {
                    redS = x;
                    greenS = c;
                    blueS = 0;
                }
                if (h >= 120 && h < 180)
                {
                    redS = 0;
                    greenS = c;
                    blueS = x;
                }
                if (h >= 180 && h < 240)
                {
                    redS = 0;
                    greenS = x;
                    blueS = c;
                }
                if (h >= 240 && h < 300)
                {
                    redS = x;
                    greenS = 0;
                    blueS = c;
                }
                if (h >= 300 && h < 360)
                {
                    redS = c;
                    greenS = 0;
                    blueS = x;
                }

                

                sliderRed.Value = (redS + m)*255;
                sliderGreen.Value = (greenS + m)*255;
                sliderBlue.Value = (blueS + m)*255;

                double cyan = 1 - sliderRed.Value / 255;
                double magenta = 1 - sliderGreen.Value / 255;
                double yellow = 1 - sliderBlue.Value / 255;
                double[] colorsCmy = { sliderCyan.Value, sliderMagenta.Value, sliderYellow.Value };
                sliderKey.Value = colorsCmy.Min();
                sliderCyan.Value = cyan;
                sliderMagenta.Value = magenta;
                sliderYellow.Value = yellow;
                #endregion
                Color color = Color.FromRgb((byte) ((redS + m)*255), (byte) ((greenS + m)*255), (byte) ((blueS + m)*255));
                #region Writing in texbox
                textBoxRed.Text = ((redS + m) * 255).ToString();
                textBoxGreen.Text = ((greenS + m) * 255).ToString();
                textBoxBlue.Text = ((blueS + m) * 255).ToString();
                #endregion
                this.Background = new SolidColorBrush(color);
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (rgb + cmy + hsv == 0)
            {
                int red, green, blue;
                try
                {
                    red = int.Parse(textBoxRed.Text);
                    green = int.Parse(textBoxGreen.Text);
                    blue = int.Parse(textBoxBlue.Text);
                }
                catch (Exception)
                {
                    red = 255;
                    green = 255;
                    blue = 255;
                }
                if (red < 0 || red > 255)
                    red = 255;
                if (green < 0 || green > 255)
                    green = 255;
                if (blue < 0 || blue > 255)
                    blue = 255;

                sliderRed.Value = red;
                sliderGreen.Value = green;
                sliderBlue.Value = blue;
                Color color = Color.FromRgb((byte)sliderRed.Value, (byte)sliderGreen.Value, (byte)sliderBlue.Value);
                this.Background = new SolidColorBrush(color);
            }
        }

        private void slider_MouseEnter(object sender, MouseEventArgs e)
        {
            rgb = 1;
        }

        private void slider_MouseLeave(object sender, MouseEventArgs e)
        {
            rgb = 0;
        }

        private void sliderHSV_MouseEnter(object sender, MouseEventArgs e)
        {
            hsv = 1;
        }

        private void sliderHSV_MouseLeave(object sender, MouseEventArgs e)
        {
            hsv = 0;
        }

        private void sliderCMY_MouseEnter(object sender, MouseEventArgs e)
        {
            cmy = 1;
        }

        private void sliderCMY_MouseLeave(object sender, MouseEventArgs e)
        {
            cmy = 0;
        }
    }
}
