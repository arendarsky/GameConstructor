using GameConstructor.Core.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameConstructor.Core.SpecialMethods
{
    public class ImageUploaded
    {
        string _fullImagePath;

        Picture _picture;


        public Picture Picture => _picture;


        public ImageUploaded()
        {
            _picture = new Picture();
        }



        public void UploadImageAndSave()
        {
            if (UploadImage())
            {
                string destinationPath = GetDestinationPath(_picture.ImageSource, "Images");

                File.Copy(_fullImagePath, destinationPath, true);
            }
        }

        private bool UploadImage()
        {
            OpenFileDialog uploadingImageDialog = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png"
            };

            if (uploadingImageDialog.ShowDialog() == DialogResult.OK)
            {
                _fullImagePath = uploadingImageDialog.FileName;

                string[] partsOfFileName = _fullImagePath.Split('\\');

                _picture.ImageSource = partsOfFileName[partsOfFileName.Length - 1];

                StateOfTheBorderWithDependencyOfType();
                
                return true;
            }

            return false;
        }

        private void StateOfTheBorderWithDependencyOfType()
        {
            string[] imageNameParts = _picture.ImageSource.Split('.');

            string imageTypeFormat = imageNameParts[imageNameParts.Length - 1];

            if (imageTypeFormat == "png")
            {
                _picture.IsBorderRequired = false;
            }

            else
            {
                _picture.IsBorderRequired = true;
            }
        }



        public static string GetDestinationPath(string fileName, string folderName)
        {
            string appStartPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            for (int i = 0; i < 2; i++)
            {
                int lastIndex = appStartPath.LastIndexOf("\\");
                appStartPath = new string(appStartPath.Take(lastIndex).ToArray());
            }           

            appStartPath = string.Format(appStartPath + "\\{0}\\" + fileName, folderName);

            return appStartPath;            
        }
    }
}
