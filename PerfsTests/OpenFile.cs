using System;
using System.Windows.Forms;
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
            DeclarationsNodalView _spagetti = new DeclarationsNodalView(code_in.Code_inApplication.MainResourceDictionary);
            code_in.Presenters.Nodal.DeclarationsNodalPresenterLocal nodalPres = new code_in.Presenters.Nodal.DeclarationsNodalPresenterLocal();
            code_in.Views.NodalView.NodalViewActions.AttachNodalViewAndPresenter(_spagetti, nodalPres);
            TimeSpan _failureValue = new TimeSpan(0,0,10);
            TimeSpan _previousValue = new TimeSpan(0,0,10);
            Stopwatch _elapsedTime = new Stopwatch();
            OpenFileDialog _dialog = new OpenFileDialog();
            
            var dialogResult = _dialog.ShowDialog();
            if (dialogResult == DialogResult.None || dialogResult == DialogResult.Cancel)
                throw new ArgumentNullException("No file was selected");
            _elapsedTime.Start();
           // global::System.Windows.Forms.MessageBox.Show(System.Reflection.Assembly.GetExecutingAssembly().Location);
            _spagetti.OpenFile(_dialog.FileName);
            _elapsedTime.Stop();
            if (_elapsedTime.Elapsed > _failureValue)
                throw new TimeoutException("File opening exceded max value");
            else if (_elapsedTime.Elapsed > _previousValue)
                System.Diagnostics.Trace.WriteLine("File opening took " + (_previousValue - _elapsedTime.Elapsed).TotalSeconds + " compared to previous result");
            else
                System.Diagnostics.Trace.WriteLine("OpenLargeFile test valid, please update _previousValue to" + _elapsedTime.Elapsed.TotalSeconds);
        }
    }
}
