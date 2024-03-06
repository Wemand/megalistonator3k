using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Megalistonator3k
{
    public partial class MainWindow : Window
    {
        private List<titleFolders> titleFoldersList;

        public MainWindow()
        {
            InitializeComponent();

            titleFoldersList = new List<titleFolders>();
            

            Closing += Window_Closing;
            readSaves("savedFolders.txt");
            readSaves("savedTitles.txt");

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
                titleList titleList1 = new titleList(" ", " ");
                titleFolders.includedTitles.Add(titleList1);
                folderName.Text = null;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (foldersList.SelectedItem != null)
            {foreach (var folder in titleFoldersList)
                {
                    if (folder.Name == foldersList.SelectedItem.ToString())
                    {
                        titleListWin.Items.Add($"{addName.Text} {addDescr.Text}");
                        titleList titleList1 = new titleList(addName.Text, addDescr.Text);
                        folder.includedTitles.Add(titleList1);
                        addDescr.Text = null;
                        addName.Text = null;
                    }
                }
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
            foreach(var folder in titleFoldersList)
            {
                if (folder.Name == folderName)
                {
                    foreach (var titlelist in folder.includedTitles)
                    {
                        titleListWin.Items.Add($"{titlelist.Title} {titlelist.Description}");
                    }
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
            using (StreamWriter swFolders = new StreamWriter("savedFolders.txt"))
            {
                foreach (var titlefolder in titleFoldersList)
                    {
                        swFolders.WriteLine($"{titlefolder.Name}");
                    }
                    swFolders.Flush();
            }
            using (StreamWriter swTitles = new StreamWriter("savedTitles.txt"))
            {
                foreach (var titlefolder in titleFoldersList)
                {
                    foreach (var title in titlefolder.includedTitles)
                    {
                        swTitles.WriteLine($"{title.Title}/{title.Description}");
                    }
                    swTitles.Flush();
                }
            }
        }
        private void readSaves(string fileName)
        {
            bool isInFolferList = false;
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        
                        string[] parts = line.Split('/');
                        if (parts[0] != " " && parts[1] != " ") // эта строчка тоже до введения порядковых номеров
                        {
                            if (parts.Length == 1)
                            {
                                titleFolders titleFolder = new titleFolders(parts[0]);
                                titleFoldersList.Add(titleFolder);
                                foldersList.Items.Add(titleFolder.Name);
                            }
                            //else if (parts.Length == 2)
                            //{
                            //    titleFolders titleFolder = new titleFolders(parts[1]);
                            //    titleFoldersList.Add(titleFolder);
                            //    foldersList.Items.Add(parts[1]);
                            //    titleList title = new titleList(parts[0], parts[1], " ");
                            //    titleListAdd.Add(title);

                            //}
                            else if (parts.Length == 3)
                            {
                                foreach (var folder in foldersList.Items)
                                {
                                    if (folder.ToString() == parts[2]) 
                                    {
                                        isInFolferList = true;
                                    }
                                }
                                if (!isInFolferList)
                                {
                                    titleFolders titleFolder = new titleFolders(parts[2]);
                                    titleFoldersList.Add(titleFolder);
                                    foldersList.Items.Add(titleFolder.Name);
                                }
                                else { break; }
                                titleList title = new titleList(parts[0], parts[1]);
                                titleListAdd.Add(title);

                            }
                        }
                        else
                        {
                            continue;
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
                    foreach (var title in folder.includedTitles)
                    {
                        if (title.Folder == foldersList.SelectedItem.ToString())
                        {
                            title.Folder = changeText.Text;
                        }
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