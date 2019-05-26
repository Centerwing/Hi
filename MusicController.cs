namespace MusicController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Media;
    using System.Reflection;
    using System.Threading;
    using Microsoft.Kinect;
    using Microsoft.Kinect.VisualGestureBuilder;
    using Newtonsoft.Json;
    class MusicController
    {
        Dictionary<string,InstrumentBase> instruments;
        //
        private DateTime lastRing;
        //
        public MusicController(string[] discreteGestureNames, string[] conGestureNames = null)
        {
            //gesture names
            this.DiscreteGestureNames = discreteGestureNames;
            this.ConGestureNames = conGestureNames;
            InitInstruments();
        }
        public void InitInstruments()
        {
            string text = System.IO.File.OpenText("config.json").ReadToEnd();

            var dic = JsonConvert.DeserializeObject<Dictionary<string, InstrumentConfig>>(text);

            //add all instruments;
            var config = new InstrumentConfig();
            {
                foreach (var gestureName in DiscreteGestureNames)
                {
                    dic.TryGetValue(gestureName, out config);
                    if(config!=null)
                    {
                        if (config.isDiscrete)
                        {
                            instruments.Add(gestureName, new InstrumentDis(gestureName, config));
                        }
                        else
                        {
                            instruments.Add(gestureName, new InstrumentCon(gestureName, config));
                        }
                    }
                }
            }

        }

        public void ProcessGestureResults(IReadOnlyDictionary<Gesture, DiscreteGestureResult> discreteResults)
        {
            foreach (var gestureResult in discreteResults)
            {
                instruments[gestureResult.Key.Name].Play(gestureResult.Value.Confidence);

            }
        }
        //npName===namespaceName
        public string NpName
        {
            get;
            private set;
        }
        //
        public string[] DiscreteGestureNames { get; private set; }
        public string[] ConGestureNames { get; private set; }
       
    }

}
