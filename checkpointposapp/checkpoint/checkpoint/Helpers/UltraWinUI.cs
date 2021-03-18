using checkpoint.Resources;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace checkpoint.Helpers
{
    public static class UltraWinUI
    {
        /// <summary>
        /// This method valid for text boxes that only receive numeric values ​​with two decimal places
        /// </summary>
        /// <param name="text">TextBox to valid</param>
        /// <returns>if the text entered is valid</returns>

        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "CheckPoint"; 
        public static void NumberValidationTextBox(this TextBox text, object sender, TextCompositionEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            var regex = new Regex(@"^[0-9]*(?:\.[0-9]{0,1})?$");
            string str = txt.Text + e.Text.ToString();
            int cntPrc = 0;
            if (str.Contains('.'))
            {
                string[] tokens = str.Split('.');
                if (tokens.Count() > 0)
                {
                    string result = tokens[1];
                    char[] prc = result.ToCharArray();
                    cntPrc = prc.Count();
                }
            }
            if (regex.IsMatch(e.Text) && !(e.Text == "." && ((TextBox)sender).Text.Contains(e.Text)) && (cntPrc < 3))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public static void OnlyNumbersValidationTextBox(this TextBox text, object sender, TextCompositionEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            var regex = new Regex(@"^\d$");
            if (regex.IsMatch(e.Text))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        public static void OnlyLettersValidationTextBox(this TextBox text, object sender, TextCompositionEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            var regex = new Regex(@"^[a-zA-Z]+$");
            if (regex.IsMatch(e.Text))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }        
        
        public static void LettersAndNumbersValidationTextBox(this TextBox text, object sender, TextCompositionEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            var regex = new Regex(@"^[a-zA-Z0-9]+$");
            if (regex.IsMatch(e.Text))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public static void TextBoxPropertiesConfigurations(this TextBox textBox, int maxLength, int maxLines, int minLines, TextAlignment textAlignment)
        {
            textBox.MaxLength = maxLength;
            textBox.MaxLines = maxLines;
            textBox.MinLines = minLines;
            textBox.TextAlignment = textAlignment;
        }

        public static void AddRange<T>(this BindingList<T> bindingList, IEnumerable<T> collection)
        {
            // The given collection may not be null.
            if (collection != null)
            {
                // Remember the current setting for RaiseListChangedEvents
                // (if it was already deactivated, we shouldn't activate it after adding!).
                var oldRaiseEventsValue = bindingList.RaiseListChangedEvents;

                // Try adding all of the elements to the binding list.
                try
                {
                    bindingList.RaiseListChangedEvents = false;

                    foreach (var value in collection)
                        bindingList.Add(value);
                }

                // Restore the old setting for RaiseListChangedEvents (even if there was an exception),
                // and fire the ListChanged-event once (if RaiseListChangedEvents is activated).
                finally
                {
                    bindingList.RaiseListChangedEvents = oldRaiseEventsValue;

                    if (bindingList.RaiseListChangedEvents)
                        bindingList.ResetBindings();
                }
            }            
        }

        public static System.Drawing.Image ResizeImage(System.Drawing.Image image, int newWidth, int newHeight)
        {
            using (Bitmap imagenBitmap = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppRgb))
            {
                imagenBitmap.SetResolution(Convert.ToInt32(image.HorizontalResolution), Convert.ToInt32(image.HorizontalResolution));
                using (Graphics imagenGraphics = Graphics.FromImage(imagenBitmap))
                {
                    imagenGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                    imagenGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    imagenGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    imagenGraphics.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                    MemoryStream imagenMemoryStream = new MemoryStream();
                    imagenBitmap.Save(imagenMemoryStream, ImageFormat.Png);
                    return System.Drawing.Image.FromStream(imagenMemoryStream);
                }
            }
        }

        public static void GetGoogleImageById(this System.Windows.Controls.Image image, string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                UserCredential credential;

                using (var stream =
                    new FileStream(StringResources.CredentialsPath, FileMode.Open, FileAccess.Read))
                {
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }
                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
                var request = service.Files.Get(id);
                var streamImage = new MemoryStream();
                request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
                {
                    switch (progress.Status)
                    {
                        case Google.Apis.Download.DownloadStatus.Completed:
                            {
                                Console.WriteLine("Descarga completada");
                                break;
                            }
                    }
                };
                request.Download(streamImage);
                Bitmap bitmap = new Bitmap(streamImage);
                image.Source = bitmap.ToBitmapImage();
            }            
        }

        public static BitmapImage ToBitmapImage(this Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
