using System;
using System.Collections.Generic;
using System.Data;
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
        PersonalCard.PersonalCard_RW personal_card;

        List<InitCard> initcardsFilter;
        List<InitCard> initcardsFilterPreparation;
        List<InitCard> initcardsFilterTable;
        List<InitCard> initcardsFilterFIO;
        List<InitCard> initcardsFilterGender;
        List<InitCard> initcardsFilterINN;

        class InitCard
        {
            public string CardId { get; set; }                              // ID
            public DateTime DatePreparation { get; set; }                   // Дата составления
            public string TablelNumber { get; set; }                        // Табельный номер
            public string INN { get; set; }                                 // ИНН
            public string FIO { get; set; }                                 // ФИО сотрудника
            public string Gender { get; set; }                              // Пол сотрудника

            /*Конструктор*/
            public InitCard(Dictionary<int, string> _card)
            {
                CardId = _card[1];
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
            }
        }

        /*Конструктор*/
        public ChooseForm(PersonalCard.PersonalCard_RW pc)
        {
            InitializeComponent();
            personal_card = pc;
            GenderCB.SelectedItem = "Любой";

            // соеденяемся с БД
            PersonalCard_dbRouteen dbRouteen = new PersonalCard_dbRouteen(DataBase.dbConnect.StartConnection());
            cards = dbRouteen.GetPersonalCarAll();
            initcards = new List<InitCard>();

            initcardsFilter = new List<InitCard>();

            initcardsFilterPreparation = new List<InitCard>();
            initcardsFilterTable = new List<InitCard>();
            initcardsFilterFIO = new List<InitCard>();
            initcardsFilterGender = new List<InitCard>();
            initcardsFilterINN = new List<InitCard>();

            for (int i = 0; i < cards.Count; i++)
            {
                initcards.Add(new InitCard(cards[i]));
            }

            initcardsFilterPreparation.AddRange(initcards);
            initcardsFilterTable.AddRange(initcards);
            initcardsFilterFIO.AddRange(initcards);
            initcardsFilterGender.AddRange(initcards);
            initcardsFilterINN.AddRange(initcards);


            dataGrid.ItemsSource = initcards;
        }

        /*Создание бокса пол*/
        private void GenderCB_Loaded(object sender, RoutedEventArgs e)
        {
            GenderCB.Items.Add("Любой");
            GenderCB.Items.Add("Мужской");
            GenderCB.Items.Add("Женский");
            GenderCB.Items.Add("Интерсекс");
        }

        /*Выбор карты*/
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItems.Count > 0)
                {
                    personal_card.CreateFullCard(initcards[dataGrid.SelectedIndex].CardId);
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception)
            {
                this.Close();
            }
        }

        /*Все фильтры в один*/
        private void ComboFilter()
        {
            List<bool> flag = new List<bool>();
            List<InitCard> filter;
            int i;

            initcardsFilter.Clear();

            // дата
            foreach (InitCard card1 in initcards)
            {
                foreach (InitCard card2 in initcardsFilterPreparation)
                {
                    if (card1.CardId == card2.CardId)
                        initcardsFilter.Add(card1);
                }
            }

            // табельный номер
            flag.Clear();
            foreach (InitCard card1 in initcards)
                flag.Add(false);

            i = 0;

            foreach (InitCard card1 in initcardsFilter)
            {   
                foreach (InitCard card2 in initcardsFilterTable)
                {
                    if (card1.CardId == card2.CardId)
                        flag[i] = true;
                }              
                i++;
            }
            i = 0;

            filter = new List<InitCard>();
            foreach (InitCard card1 in initcardsFilter)
            {
                if (flag[i])
                    filter.Add(card1);
                i++;
 
            }
            i = 0;

            initcardsFilter.Clear();
            initcardsFilter = filter;

            // ФИО
            flag.Clear();
            foreach (InitCard card1 in initcards)
                flag.Add(false);

            i = 0;

            foreach (InitCard card1 in initcardsFilter)
            {
                foreach (InitCard card2 in initcardsFilterFIO)
                {
                    if (card1.CardId == card2.CardId)
                        flag[i] = true;
                }
                i++;
            }
            i = 0;

            filter = new List<InitCard>();
            foreach (InitCard card1 in initcardsFilter)
            {
                if (flag[i])
                    filter.Add(card1);
                i++;

            }
            i = 0;


            initcardsFilter.Clear();
            initcardsFilter = filter;

            // ПОЛ
            flag.Clear();
            foreach (InitCard card1 in initcards)
                flag.Add(false);

            i = 0;

            foreach (InitCard card1 in initcardsFilter)
            {
                foreach (InitCard card2 in initcardsFilterGender)
                {
                    if (card1.CardId == card2.CardId)
                        flag[i] = true;
                }
                i++;
            }
            i = 0;

            filter = new List<InitCard>();
            foreach (InitCard card1 in initcardsFilter)
            {
                if (flag[i])
                    filter.Add(card1);
                i++;

            }
            i = 0;

            initcardsFilter.Clear();
            initcardsFilter = filter;

            // ИНН
            flag.Clear();
            foreach (InitCard card1 in initcards)
                flag.Add(false);

            i = 0;

            foreach (InitCard card1 in initcardsFilter)
            {
                foreach (InitCard card2 in initcardsFilterINN)
                {
                    if (card1.CardId == card2.CardId)
                        flag[i] = true;
                }
                i++;
            }
            i = 0;

            filter = new List<InitCard>();
            foreach (InitCard card1 in initcardsFilter)
            {
                if (flag[i])
                    filter.Add(card1);
                i++;

            }
            i = 0;

            initcardsFilter.Clear();
            initcardsFilter = filter;

            dataGrid.ItemsSource = initcardsFilter.ToList();
        }


        private void DatePreparationDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            initcardsFilterPreparation.Clear();

            if (DatePreparationDP.SelectedDate == null)
                initcardsFilterPreparation.AddRange(initcards);
            else
                foreach (InitCard card in initcards)
                    if (card.DatePreparation == DatePreparationDP.SelectedDate)
                        initcardsFilterPreparation.Add(card);
    
            ComboFilter();
        }

        private void TablelNumberTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            initcardsFilterTable.Clear();

            if (TablelNumberTB.Text.Equals(""))
                initcardsFilterTable.AddRange(initcards);
            else
                foreach (InitCard card in initcards)
                    if (card.TablelNumber.Contains(TablelNumberTB.Text))
                        initcardsFilterTable.Add(card);

            ComboFilter();
        }

        private void FIO_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            initcardsFilterFIO.Clear();

            if (FIO_TB.Text.Equals(""))
                initcardsFilterFIO.AddRange(initcards);
            else
                foreach (InitCard card in initcards)
                    if (card.FIO.Contains(FIO_TB.Text))
                        initcardsFilterFIO.Add(card);

            ComboFilter();
        }

        private void GenderCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            initcardsFilterGender.Clear();

            if (GenderCB.SelectedItem.Equals("Любой"))
                initcardsFilterGender.AddRange(initcards);
            else
                foreach (InitCard card in initcards)
                    if (card.Gender.Equals(GenderCB.SelectedItem))
                        initcardsFilterGender.Add(card);

            ComboFilter();
        }

        private void INN_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            initcardsFilterINN.Clear();

            if (INN_TB.Text.Equals(""))
                initcardsFilterINN.AddRange(initcards);
            else
                foreach (InitCard card in initcards)
                    if (card.INN.Contains(INN_TB.Text))
                        initcardsFilterINN.Add(card);

            ComboFilter();
        }

    }
}
