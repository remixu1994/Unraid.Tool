using JetBrains.Annotations;
using Unraid.Tool;

namespace Unraid.Test;

[TestClass]
[TestSubject(typeof(AudioConverter))]
public class AudioConverterTest
{

    [TestMethod]
    public void Should_convert_to_Mp3()
    {
        var audioConverter = new AudioConverter();
        audioConverter.ConvertFlacToLowBitrate(@"C:\Users\hp\Downloads\周杰伦_ 给我一首歌的时间.wav", @"C:\Users\hp\Downloads\周杰伦_ 给我一首歌的时间2.wav");
    }
}