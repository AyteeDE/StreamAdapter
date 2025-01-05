using System.Numerics;
using System.Text.Json.Serialization;
namespace AyteeDE.StreamAdapter.Core.Entities
{
    public class OutputStatusInformation
    {
        public OutputStatus Status { get; set; }
        public bool IsReconnecting { get; set; }
        public bool IsPaused { get; set; }
        public string? Timecode { get; set; }
        public double Duration { get; set; }
        public double Congestion { get; set; }
        public double Bytes { get; set; }
        public int SkippedFrames { get; set; }
        public int TotalFrames { get; set; }
    }

    public enum OutputStatus
    {
        Recording,
        Stopped,
        Streaming,
        Paused
    }
}