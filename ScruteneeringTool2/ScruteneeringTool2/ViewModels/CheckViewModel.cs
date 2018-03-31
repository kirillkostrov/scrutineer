using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

using ScruteneeringTool2.Models;
using ScruteneeringTool2.Views;

namespace ScruteneeringTool2.ViewModels
{
    public class CheckViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; set; }

        public ICommand TakePictureCommand { get; }

        public ICommand PickFromGalleryCommand { get; }

        private ImageSource _takenImage;

        public ImageSource TakenImage
        {
            get => _takenImage;
            set => SetProperty(ref _takenImage, value);
        }

        private byte[] _imageBytes { get; set; }

        private string _ocrtext;

        public string OCRText
        {
            get => _ocrtext;
            set => SetProperty(ref _ocrtext, value);
        }

        public CheckViewModel()
        {
            Title = "Check";

            TakePictureCommand = new Command(TakePicture);
            PickFromGalleryCommand = new Command(PickFromGallery);


            //Items = new ObservableCollection<Item>();
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var _item = item as Item;
            //    Items.Add(_item);
            //    await DataStore.AddItemAsync(_item);
            //});
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