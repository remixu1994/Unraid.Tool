using NAudio.Flac;
using NAudio.Lame;
using NAudio.Wave;
using NAudio;
namespace Unraid.Tool;

public static class MusicTool
{
    public static void Main(string inputFilePath, string outputFilePath, int outputBitrate)
    {
        var audioConverter = new AudioConverter();
        audioConverter.ConvertFlacToLowBitrate(@"C:\Users\hp\Downloads\周杰伦_ 给我一首歌的时间.wav", @"C:\Users\hp\Downloads\周杰伦_ 给我一首歌的时间2.wav");
    }
}


public class AudioConverter
{
    public void ConvertFlacToLowBitrate(string inputFilePath, string outputFilePath)
    {
        using (var reader = new WaveFileReader(inputFilePath))
        {
            // 创建一个新的WaveFormat对象，设置较低的采样率、位深度和声道数
            var newFormat = new WaveFormat(22050, 8, 1); // 22.05kHz采样率，8位位深度，1声道

            // 创建一个WaveFormatConversionStream，用于将原始音频流转换为新的格式
            using (var converter = new WaveFormatConversionStream(newFormat, reader))
            {
                // 创建一个WaveFileWriter，用于将转换后的音频流写入新的WAV文件
                using (var writer = new WaveFileWriter(outputFilePath, converter.WaveFormat))
                {
                    byte[] buffer = new byte[converter.WaveFormat.AverageBytesPerSecond];
                    int bytesRead;
                    while ((bytesRead = converter.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        writer.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }
    }
}