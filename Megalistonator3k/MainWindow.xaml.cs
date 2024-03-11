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
            readFolders("savedFolders.txt");
            readTitles("savedTitles.txt");

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addToFolderList(object sender, RoutedEventArgs e)
        {
            titleFolders titleFolders = new titleFolders(folderName.Text);
            titleFolders.addFolder(folderName.Text, foldersList, titleFoldersList, titleFolders);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (foldersList.SelectedItem != null && (!string.IsNullOrEmpty(addName.Text) || !string.IsNullOrEmpty(addDescr.Text)))
            {

                foreach (var folder in titleFoldersList)
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
            foreach (var folder in titleFoldersList)
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
                    swTitles.WriteLine(titlefolder.Name);
                    foreach (var title in titlefolder.includedTitles)
                    {
                        swTitles.WriteLine($"{title.Title}/{title.Description}");
                    }
                    swTitles.Flush();
                }
            }
        }
        private void readFolders(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        titleFolders titleFolder = new titleFolders(line);
                        titleFoldersList.Add(titleFolder);
                        foldersList.Items.Add(titleFolder.Name);
                    }
                }
            }
        }

        private void AddTitleToFolder(titleList title, string folderName)
        {
            foreach (var folder in titleFoldersList)
            {
                if (folder.Name == folderName)
                {
                    titleList title1 = new titleList(title.Title, title.Description);
                    folder.includedTitles.Add(title1);
                }
            }
        }
        private void readTitles(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    titleFolders currentFolder = null;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('/');

                        if (parts.Length == 1)
                        {
                            currentFolder = titleFoldersList.FirstOrDefault(folder => folder.Name == parts[0]);
                        }
                        else if (parts.Length == 2 && currentFolder != null)
                        {
                            string titleName = parts[0];
                            string description = parts[1];
                            titleList newTitle = new titleList(titleName, description);
                            currentFolder.includedTitles.Add(newTitle);
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
                titleListWin.Items.Clear();
                foreach (var folder in titleFoldersList)
                {
                    if (folder.Name == selectedFolder)
                    {
                        for (int i = folder.includedTitles.Count - 1; i >= 0; i--)
                        {
                            var title = folder.includedTitles[i];
                            
                                folder.includedTitles.RemoveAt(i);
                                break;
                            
                        }
                    }
                }

            }

        }

        private void Change_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void removeTitle(string title)
        {
            foreach (var folder in titleFoldersList)
            {
                if (folder.Name == foldersList.SelectedItem.ToString())
                {
                    var titlesToRemove = folder.includedTitles.Where(t => t.Title + " " + t.Description == title).ToList();
                    foreach (var titleToRemove in titlesToRemove)
                    {
                        folder.includedTitles.Remove(titleToRemove);
                    }

                    titleListWin.Items.Clear();
                    for (int i = folder.includedTitles.Count - 1; i >= 0; i--)
                    {
                        titleListWin.Items.Add($"{folder.includedTitles[i].Title} {folder.includedTitles[i].Description}");
                    }

                    // Exit the loop after removing the title
                    break;
                }
            }
        }


        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (titleListWin.SelectedItem != null)
            {
                removeTitle(titleListWin.SelectedItem.ToString());
            }
        }
    }
}