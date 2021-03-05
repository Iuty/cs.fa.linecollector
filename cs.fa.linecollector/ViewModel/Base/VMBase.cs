using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;

namespace cs.fa.linecollector.ViewModel
{
    public class VMBase:ViewModelBase
    {
        public VMBase()
        {
            DispatcherHelper.Initialize();
        }
        
        public override void RaisePropertyChanged(string PropertyName)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                base.RaisePropertyChanged(PropertyName);
            }
            );
        }
    }
}
