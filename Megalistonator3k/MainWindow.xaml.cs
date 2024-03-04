using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Megalistonator3k
{
    public partial class MainWindow : Window
    {
        private List<titleFolders> titleFoldersList;
        private List<titleList> titleListAdd;

        public MainWindow()
        {
            InitializeComponent();

            titleFoldersList = new List<titleFolders>();
            titleListAdd = new List<titleList>();

            Closing += Window_Closing;
            readSaves("savedData.txt");
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(folderName.Text))
            {
                foldersList.Items.Add(folderName.Text);
                titleFolders titleFolders = new titleFolders(addName.Text);
                titleFoldersList.Add(titleFolders);
                folderName.Text = null;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (foldersList.SelectedItem != null)
            {
                titleListWin.Items.Add($"{addName.Text} {addDescr.Text}");
                string a = foldersList.SelectedItem.ToString();
                titleList titleList1 = new titleList(addName.Text, addDescr.Text, a);
                titleListAdd.Add(titleList1);
                addDescr.Text = null;
                addName.Text = null;
            }
            else
            {
                
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if(folderName.Text == null) 
            //{
            //    addFolder.IsEnabled = false;
            //}
        }
        private void toSelect(string folderName)
        {

            titleListWin.Items.Clear();
            foreach (var titlelist in titleListAdd)
            {
                if (titlelist.Folder == folderName)
                {
                    titleListWin.Items.Add($"{titlelist.Title} {titlelist.Description}");
                }
            }
        }
        private void ListofFolders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (foldersList.SelectedItem != null)
            {
                change.IsEnabled = true;
                string selectedFolder = foldersList.SelectedItem.ToString();
                toSelect(selectedFolder);
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Сохранение данных в текстовый файл
            using (StreamWriter sw = new StreamWriter("savedData.txt"))
            {
                foreach (var title in titleListAdd)
                {
                    sw.WriteLine($"{title.Title}/{title.Description}/{title.Folder}");
                }
                sw.Flush();
            }
        }
        private void readSaves(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('/');
                        if (parts.Length == 1)
                        {
                            titleFolders titleFolder = new titleFolders(parts[0]);
                            titleFoldersList.Add(titleFolder);
                            foldersList.Items.Add(parts[0]);
                        }
                        else if (parts.Length == 3)
                        {
                            titleFolders titleFolder = new titleFolders(parts[2]);
                            titleFoldersList.Add(titleFolder);
                            foldersList.Items.Add(parts[2]);
                            titleList title = new titleList(parts[0], parts[1], parts[2]);
                            titleListAdd.Add(title);

                        }
                    }
                }
            }

        }
        
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty((string?)foldersList.SelectedItem) && !string.IsNullOrEmpty(changeText.Text))
            {
                foreach (var folder in titleFoldersList)
                {
                    if (foldersList.SelectedItem == null) { continue; }
                    if (folder.Name == foldersList.SelectedItem.ToString())
                    {
                        folder.Name = changeText.Text;
                    }
                }
                foreach (var title in titleListAdd)
                {
                    if (title.Folder == foldersList.SelectedItem.ToString())
                    {
                        title.Folder = changeText.Text;
                    }
                }

                int selectedIndex = foldersList.SelectedIndex;
                foldersList.Items[selectedIndex] = changeText.Text;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty((string?)foldersList.SelectedItem))
            {
                string selectedFolder = foldersList.SelectedItem.ToString();

                // Найдите объект titleFolders в списке и удалите его
                var folderToRemove = titleFoldersList.FirstOrDefault(folder => folder.Name == selectedFolder);
                titleFoldersList.Remove(folderToRemove);


                // Удалите выбранную папку из списка foldersList
                foldersList.Items.Remove(selectedFolder);
                for (int i = titleListAdd.Count - 1; i >= 0; i--)
                {
                    var title = titleListAdd[i];
                    if (title.Folder == selectedFolder)
                    {
                        titleListWin.Items.Clear();
                        titleListAdd.RemoveAt(i);
                        break;
                    }
                }
            }

        }

        private void Change_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}