using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinNavigation.ViewModels.Base
{
    public abstract class BaseViewModel : BindableObject
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
    }
}