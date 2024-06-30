using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bijington.Cupcakes.Products.ViewModels;

public partial class AddProductPageViewModel : ObservableObject
{
    public AddProductPageViewModel(IMediaPicker mediaPicker, IProductRepository productRepository)
    {
        _mediaPicker = mediaPicker;
        _productRepository = productRepository;
    }
    
    private readonly IMediaPicker _mediaPicker;
    private readonly IProductRepository _productRepository;

    [ObservableProperty]
    private string _productName = string.Empty;
    
    [ObservableProperty]
    private string _price = string.Empty;
    
    [ObservableProperty]
    private string _imagePath = string.Empty;
    
    [RelayCommand]
    private async Task OnTakePhoto()
    {
        if (_mediaPicker.IsCaptureSupported)
        {
            var photo = await _mediaPicker.PickPhotoAsync();//.CapturePhotoAsync();

            if (photo is not null)
            {
                // save the file into local storage
                var localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                await using var sourceStream = await photo.OpenReadAsync();
                await using var localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);

                ImagePath = localFilePath;
            }
        }
    }

    [RelayCommand]
    private async Task OnSave()
    {
        var product = new Product
        {
            Name = ProductName,
            Price = decimal.Parse(Price),
            ImagePath = ImagePath
        };

        await _productRepository.Save(product);

        await Shell.Current.GoToAsync("..");
    }
}