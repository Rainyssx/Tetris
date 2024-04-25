using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace tetris
{
    
        public class User : INotifyPropertyChanged
        {
            private string _nick = "";
            private string _name = "";
            private DateTime _birth = DateTime.Now.AddYears(-18);

            public string Nick
            {
                get => _nick;
                set => SetField(ref _nick, value);
            }

            public string Name
            {
                get => _name;
                set => SetField(ref _name, value);
            }

            public DateTime Birth
            {
                get => _birth;
                set => SetField(ref _birth, value);
            }

            [JsonIgnore]
            public bool IsValid => Nick.Trim().Length > 0
                                      && Name.Trim().Length > 0
                                      && Birth.Date >= DateTime.Now.Date.AddYears(-120)
                                      && Birth.Date <= DateTime.Now.Date.AddYears(-6);

            public void UpdateInfoFrom(User user)
            {
                Nick = user.Nick;
                Name = user.Name;
                Birth = user.Birth;
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
            {
                if (EqualityComparer<T>.Default.Equals(field, value)) return false;
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }

            public User Clone()
            {
                var u = new User();
                u.UpdateInfoFrom(this);
                return u;
            }
        }
    }

