using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;



namespace PruebaWebBrowser
{
    public partial class Principal : Form
    {

        public Muros Muros_form = new Muros();
        public Pisos Pisos_form = new Pisos();
        public Principal()
        {
            InitializeComponent();
            InitBrowser();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int i = 0;
            Screen[] sMonitores;
            sMonitores = Screen.AllScreens;

            //Obtiene numero de pantallas conectadas
            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                String var = "Device Name: " + screen.DeviceName;
                var += "Primary Screen = " + screen.Primary.ToString();
                var += "Working Area: " + screen.WorkingArea.ToString();
                i++;
            }

            Screen primaryFormScreen = Screen.FromControl(this);

            // Logica para posicionar pantalla de Muros
            Screen secondaryFormScreen = Screen.AllScreens.FirstOrDefault(s => !s.Equals(primaryFormScreen)) ?? primaryFormScreen;
            Muros_form.Left = secondaryFormScreen.Bounds.Width;
            Muros_form.Top = secondaryFormScreen.Bounds.Height;
            Muros_form.StartPosition = FormStartPosition.Manual;
            Muros_form.Location = secondaryFormScreen.Bounds.Location;
            Point p = new Point(secondaryFormScreen.Bounds.Location.X, secondaryFormScreen.Bounds.Location.Y);
            Muros_form.Location = p;
            Muros_form.Show();


            // Logica para posicionar pantalla de Pisos

            Screen thirdFormScreen = Screen.AllScreens.Last();
            Pisos_form.Left = thirdFormScreen.Bounds.Width;
            Pisos_form.Top = thirdFormScreen.Bounds.Height;
            Pisos_form.StartPosition = FormStartPosition.Manual;
            Pisos_form.Location = thirdFormScreen.Bounds.Location;
            Point q = new Point(thirdFormScreen.Bounds.Location.X, thirdFormScreen.Bounds.Location.Y);
            Pisos_form.Location =q;
            Pisos_form.Show();

        
        }

        public ChromiumWebBrowser browser;
        public void InitBrowser()
        {
            var settings = new CefSettings { RemoteDebuggingPort = 8088 };
          //  settings
            Cef.Initialize(settings);
            //   browser = new ChromiumWebBrowser("http://pisosymuros.triplea.cl/index-2.html");
          //  browser = new ChromiumWebBrowser("http://localhost:3000");
           browser = new ChromiumWebBrowser("https://sodimaqueta.herokuapp.com?v7");
            browser.RegisterJsObject("bound", this);
            this.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
            
            browser.FrameLoadEnd += OnFrameLoadEnd;
            // browser.LoadError += OnLoadError;
                       browser.RenderProcessMessageHandler = new RenderProcessMessageHandler();



            browser.LoadError += (sender, args) =>
            {
                CheckForIllegalCrossThreadCalls = false;
                this.reiniciarBtn.Visible = true;
                
                this.reiniciarBtn.Location = new Point(
    this.ClientSize.Width / 2 - this.reiniciarBtn.Size.Width / 2,
    this.ClientSize.Height / 2 - this.reiniciarBtn.Size.Height / 2);
                this.reiniciarBtn.Anchor = AnchorStyles.None;
              
            };


                browser.LoadingStateChanged += (sender, args) =>
            {
                //Wait for the Page to finish loading
                if (args.IsLoading == false)
                {
                    

                    string script = " $('#loading_app').fadeOut(300);  $('.fondo').fadeIn(1400);";

                    
                    //EVENTO PROYECCIÓN MURO
                    script +="document.getElementById('muro_img_url').addEventListener('click', function(){bound.cambiaImagenMuro(document.getElementById('muro_img_url').value);});";

                    //EVENTO rotar MURO
                    script += "document.getElementById('muro_rotar').addEventListener('click', function(){bound.rotarImagenMuro(document.getElementById('muro_img_url').value);});";


                    //EVENTO PROYECCIÓN PISO
                    script += "document.getElementById('piso_img_url').addEventListener('click', function(){bound.cambiaImagenPiso(document.getElementById('piso_img_url').value);});";

                    //EVENTO rotar Piso
                   script += "document.getElementById('piso_rotar').addEventListener('click', function(){bound.rotarImagenPiso(document.getElementById('piso_img_url').value);});";

                    browser.ExecuteScriptAsync(script);
                }
               
            };


        }

        public void OnLoadError(object sender, FrameLoadEndEventArgs e)
        {
          
            

        }
        public void OnFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
         
            if (e.Frame.IsMain)
            {

                browser.ExecuteScriptAsync(@"

            ");
                

            }

            // Obtiene URL Actual
            //MessageBox.Show(browser.GetMainFrame().Url.ToString());
        }

        public void initalizateAPP()
        {
           
            string URL = "http://www.triplea.cl/imagesPisosYmuros/fondo1_muro_default.jpg";
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(URL);
            Bitmap img2;
            img2 = new Bitmap(stream);
            Muros_form.BackgroundImage = img2;

            URL = "http://www.triplea.cl/imagesPisosYmuros/piso-23.jpg";
            stream = client.OpenRead(URL);
            Bitmap img3;
            img3 = new Bitmap(stream);
            Pisos_form.BackgroundImage = img3;


        }
        public void cambiaImagenPiso(string URL)
        {
            
             
            
            if (URL == "")
            return; 
            string ImgLocalPath = "";

            int noExisteImg=0;
            int imagen_default = 0;
            switch (URL)
            {
                case "http://sodimac.scene7.com/is/image/SodimacCL/1280171":
                    ImgLocalPath = "Flotante/1280171.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/202036X":
                    ImgLocalPath = "Flotante/202036X.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2020378":
                    ImgLocalPath = "Flotante/2020378.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2037971":
                    ImgLocalPath = "Flotante/2037971.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2666898":
                    ImgLocalPath = "Flotante/2666898.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/266691X":
                    ImgLocalPath = "Flotante/266691X.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2666928":
                    ImgLocalPath = "Flotante/2666928.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2048736":
                    ImgLocalPath = "Flotante/2048736.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2666901":
                    ImgLocalPath = "Flotante/2666901.jpg";
                    break;

            
                case "http://sodimac.scene7.com/is/image/SodimacCL/2052121":
                    ImgLocalPath = "Porcelanato/2052121.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2667363":
                    ImgLocalPath = "Porcelanato/2667363.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2815915":
                    ImgLocalPath = "Porcelanato/2815915.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2840111":
                    ImgLocalPath = "Porcelanato/2840111.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2931176":
                    ImgLocalPath = "Porcelanato/2931176.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2931184":
                    ImgLocalPath = "Porcelanato/2931184.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2931192":
                    ImgLocalPath = "Porcelanato/2931192.jpg";
                    break;

                case "http://sodimac.scene7.com/is/image/SodimacCL/1116983":
                    ImgLocalPath = "Madera/1116983.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1181777":
                    ImgLocalPath = "Madera/1181777.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1181785":
                    ImgLocalPath = "Madera/1181785.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1194364":
                    ImgLocalPath = "Madera/1194364.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2011778":
                    ImgLocalPath = "Madera/2011778.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2123568":
                    ImgLocalPath = "Madera/2123568.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2123584":
                    ImgLocalPath = "Madera/2123584.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2123592":
                    ImgLocalPath = "Madera/2123592.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2279657":
                    ImgLocalPath = "Madera/2279657.jpg";
                    break;


                case "http://sodimac.scene7.com/is/image/SodimacCL/212548X":
                    ImgLocalPath = "Vinilico/212548X.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2346834":
                    ImgLocalPath = "Vinilico/2346834.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2346842":
                    ImgLocalPath = "Vinilico/2346842.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2756129":
                    ImgLocalPath = "Vinilico/2756129.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2756137":
                    ImgLocalPath = "Vinilico/2756137.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2829088":
                    ImgLocalPath = "Vinilico/2829088.jpg";
                    break;

                default:
                    noExisteImg = 1;
                    break;


            }


            if (noExisteImg > 0 || imagen_default >0)
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(URL);
                Bitmap img2;
                img2 = new Bitmap(stream);

              
                Pisos_form.BackgroundImage = img2;
                Pisos_form.BackgroundImageLayout = ImageLayout.Tile;

            }
            else
            {
                System.Drawing.Image img2 = System.Drawing.Image.FromFile("../../Resources/ImagenesProductos/" + ImgLocalPath);

                if (URL.ToString().Equals("f2"))
                {
                    img2.RotateFlip(RotateFlipType.Rotate90FlipXY);

                }

                Pisos_form.BackgroundImage = img2;
                Pisos_form.BackgroundImageLayout = ImageLayout.Stretch;

            }


            //CAMBIA TAMAÑO DE IMAGEN
            /*
             Bitmap newImg = new Bitmap(img2);
             Graphics g = Graphics.FromImage(newImg);
           // g.RotateTransform(45);
            g.InterpolationMode =
               System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
             g.DrawImage(img2, 0, 0, newImg.Width, newImg.Height);*/



        }

        public void rotarImagenPiso(string URL)
        {



            if (URL == "")
                return;
            string ImgLocalPath = "";

            int noExisteImg = 0;
            int imagen_default = 0;
            switch (URL)
            {
                case "http://sodimac.scene7.com/is/image/SodimacCL/1280171":
                    ImgLocalPath = "Flotante/1280171.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/202036X":
                    ImgLocalPath = "Flotante/202036X.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2020378":
                    ImgLocalPath = "Flotante/2020378.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2037971":
                    ImgLocalPath = "Flotante/2037971.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2666898":
                    ImgLocalPath = "Flotante/2666898.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/266691X":
                    ImgLocalPath = "Flotante/266691X.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2666928":
                    ImgLocalPath = "Flotante/2666928.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2048736":
                    ImgLocalPath = "Flotante/2048736.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2666901":
                    ImgLocalPath = "Flotante/2666901.jpg";
                    break;


                case "http://sodimac.scene7.com/is/image/SodimacCL/2052121":
                    ImgLocalPath = "Porcelanato/2052121.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2667363":
                    ImgLocalPath = "Porcelanato/2667363.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2815915":
                    ImgLocalPath = "Porcelanato/2815915.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2840111":
                    ImgLocalPath = "Porcelanato/2840111.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2931176":
                    ImgLocalPath = "Porcelanato/2931176.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2931184":
                    ImgLocalPath = "Porcelanato/2931184.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2931192":
                    ImgLocalPath = "Porcelanato/2931192.jpg";
                    break;

                case "http://sodimac.scene7.com/is/image/SodimacCL/1116983":
                    ImgLocalPath = "Madera/1116983.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1181777":
                    ImgLocalPath = "Madera/1181777.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1181785":
                    ImgLocalPath = "Madera/1181785.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1194364":
                    ImgLocalPath = "Madera/1194364.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2011778":
                    ImgLocalPath = "Madera/2011778.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2123568":
                    ImgLocalPath = "Madera/2123568.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2123584":
                    ImgLocalPath = "Madera/2123584.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2123592":
                    ImgLocalPath = "Madera/2123592.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2279657":
                    ImgLocalPath = "Madera/2279657.jpg";
                    break;


                case "http://sodimac.scene7.com/is/image/SodimacCL/212548X":
                    ImgLocalPath = "Vinilico/212548X.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2346834":
                    ImgLocalPath = "Vinilico/2346834.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2346842":
                    ImgLocalPath = "Vinilico/2346842.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2756129":
                    ImgLocalPath = "Vinilico/2756129.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2756137":
                    ImgLocalPath = "Vinilico/2756137.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2829088":
                    ImgLocalPath = "Vinilico/2829088.jpg";
                    break;

                default:
                    noExisteImg = 1;
                    break;


            }


            if (noExisteImg > 0 || imagen_default > 0)
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(URL);
                Bitmap img2;
                img2 = new Bitmap(stream);


                Pisos_form.BackgroundImage = img2;
                Pisos_form.BackgroundImageLayout = ImageLayout.Tile;

            }
            else
            {
                System.Drawing.Image img2 = System.Drawing.Image.FromFile("../../Resources/ImagenesProductos/" + ImgLocalPath);
                img2.RotateFlip(RotateFlipType.Rotate90FlipXY);
                Pisos_form.BackgroundImage = img2;
              
                Pisos_form.BackgroundImageLayout = ImageLayout.Stretch;

            }


            //CAMBIA TAMAÑO DE IMAGEN
            /*
             Bitmap newImg = new Bitmap(img2);
             Graphics g = Graphics.FromImage(newImg);
           // g.RotateTransform(45);
            g.InterpolationMode =
               System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
             g.DrawImage(img2, 0, 0, newImg.Width, newImg.Height);*/



        }

        public void rotarImagenMuro(string URL)
        {
           

            if (URL == "")
                return;
            string ImgLocalPath = "";
            int noExisteImg = 0;
            switch (URL)
            {

                case "http://sodimac.scene7.com/is/image/SodimacCL/1182005":
                    ImgLocalPath = "CeramicaMarm/1182005.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2230313":
                    ImgLocalPath = "CeramicaMarm/2230313.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2230321":
                    ImgLocalPath = "CeramicaMarm/2230321.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2771640":
                    ImgLocalPath = "CeramicaMarm/2771640.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2771667":
                    ImgLocalPath = "CeramicaMarm/2771667.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2850397":
                    ImgLocalPath = "CeramicaMarm/2850397.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/3179702":
                    ImgLocalPath = "CeramicaMarm/3179702.jpg";
                    break;


                case "http://sodimac.scene7.com/is/image/SodimacCL/1862626":
                    ImgLocalPath = "Ceramicas/1862626.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1862634":
                    ImgLocalPath = "Ceramicas/1862634.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1862642":
                    ImgLocalPath = "Ceramicas/1862642.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1862707":
                    ImgLocalPath = "Ceramicas/1862707.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1862715":
                    ImgLocalPath = "Ceramicas/1862715.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1862723":
                    ImgLocalPath = "Ceramicas/1862723.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2684357":
                    ImgLocalPath = "Ceramicas/2684357.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/3010341":
                    ImgLocalPath = "Ceramicas/3010341.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/301035x":
                    ImgLocalPath = "Ceramicas/301035x.jpg";
                    break;


                case "http://sodimac.scene7.com/is/image/SodimacCL/2052121":
                    ImgLocalPath = "Porcelanato/2052121.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2667363":
                    ImgLocalPath = "Porcelanato/2667363.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2815915":
                    ImgLocalPath = "Porcelanato/2815915.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2840111":
                    ImgLocalPath = "Porcelanato/2840111.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2931176":
                    ImgLocalPath = "Porcelanato/2931176.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2931184":
                    ImgLocalPath = "Porcelanato/2931184.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2931192":
                    ImgLocalPath = "Porcelanato/2931192.jpg";
                    break;


                case "http://sodimac.scene7.com/is/image/SodimacCL/1592378":
                    ImgLocalPath = "Papel/1592378.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592394":
                    ImgLocalPath = "Papel/1592394.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592416":
                    ImgLocalPath = "Papel/1592416.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592459":
                    ImgLocalPath = "Papel/1592459.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592548":
                    ImgLocalPath = "Papel/1592548.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592564":
                    ImgLocalPath = "Papel/1592564.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592599":
                    ImgLocalPath = "Papel/1592599.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592602":
                    ImgLocalPath = "Papel/1592602.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592629":
                    ImgLocalPath = "Papel/1592629.jpg";
                    break;

                default:
                    noExisteImg = 1;
                    break;


            }

            if (noExisteImg > 0 )
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(URL);
                Bitmap img2;
                img2 = new Bitmap(stream);

                    img2.RotateFlip(RotateFlipType.Rotate90FlipXY);
                Muros_form.BackgroundImage = img2;
                 Muros_form.BackgroundImageLayout = ImageLayout.Tile;

            }
            else
            {
                System.Drawing.Image img2 = System.Drawing.Image.FromFile("../../Resources/ImagenesProductos/" + ImgLocalPath);

              
                    img2.RotateFlip(RotateFlipType.Rotate90FlipXY);
                
                Muros_form.BackgroundImage = img2;
                Muros_form.BackgroundImageLayout = ImageLayout.Stretch;

            }

        }

        public void cambiaImagenMuro(string URL)
        {

            if (URL == "")
                return;
            string ImgLocalPath = "";
            int noExisteImg = 0;
            int imagen_default = 0;
            switch (URL)
            {

                case "http://sodimac.scene7.com/is/image/SodimacCL/1182005":
                    ImgLocalPath = "CeramicaMarm/1182005.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2230313":
                    ImgLocalPath = "CeramicaMarm/2230313.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2230321":
                    ImgLocalPath = "CeramicaMarm/2230321.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2771640":
                    ImgLocalPath = "CeramicaMarm/2771640.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2771667":
                    ImgLocalPath = "CeramicaMarm/2771667.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2850397":
                    ImgLocalPath = "CeramicaMarm/2850397.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/3179702":
                    ImgLocalPath = "CeramicaMarm/3179702.jpg";
                    break;
          

                case "http://sodimac.scene7.com/is/image/SodimacCL/1862626":
                    ImgLocalPath = "Ceramicas/1862626.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1862634":
                    ImgLocalPath = "Ceramicas/1862634.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1862642":
                    ImgLocalPath = "Ceramicas/1862642.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1862707":
                    ImgLocalPath = "Ceramicas/1862707.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1862715":
                    ImgLocalPath = "Ceramicas/1862715.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1862723":
                    ImgLocalPath = "Ceramicas/1862723.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2684357":
                    ImgLocalPath = "Ceramicas/2684357.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/3010341":
                    ImgLocalPath = "Ceramicas/3010341.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/301035x":
                    ImgLocalPath = "Ceramicas/301035x.jpg";
                    break;


                case "http://sodimac.scene7.com/is/image/SodimacCL/2052121":
                    ImgLocalPath = "Porcelanato/2052121.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2667363":
                    ImgLocalPath = "Porcelanato/2667363.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2815915":
                    ImgLocalPath = "Porcelanato/2815915.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2840111":
                    ImgLocalPath = "Porcelanato/2840111.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2931176":
                    ImgLocalPath = "Porcelanato/2931176.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2931184":
                    ImgLocalPath = "Porcelanato/2931184.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/2931192":
                    ImgLocalPath = "Porcelanato/2931192.jpg";
                    break;


                case "http://sodimac.scene7.com/is/image/SodimacCL/1592378":
                    ImgLocalPath = "Papel/1592378.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592394":
                    ImgLocalPath = "Papel/1592394.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592416":
                    ImgLocalPath = "Papel/1592416.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592459":
                    ImgLocalPath = "Papel/1592459.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592548":
                    ImgLocalPath = "Papel/1592548.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592564":
                    ImgLocalPath = "Papel/1592564.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592599":
                    ImgLocalPath = "Papel/1592599.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592602":
                    ImgLocalPath = "Papel/1592602.jpg";
                    break;
                case "http://sodimac.scene7.com/is/image/SodimacCL/1592629":
                    ImgLocalPath = "Papel/1592629.jpg";
                    break;
                case "http://www.triplea.cl/imagesPisosYmuros/fondo1_muro_default.jpg":
                    imagen_default = 1;

                    break;

                default:
                    noExisteImg = 1;
                    break ;
                
               
            }

            if (noExisteImg > 0 || imagen_default > 0)
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(URL);
                Bitmap img2;
                img2 = new Bitmap(stream);
           

                Muros_form.BackgroundImage = img2;
                if (imagen_default > 0)
                {
                    Muros_form.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                    Muros_form.BackgroundImageLayout = ImageLayout.Tile;
             
            }
            else
            {
                System.Drawing.Image img2 = System.Drawing.Image.FromFile("../../Resources/ImagenesProductos/" + ImgLocalPath);
                
           
                Muros_form.BackgroundImage = img2;
                Muros_form.BackgroundImageLayout = ImageLayout.Stretch;

            }

            // PRESENTA IMAGEN DESDE URL //
            /*
            
          


            //CAMBIA TAMAÑO DE IMAGEN
            /*
             Bitmap newImg = new Bitmap(img2);
             Graphics g = Graphics.FromImage(newImg);
           // g.RotateTransform(45);
            g.InterpolationMode =
               System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
             g.DrawImage(img2, 0, 0, newImg.Width, newImg.Height);*/



        }

        public void RestartApp()
        {
            Application.Restart();
        }

        private void reiniciarBtn_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    
}
