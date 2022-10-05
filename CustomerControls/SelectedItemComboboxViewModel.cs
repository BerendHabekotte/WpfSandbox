using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerControls
{
    public class SelectedItemComboboxViewModel : INotifyPropertyChanged
    {
        public List<Tuple<string, string, string>> AvailableWords = new List<Tuple<string, string, string>>();

        public List<string> EasyWords = new List<string>();

        private Tuple<string, string, string> selectedCurrentWord;
        public Tuple<string, string, string> SelectedCurrentWord
        {
            get { return selectedCurrentWord; }
            set
            {
                selectedCurrentWord = value;
                OnPropertyChanged("SelectedCurrentWord");
            }
        }

        private string selectedEasyWord;
        public string SelectedEasyWord
        {
            get => selectedEasyWord;
            set
            {
                selectedEasyWord = value;
                OnPropertyChanged("SelectedEasyWord");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public SelectedItemComboboxViewModel()
        {
            LoadAvailableWords();
            LoadEasyWords();
        }

        private void LoadAvailableWords()
        {
            AvailableWords.Add(new Tuple<string, string, string>("A1", "A2", "A3"));
            AvailableWords.Add(new Tuple<string, string, string>("B1", "B2", "B3"));
            AvailableWords.Add(new Tuple<string, string, string>("C1", "C2", "C3"));
            AvailableWords.Add(new Tuple<string, string, string>("D1", "D2", "D3"));
            AvailableWords.Add(new Tuple<string, string, string>("E1", "E2", "E3"));
            AvailableWords.Add(new Tuple<string, string, string>("F1", "F2", "F3"));
        }

        private void LoadEasyWords()
        {
            EasyWords.Add("A");
            EasyWords.Add("B");
            EasyWords.Add("C");
            EasyWords.Add("D");
            EasyWords.Add("E");
            EasyWords.Add("F");
            EasyWords.Add("G");
            EasyWords.Add("H");
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
