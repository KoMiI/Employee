using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Employee.DataBase;

namespace Employee.PersonalCard
{
    public partial class ChooseForm : Window
    {
        List<Dictionary<int, string>> cards;
        List<InitCard> initcards;

        class InitCard
        {
            public DateTime DatePreparation { get; set; }                   // Дата составления
            public string TablelNumber { get; set; }                        // Табельный номер
            public string INN { get; set; }                                 // ИНН
            public string FIO { get; set; }                                 // ФИО сотрудника
            public string Gender { get; set; }                              // Пол сотрудника
            public string PassportNumner { get; set; }                      // Номер паспорта
            public string PassportSerial { get; set; }                      // Серия паспорта

            public InitCard(Dictionary<int, string> _card)
            {
                DatePreparation = new DateTime(
                       Int32.Parse(_card[16].Substring(6, 4)),
                       Int32.Parse(_card[16].Substring(3, 2)),
                       Int32.Parse(_card[16].Substring(0, 2)));
                TablelNumber = _card[6];
                INN = _card[5];
                FIO = _card[2] + " " + _card[3] + " " + _card[4];

                if (_card[8][0] == 'М')
                    Gender = "Мужской";
                else if (_card[8][0] == 'Ж')
                    Gender = "Женский";
                else if (_card[8][0] == 'И')
                    Gender = "Интерсекс";

                PassportNumner = _card[12];
                PassportSerial = _card[11];
            }
        }


        public ChooseForm()
        {
            InitializeComponent();

            // соеденяемся с БД
            PersonalCard_dbRouteen dbRouteen = new PersonalCard_dbRouteen(DataBase.dbConnect.StartConnection());
            cards = dbRouteen.GetPersonalCarAll();
            initcards = new List<InitCard>();

            for(int i = 0; i < cards.Count; i++)
            {
                initcards.Add(new InitCard(cards[i]));
            }

            dataGrid.ItemsSource = initcards;
        }
    }
}
