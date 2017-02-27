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
        int lab = 0;

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


                double var_R = sliderRed.Value / 255;        //R from 0 to 255
                double var_G = sliderGreen.Value / 255;        //G from 0 to 255
                double var_B = sliderBlue.Value / 255;        //B from 0 to 255

                if (var_R > 0.04045)
                    var_R = Math.Pow((var_R + 0.055)/1.055, 2.4);
                else var_R = var_R/12.92;
                if (var_G > 0.04045)
                    var_G = Math.Pow((var_G + 0.055)/1.055, 2.4);
                else var_G = var_G/12.92;
                if (var_B > 0.04045)
                    var_B = Math.Pow((var_B + 0.055)/1.055, 2.4);
                else var_B = var_B/12.92;

                var_R *= 100;
                var_G *= 100;
                var_B *= 100;

                //Observer. = 2°, Illuminant = D65
                double X = var_R*0.4124 + var_G*0.3576 + var_B*0.1805;
                double Y = var_R*0.2126 + var_G*0.7152 + var_B*0.0722;
                double Z = var_R*0.0193 + var_G*0.1192 + var_B*0.9505;

                sliderLightness.Value = 116*FuncLab(Y/100) - 16;
                sliderA.Value = 500*(FuncLab(X/95.047) - FuncLab(Y/100));
                sliderB.Value = 200 * (FuncLab(X / 95.047) - FuncLab(Z / 108.883));
                CountSliders();
                #endregion
            }
            #region Writing in texbox
            
            #endregion
        }

        private void sliderCMYK_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (cmy == 1)
            {
                double[] colorsCmy = {sliderCyan.Value, sliderMagenta.Value, sliderYellow.Value};
                sliderKey.Value = colorsCmy.Min();
                #region Writing in rgb and hsv sliders
                double red = 255*(1 - sliderCyan.Value);
                double green = 255*(1 - sliderMagenta.Value);
                double blue = 255*(1 - sliderYellow.Value);
                sliderRed.Value = red;
                sliderGreen.Value = green;
                sliderBlue.Value = blue;
                Color color = Color.FromRgb((byte)red, (byte)green, (byte)blue);
                this.Background = new SolidColorBrush(color);

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
                if (delta == 0)
                    hue = 0;

                double satur = 0;
                if (cmax > 0)
                    satur = delta / cmax;
                double value = cmax;

                sliderHue.Value = hue;
                sliderSaturation.Value = satur;
                sliderValue.Value = value;

                double var_R = sliderRed.Value / 255;        //R from 0 to 255
                double var_G = sliderGreen.Value / 255;        //G from 0 to 255
                double var_B = sliderBlue.Value / 255;        //B from 0 to 255

                if (var_R > 0.04045)
                    var_R = Math.Pow((var_R + 0.055) / 1.055, 2.4);
                else var_R = var_R / 12.92;
                if (var_G > 0.04045)
                    var_G = Math.Pow((var_G + 0.055) / 1.055, 2.4);
                else var_G = var_G / 12.92;
                if (var_B > 0.04045)
                    var_B = Math.Pow((var_B + 0.055) / 1.055, 2.4);
                else var_B = var_B / 12.92;

                var_R *= 100;
                var_G *= 100;
                var_B *= 100;

                //Observer. = 2°, Illuminant = D65
                double X = var_R * 0.4124 + var_G * 0.3576 + var_B * 0.1805;
                double Y = var_R * 0.2126 + var_G * 0.7152 + var_B * 0.0722;
                double Z = var_R * 0.0193 + var_G * 0.1192 + var_B * 0.9505;

                sliderLightness.Value = 116 * FuncLab(Y / 100) - 16;
                sliderA.Value = 500 * (FuncLab(X / 95.047) - FuncLab(Y / 100));
                sliderB.Value = 200 * (FuncLab(X / 95.047) - FuncLab(Z / 108.883));
                #endregion
                
                #region Writing in texbox
                textBoxRed.Text = red.ToString();
                textBoxGreen.Text = green.ToString();
                textBoxBlue.Text = blue.ToString();
                #endregion
                
                CountSliders();

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

                double var_R = sliderRed.Value / 255;        //R from 0 to 255
                double var_G = sliderGreen.Value / 255;        //G from 0 to 255
                double var_B = sliderBlue.Value / 255;        //B from 0 to 255

                if (var_R > 0.04045)
                    var_R = Math.Pow((var_R + 0.055) / 1.055, 2.4);
                else var_R = var_R / 12.92;
                if (var_G > 0.04045)
                    var_G = Math.Pow((var_G + 0.055) / 1.055, 2.4);
                else var_G = var_G / 12.92;
                if (var_B > 0.04045)
                    var_B = Math.Pow((var_B + 0.055) / 1.055, 2.4);
                else var_B = var_B / 12.92;

                var_R *= 100;
                var_G *= 100;
                var_B *= 100;

                //Observer. = 2°, Illuminant = D65
                double X = var_R * 0.4124 + var_G * 0.3576 + var_B * 0.1805;
                double Y = var_R * 0.2126 + var_G * 0.7152 + var_B * 0.0722;
                double Z = var_R * 0.0193 + var_G * 0.1192 + var_B * 0.9505;

                sliderLightness.Value = 116 * FuncLab(Y / 100) - 16;
                sliderA.Value = 500 * (FuncLab(X / 95.047) - FuncLab(Y / 100));
                sliderB.Value = 200 * (FuncLab(X / 95.047) - FuncLab(Z / 108.883));
                #endregion
                Color color = Color.FromRgb((byte) ((redS + m)*255), (byte) ((greenS + m)*255), (byte) ((blueS + m)*255));
                #region Writing in texbox
                textBoxRed.Text = ((redS + m) * 255).ToString();
                textBoxGreen.Text = ((greenS + m) * 255).ToString();
                textBoxBlue.Text = ((blueS + m) * 255).ToString();
                #endregion
                this.Background = new SolidColorBrush(color);
                CountSliders();

            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (rgb + cmy + hsv + lab == 0)
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

        public double FuncLab(double t) =>  t > Math.Pow(6.0/29, 3)? Math.Pow(t, 1.0/3) : t/(108.0/841) + 4.0/29;
        public double ReverseFuncLab(double t) =>  t > (6.0/29)? Math.Pow(t, 3) : (108.0/841)*(t - 4.0/29);

        #region Cursor
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

        private void sliderLab_MouseEnter(object sender, MouseEventArgs e)
        {
            lab = 1;
        }

        private void sliderLab_MouseLeave(object sender, MouseEventArgs e)
        {
            lab = 0;
        }
        #endregion

        private void sliderLab_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (lab == 1)
            {
                double X = 95.047*ReverseFuncLab((sliderLightness.Value + 16)/116 + sliderA.Value/500);
                double Y = 100 * ReverseFuncLab((sliderLightness.Value + 16) / 116);
                double Z = 108.883 * ReverseFuncLab((sliderLightness.Value + 16) / 116 - sliderB.Value / 500);

                var var_X = X/100;        //X from 0 to  95.047      (Observer = 2°, Illuminant = D65)
                var var_Y = Y/100;       //Y from 0 to 100.000
                var var_Z = Z/100;        //Z from 0 to 108.883

                var var_R = var_X*3.2406 + -var_Y*1.5372 + -var_Z*0.4986;
                var var_G = -var_X*0.9689 + var_Y*1.8758 + var_Z*0.0415;
                var var_B = var_X*0.0557 + -var_Y*0.2040 + var_Z*1.0570;

                if (var_R > 0.0031308)
                    var_R = 1.055*(Math.Pow(var_R,(1.0/2.4))) - 0.055;
                else var_R = 12.92*var_R;
                if (var_G > 0.0031308)
                    var_G = 1.055*(Math.Pow(var_G, (1.0 / 2.4))) - 0.055;
                else var_G = 12.92*var_G;
                if (var_B > 0.0031308)
                    var_B = 1.055*(Math.Pow(var_B, (1.0 / 2.4))) - 0.055;
                else var_B = 12.92*var_B;

                double R = var_R*255;
                double G = var_G*255;
                double B = var_B*255;

                if (R > 255 || G > 255 || B > 255)
                {
                    if (R > 255)
                        R = 255;
                    if (G > 255)
                        G = 255;
                    else
                        B = 255;
                    textBlock.Text = "ОСТОРОЖНО";
                }
                else if (R < 0 || G < 0 || B < 0)
                {
                    if (R < 0)
                        R = 0;
                    if (G < 0)
                        G = 0;
                    else
                        B = 0;
                    textBlock.Text = "ОСТОРОЖНО";
                }
                else
                    textBlock.Text = "ПОРЯДОК";

                Color color = Color.FromRgb((byte)((R)), (byte)((G)), (byte)((B)));
                this.Background = new SolidColorBrush(color);

                sliderRed.Value = R;
                sliderGreen.Value = G;
                sliderBlue.Value = B;

                double cyan = 1 - sliderRed.Value / 255;
                double magenta = 1 - sliderGreen.Value / 255;
                double yellow = 1 - sliderBlue.Value / 255;
                double[] colorsCmy = { sliderCyan.Value, sliderMagenta.Value, sliderYellow.Value };
                sliderKey.Value = colorsCmy.Min();
                sliderCyan.Value = cyan;
                sliderMagenta.Value = magenta;
                sliderYellow.Value = yellow;

                double r = sliderRed.Value / 255;
                double g = sliderGreen.Value / 255;
                double b = sliderBlue.Value / 255;
                double[] mas = { r, g, b };
                double cmax = mas.Max();
                double cmin = mas.Min();
                double delta = cmax - cmin;
                double hue = 0;

                if (cmax == b)
                    hue = 60 * ((r - g) / delta) + 240;
                if (cmax == g)
                    hue = 60 * ((b - r) / delta) + 120;
                if (cmax == r)
                    hue = (60 * ((g - b) / delta) + 360) % 360;
                if (delta == 0)
                    hue = 0;
                double satur = 0;
                if (Math.Abs(cmax) > 0.000001)
                    satur = delta / cmax;
                double value = cmax;

                sliderHue.Value = hue;
                sliderSaturation.Value = satur;
                sliderValue.Value = value;
                CountSliders();

            }
        }

        private void CountSliders()
        {
            textBoxC.Text = sliderCyan.Value.ToString();
            textBoxM.Text = sliderMagenta.Value.ToString();
            textBoxY.Text = sliderYellow.Value.ToString();
            textBoxH.Text = sliderHue.Value.ToString();
            textBoxS.Text = sliderSaturation.Value.ToString();
            textBoxV.Text = sliderValue.Value.ToString();
            textBoxL.Text = sliderLightness.Value.ToString();
            textBoxA.Text = sliderA.Value.ToString();
            textBoxB.Text = sliderB.Value.ToString();
            textBoxRed.Text = sliderRed.Value.ToString();
            textBoxGreen.Text = sliderGreen.Value.ToString();
            textBoxBlue.Text = sliderBlue.Value.ToString();
        }
    }
}
