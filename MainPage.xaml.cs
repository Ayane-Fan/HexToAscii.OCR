using HexToAscii.OCR.Services;
using TesseractOcrMaui;
using TesseractOcrMaui.Results;

namespace HexToAscii.OCR;

public partial class MainPage : ContentPage
{
    int count = 0;
    private readonly ITesseract Tesseract;
    private readonly IHexService HexService;

    public MainPage(ITesseract tesseract, IHexService hexService)
    {
        InitializeComponent();
        Tesseract = tesseract;
        HexService = hexService;

        ConfigureEngine();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        // Make user pick file
        var pickResult = await FilePicker.PickAsync(new PickOptions()
        {
            PickerTitle = "Pick jpeg or png image",
            // Currently usable image types
            FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>()
            {
                [DevicePlatform.Android] = new List<string>() { "image/png", "image/jpeg" },
            })
        });
        // null if user cancelled the operation
        if (pickResult is null)
        {
            return;
        }

        // Recognize image
        var result = await Tesseract.RecognizeTextAsync(pickResult.FullPath);

        // Show output
        var resultLabel = "";
        var confidenceLabel = $"Confidence: {result.Confidence}";
        if (result.NotSuccess())
        {
            resultLabel = $"Recognizion failed: {result.Status}";
            return;
        }
        resultLabel = result.RecognisedText;
        var asciiResult = HexService.ConvertToAscii(resultLabel);
        Console.WriteLine(resultLabel);
    }

    private void ConfigureEngine()
    {
        Tesseract.EngineConfiguration = (engine) =>
        {
            // Tell the ocr to only look for hexadecimal characters
            engine.SetCharacterWhitelist("0123456789ABCDEFabcdefg");
        };
    }
}

