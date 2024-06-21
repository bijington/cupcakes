using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bijington.Cupcakes.ViewModels;

public partial class AddProductViewModel : ObservableObject
{
    public AddProductViewModel(IMediaPicker mediaPicker)
    {
        _mediaPicker = mediaPicker;
    }
    
    private readonly IMediaPicker _mediaPicker;
    
    [ObservableProperty]
    private string _productName = string.Empty;
    
    [ObservableProperty]
    private string _price = string.Empty;
    
    [ObservableProperty]
    private string _imagePath = string.Empty;
    
    [RelayCommand]
    async Task OnTakePhoto()
    {
        if (_mediaPicker.IsCaptureSupported)
        {
            var photo = await _mediaPicker.CapturePhotoAsync();

            if (photo is not null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);

                ImagePath = localFilePath;
            }
        }
    }
}