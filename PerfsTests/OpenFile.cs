using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using code_in.Views.NodalView;
using code_in;
using Code_in;

namespace PerfsTests
{
    [TestClass]
    public class OpenFile
    {
        [TestMethod]
        public void OpenLargeFile()
        {
            NodalView _spagetti = new NodalView(code_in.Code_inApplication.MainResourceDictionary);
            TimeSpan _failureValue = new TimeSpan(0,0,10);
            TimeSpan _previousValue = new TimeSpan(0,0,10);
            Stopwatch _elapsedTime = new Stopwatch();
            
            _elapsedTime.Start();
            _spagetti._nodalPresenter.OpenFile("../TestSamples/Size.cs");
            _elapsedTime.Stop();
            if (_elapsedTime.Elapsed > _failureValue)
                throw new TimeoutException("File opening exceded max value");
            else if (_elapsedTime.Elapsed > _previousValue)
                System.Diagnostics.Trace.WriteLine("File opening took " + (_previousValue - _elapsedTime.Elapsed).TotalSeconds + " compared to previous result");
            else
                System.Diagnostics.Trace.WriteLine("OpenLargeFile test valid, please update _previousValue to" + _elapsedTime.Elapsed);
        }
    }
}
