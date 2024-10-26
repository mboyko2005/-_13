
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ПЗ_13.Commands;
using ПЗ_13.MementoPattern;
using ПЗ_13.Models;

namespace ПЗ_13.ViewModels
{
	public class UserProfileViewModel : INotifyPropertyChanged
	{
		private readonly UserProfile currentProfile;
		private readonly Caretaker caretaker;

		private string name;
		private string email;
		private string settings;

		public event PropertyChangedEventHandler PropertyChanged;

		public string Name
		{
			get => name;
			set
			{
				if (name != value)
				{
					SaveState();
					name = value;
					OnPropertyChanged();
				}
			}
		}

		public string Email
		{
			get => email;
			set
			{
				if (email != value)
				{
					SaveState();
					email = value;
					OnPropertyChanged();
				}
			}
		}

		public string Settings
		{
			get => settings;
			set
			{
				if (settings != value)
				{
					SaveState();
					settings = value;
					OnPropertyChanged();
				}
			}
		}

		public ICommand UndoCommand { get; }
		public ICommand RedoCommand { get; }

		public bool CanUndo => caretaker.CanUndo;
		public bool CanRedo => caretaker.CanRedo;

		public UserProfileViewModel()
		{
			currentProfile = new UserProfile
			{
				Name = "Иван Иванов",
				Email = "ivan@example.com",
				Settings = "Default"
			};
			caretaker = new Caretaker();

			UndoCommand = new RelayCommand(ExecuteUndo, () => CanUndo);
			RedoCommand = new RelayCommand(ExecuteRedo, () => CanRedo);

			// Инициализация профиля в UI
			LoadProfile(currentProfile);
		}

		private void LoadProfile(UserProfile profile)
		{
			name = profile.Name;
			email = profile.Email;
			settings = profile.Settings;
			OnPropertyChanged(nameof(Name));
			OnPropertyChanged(nameof(Email));
			OnPropertyChanged(nameof(Settings));
		}

		private void SaveProfile(UserProfile profile)
		{
			profile.Name = this.Name;
			profile.Email = this.Email;
			profile.Settings = this.Settings;
		}

		private void SaveState()
		{
			SaveProfile(currentProfile);
			caretaker.Save(new Memento(currentProfile.Clone()));
			OnPropertyChanged(nameof(CanUndo));
			OnPropertyChanged(nameof(CanRedo));
			// Обновляем состояния команд
			(UndoCommand as RelayCommand)?.RaiseCanExecuteChanged();
			(RedoCommand as RelayCommand)?.RaiseCanExecuteChanged();
		}

		private void ExecuteUndo()
		{
			var memento = caretaker.Undo();
			if (memento != null)
			{
				LoadProfile(memento.State);
				OnPropertyChanged(nameof(CanUndo));
				OnPropertyChanged(nameof(CanRedo));
				// Обновляем состояния команд
				(UndoCommand as RelayCommand)?.RaiseCanExecuteChanged();
				(RedoCommand as RelayCommand)?.RaiseCanExecuteChanged();
			}
		}

		private void ExecuteRedo()
		{
			var memento = caretaker.Redo();
			if (memento != null)
			{
				LoadProfile(memento.State);
				OnPropertyChanged(nameof(CanUndo));
				OnPropertyChanged(nameof(CanRedo));
				// Обновляем состояния команд
				(UndoCommand as RelayCommand)?.RaiseCanExecuteChanged();
				(RedoCommand as RelayCommand)?.RaiseCanExecuteChanged();
			}
		}

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
