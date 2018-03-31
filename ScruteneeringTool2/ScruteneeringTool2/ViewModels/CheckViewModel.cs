using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

using ScruteneeringTool2.Models;
using ScruteneeringTool2.Views;

using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;

namespace ScruteneeringTool2.ViewModels
{
    public class CheckViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; set; }

        public ICommand TakePictureCommand { get; }

        public ICommand PickFromGalleryCommand { get; }

        public ICommand DoOCR { get; }

        private ImageSource _takenImage;

        public ImageSource TakenImage
        {
            get => _takenImage;
            set => SetProperty(ref _takenImage, value);
        }

        private byte[] _imageBytes { get; set; }

        private string _ocrtext;

        private string _ocrrawtext { get; set; }

        public string OCRText
        {
            get => _ocrtext;
            set => SetProperty(ref _ocrtext, value);
        }

        private string _responseText;

        public string ResponseText
        {
            get => _responseText;
            set => SetProperty(ref _responseText, value);
        }

        public CheckViewModel()
        {
            Title = "Check";

            TakePictureCommand = new Command(TakePicture);
            PickFromGalleryCommand = new Command(PickFromGallery);
            DoOCR = new Command(TryOCR);



        }

        private async void TryOCR()
        {
            // KK
            //var client = new VisionServiceClient("18b08c8bb71a4aae9ab8938cf1a43fea");
            var client = new VisionServiceClient("18b08c8bb71a4aae9ab8938cf1a43fea", "https://westeurope.api.cognitive.microsoft.com/vision/v1.0");
            // VK
            //var client = new VisionServiceClient("f5071904cfd445dea680748c771f84d5");

            using (var photoStream = new MemoryStream(_imageBytes))
            {
                try
                {
                    var result = await client.RecognizeTextAsync(photoStream);                    

                    var sb1 = new StringBuilder();
                    var sb = new StringBuilder();

                    foreach (var region in result.Regions)
                    {
                        foreach (var line in region.Lines)
                        {
                            foreach (var word in line.Words)
                            {
                                sb.AppendLine(word.Text);
                                sb1.Append(word.Text);
                            }
                        }
                    }

                    OCRText = sb.ToString();
                    _ocrrawtext = sb1.ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }           
        }

        private async void TakePicture()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                //AlertService.Instance.ShowMessage("NoCameraAvailable".Translate(), true);
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Name = $"{DateTime.UtcNow.Ticks}",
                Directory = "MRO Historian",
            });

            if (file == null) return;

            SetTakenImage(file);
        }

        private async void PickFromGallery()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                //AlertService.Instance.ShowMessage("PhotoDisabled".Translate(), true);
            }
            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null) return;

            SetTakenImage(file);
        }

        private void SetTakenImage(MediaFile file)
        {
            //CanSave = true;

            var fileStream = file.GetStream();

            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                _imageBytes = memoryStream.ToArray();
            }

            TakenImage = ImageSource.FromStream(() => file.GetStream());
        }

        //async Task ExecuteLoadItemsCommand()
        //{
        //    if (IsBusy)
        //        return;

        //    IsBusy = true;

        //    try
        //    {
        //        Items.Clear();
        //        var items = await DataStore.GetItemsAsync(true);
        //        foreach (var item in items)
        //        {
        //            Items.Add(item);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
    }
}