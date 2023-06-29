using BLL.Services;
using BLL.Services.Contracts;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF.ViewModel
{
    public class ArtistViewModel : INotifyPropertyChanged
    {
        private List<Artist> _artist;
        public List<Artist> Artist
        {
            get { return _artist; }
            set
            {
                _artist = value;
                OnPropertyChanged("Artist");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _popularity;
        public string Popularity
        {
            get { return _popularity; }
            set
            {
                _popularity = value;
                OnPropertyChanged(nameof(Popularity));
            }
        }

        private string _namegenre;
        public string NameGenre
        {
            get { return _namegenre; }
            set
            {
                _namegenre = value;
                OnPropertyChanged(nameof(NameGenre));
            }
        }

        private string _followers;
        public string Followers
        {
            get { return _followers; }
            set
            {
                _followers = value;
                OnPropertyChanged(nameof(Followers));
            }
        }

        private readonly IArtistManager _artistManager;

        public ICommand AddArtistCommand { get; }
        public ICommand DeleteArtistCommand { get; }

        public ArtistViewModel(IArtistManager artistManager)
        {
            _artistManager = artistManager;
            Artist = new List<Artist>();
            LoadArtist();
            AddArtistCommand = new RelayCommand(AddArtistExecute, CanAddArtistExecute);
            DeleteArtistCommand = new RelayCommand(DeleteArtistExecute, CanDeleteArtistExecute);
        }

        private async void AddArtistExecute(object parameter)
        {
            var temp = new Artist();
            temp.Name = Name;
            temp.Followers = Followers;
            temp.Popularity = Popularity;
            temp.Name_Genre = NameGenre;
            await _artistManager.UnitOfWork.ArtistRepository.AddAsync(temp);
            await _artistManager.UnitOfWork.SaveChangesAsync();

            Name = "";
            NameGenre = "";
            Popularity = "";
            Followers = "";
            await LoadArtist();
        }

        private bool CanAddArtistExecute(object parameter)
        {
            return true;
        }

        private async void DeleteArtistExecute(object parameter)
        {
            if (parameter is Artist artist)
            {
                await _artistManager.UnitOfWork.ArtistRepository.DeleteAsync(artist);
                await _artistManager.UnitOfWork.SaveChangesAsync();
                Artist.Remove(artist);
                await LoadArtist();
            }
        }

        private bool CanDeleteArtistExecute(object parameter)
        {
            return true; 
        }

        public async Task LoadArtist()
        {
            var result = await _artistManager.UnitOfWork.ArtistRepository.GetAllAsync();

            Artist = result.ToList();

            OnPropertyChanged(nameof(Artist));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}