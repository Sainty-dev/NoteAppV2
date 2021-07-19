using NoteAppV2.Model;
using NoteAppV2.Service;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoteAppV2.ViewModels
{
    public class NoteFormPageViewModel : ViewModelBase
    {
        //service
        private readonly INavigationService _navigationService;
        private readonly INoteAppService _noteAppService;

        //delegates
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public NoteFormPageViewModel(INavigationService navigationService, INoteAppService noteAppService) :base(navigationService)
        {
            _navigationService = navigationService;
            _noteAppService = noteAppService;

            SaveCommand = new DelegateCommand(SaveItem);
            CancelCommand = new DelegateCommand(CancelPage);

        }

        //Accepting parameters
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("item"))
            {
                NoteItem = (NoteItem)parameters["item"];
                NoteItem.ID = NoteItem.ID;
                Titles = NoteItem.Titles;
                Content = NoteItem.Content;

                Update = true;

            }
            else
            {
                Update =false;

            }
        }

        //Cancel Note (Unsaved)
        private void CancelPage()
        {
            _navigationService.GoBackAsync();
        }

        //Save Item only if fields are not empty
        private async void SaveItem()
        {

            if (Update)
            {
                NoteItem.Titles = Titles;
                NoteItem.Content = Content;
                NoteItem.Date = DateTime.UtcNow; // the curent time

                await _noteAppService.UpdateItemAsync(NoteItem);
                await _navigationService.GoBackAsync();

            }
            else if (!Update) //New note
            {
                NoteItem item = new NoteItem();
                item.Titles = Titles;
                item.Content = Content;
                item.Date = DateTime.UtcNow;

                if (!string.IsNullOrWhiteSpace(item.Content) ||
                     !string.IsNullOrWhiteSpace(item.Titles))
                {
                    await _noteAppService.InsertItemAsync(item);
                    await _navigationService.GoBackAsync();
                }

            }


        }

        // all properties 
        private bool _editMode;
        public bool Update
        {
            get { return _editMode; }
            set { SetProperty(ref _editMode, value); }
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

        private NoteItem _noteItem;
        public NoteItem NoteItem
        {
            get { return _noteItem; }
            set { SetProperty(ref _noteItem, value); }
        }

    }
}
