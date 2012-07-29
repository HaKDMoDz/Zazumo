using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace SpriteLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            var palPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "Zazumo",
                    "PAL",
                    "Game.Pal");


            var spriteInputPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "Zazumo",
                    "Sprites");

            String spriteOutputPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "Zazumo",
                "Sprites",
                "Transformed");


            Color[] pal = new Color[256];

            using (var pFile = File.OpenRead(palPath))
            {
                using (var pReader = new BinaryReader(pFile))
                {
                    for (Int32 palIndex = 0; palIndex < 256; palIndex++)
                    {
                        pal[palIndex] = Color.FromRgb((Byte)(pReader.ReadByte()), (Byte)(pReader.ReadByte()), (Byte)(pReader.ReadByte()));
                    }
                }
            }

            pal[0] = Colors.Transparent;
            
            foreach (var fileName in Directory.GetFiles(spriteInputPath, "*.SPR"))
            {

                Byte[] fileData = null;

                using (var file = File.OpenRead(fileName))
                {
                    using (var reader = new BinaryReader(file))
                    {
                        fileData = reader.ReadBytes((Int32)file.Length);
                    }
                }

                if (fileData.Length == 0)
                    continue;

                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();

                var imageWidth = fileData[9];
                Int32 row = 0;

                if (imageWidth == 0)
                    continue;

                for (Int32 imagePosition = 0; imagePosition < (((fileData[6] << 8) + fileData[5]) - 11); imagePosition++)
                {
                    Int32 column = imagePosition % imageWidth;
                    row = imagePosition / imageWidth;

                    drawingContext.DrawRectangle(new SolidColorBrush(pal[fileData[imagePosition + 11]]), null, new Rect(column, row, 1, 1));
                }


                drawingContext.Close();

                RenderTargetBitmap bmp = new RenderTargetBitmap(imageWidth, row - 1, 96, 96, PixelFormats.Pbgra32);
                bmp.Render(drawingVisual);

                PngBitmapEncoder png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(bmp));

                var outputFile = Path.Combine(spriteOutputPath, Path.GetFileName(fileName).Replace(".SPR", ".png"));
                using (Stream stm = File.Create(outputFile))
                {
                    png.Save(stm);
                }
            }
        }
    }
}
