using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageSaver saver = new ImageSaver();
            PictureProvider provider = new PictureProvider();
            FilterNegative filterNegative = new FilterNegative();
            FilterGreyscale filterGreyscale = new FilterGreyscale();

            IPicture picture = provider.GetPicture(@"luke.jpg");

            PipeNull pipeNull = new PipeNull();
            IPicture image2 = pipeNull.Send(picture);

            PipeSerial pipeSerial2 = new PipeSerial(filterNegative, pipeNull);
            IPicture image1 = pipeSerial2.Send(image2);

            saver.SavePicture(image1, @"lukeNegative.jpg");

            PipeSerial pipeSerial1 = new PipeSerial(filterGreyscale, pipeSerial2);
            IPicture image = pipeSerial1.Send(image1);

            PictureProvider provider_end = new PictureProvider();
            provider_end.SavePicture(image, @"luke_final.jpg");
            
        }
    }
}
