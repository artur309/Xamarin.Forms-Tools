using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;

namespace App1
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();
        private MediaFile file;

        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void BtnPick_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void BtnUpload_Clicked(object sender, EventArgs e)
        {
            await firebaseStorageHelper.UploadFile(file.GetStream(), Path.GetFileName(file.Path));
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            await firebaseStorageHelper.DeleteFile(txtFileName.Text);
            lblPath.Text = string.Empty;
            await DisplayAlert("Success", "Deleted", "OK");
        }

        private async void BtnDownload_Clicked(object sender, EventArgs e)
        {
            string path = await firebaseStorageHelper.GetFile(txtFileName.Text);
            if (path != null)
            {
                lblPath.Text = path;
                await DisplayAlert("Success", path, "OK");
            }
        }
    }
}