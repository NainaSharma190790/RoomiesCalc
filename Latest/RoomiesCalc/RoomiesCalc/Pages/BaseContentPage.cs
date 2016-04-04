using RoomiesCalc.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomiesCalc
{
    public class BaseContentPage<T>: MainBaseContentPage where T : BaseViewModel, new()
    {
        protected T _viewModel;

        public T ViewModel
        {
            get
            {
                return _viewModel ?? (_viewModel = new T());
            }
        }

        ~BaseContentPage()
        {
            _viewModel = null;
        }

        public BaseContentPage()
        {
            BindingContext = ViewModel;
        }
    }
}
