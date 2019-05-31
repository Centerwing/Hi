using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Reflection;
namespace MusicController
{
    abstract class InstrumentBase
    {
        public InstrumentBase(string insName, InstrumentConfig config)
        {
            //这里必须是主程序（Microsoft.Samples.Kinect.DiscreteGestureBasics）的namespace
            string npName = Assembly.GetExecutingAssembly().GetName().Name.ToString();
            string musicPath = @".music";
            var assembly = Assembly.GetExecutingAssembly();
            System.IO.Stream stream = assembly.GetManifestResourceStream(npName + musicPath + "." + insName + ".wav");
            sp = new SoundPlayer(stream);
            threshold = config.threshold;
            delay = config.delay;
        }
        public abstract void Play(double confidience);
        ///returns the soundPlayer
        protected SoundPlayer sp { get; set; }
        protected DateTime lastDetection;
        protected double threshold;
        protected double delay;
    }
    class InstrumentDis : InstrumentBase
    {
        public InstrumentDis(string insName, InstrumentConfig config) : base(insName, config)
        {

        }
        public override void Play(double confidence)
        {
            if (confidence > threshold)
            {
                if ((DateTime.Now - base.lastDetection).TotalMilliseconds > 1000)
                {
                    base.lastDetection = DateTime.Now;
                    sp.Play();
                }
            }
        }
    }
    class InstrumentCon : InstrumentBase
    {
        public InstrumentCon(string insName, InstrumentConfig config) : base(insName, config)
        {
            isRinging = false;
        }
        public override void Play(double confidence)
        {
            if (confidence > threshold)
            {
                lastDetection = DateTime.Now;
                if (!isRinging)
                {
                    sp.PlayLooping();
                    isRinging = true;
                }
            }
            else
            {
                if (isRinging)
                {
                    if ((DateTime.Now - lastDetection).TotalMilliseconds > delay)
                    {
                        isRinging = false;
                        sp.Stop();
                    }

                }
            }
        }
        private bool isRinging;
    }
    public class InstrumentConfig
    {
        public double threshold;
        public double runningThreshold;
        public double delay;
        /// <summary>
        /// is discrete?
        /// </summary>
        public bool isDiscrete;
    }
}
