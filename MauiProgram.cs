using Camera.MAUI;
using HexToAscii.OCR.Services;
using Microsoft.Extensions.Logging;
using TesseractOcrMaui;

namespace HexToAscii.OCR;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCameraView();

        // Inject library functionality
        builder.Services.AddTesseractOcr(
            files =>
            {
                // must have matching files in Resources/Raw folder
                files.AddFile("eng.traineddata");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IHexService, HexService>();
        builder.Services.AddSingleton<MainPage>();
        return builder.Build();
    }
}
