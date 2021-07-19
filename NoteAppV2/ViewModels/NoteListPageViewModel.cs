using NoteAppV2.Model;
using NoteAppV2.Service;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NoteAppV2.ViewModels
{
    public class NoteListPageViewModel : ViewModelBase
    {
        readonly INavigationService _navigationService;
        readonly INoteAppService _noteAppService;

        //delegates
        public DelegateCommand AddClickedCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand<NoteItem> ItemSelectedCommand => new DelegateCommand<NoteItem>(OnItemSelectedCommand);


        public NoteListPageViewModel(INavigationService navigationService, INoteAppService noteAppService) :base(navigationService)
        {
            _navigationService = navigationService;
            _noteAppService = noteAppService;

            AddClickedCommand = new DelegateCommand(AddNewNote);
            DeleteCommand = new DelegateCommand(DeleteItem);
        }

        // sending parameter on navigatin
        private async void OnItemSelectedCommand(NoteItem senditem)
        {
            var parameter = new NavigationParameters();
            parameter.Add("item", senditem);

            await _navigationService.NavigateAsync("NoteFormPage", parameter);
        }

        //Naviate to Entry page (Icon onToolbar)
        private async void AddNewNote()
        {
            await _navigationService.NavigateAsync("NoteFormPage");
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
                SelectedItem = null;

                var res = await _noteAppService.GetItemAsync();

                if (!Equals(res, null))
                {
                    NoteItems = new ObservableCollection<NoteItem>(res);
                }
            
         
        }

        private void DeleteItem()
        {
            int id = NoteItem.ID; //ToDo:remove
            _noteAppService.DeleteItemAsync(NoteItem);
            _navigationService.GoBackAsync();
        }

        //properties and manipulations

        private ObservableCollection<NoteItem> _noteitems;
        public ObservableCollection<NoteItem> NoteItems
        {
            get { return _noteitems; }
            set { SetProperty(ref _noteitems, value); }
        }


        private NoteItem _noteItem;
        public NoteItem NoteItem
        {
            get { return _noteItem; }
            set { SetProperty(ref _noteItem, value); }
        }

        private NoteItem _selectedItem;
        public NoteItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                IsItemSelected = !Equals(_selectedItem, null);
            }
        }

        private bool _isItemSelected;
        public bool IsItemSelected
        {
            get { return _isItemSelected; }
            set { SetProperty(ref _isItemSelected, value); }
        }

        private string _titles;
        public string Titles
        {
            get { return _titles; }
            set { SetProperty(ref _titles, value); }
        }

        private string _content;
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        private string _date;
        public string Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }
    }
}
