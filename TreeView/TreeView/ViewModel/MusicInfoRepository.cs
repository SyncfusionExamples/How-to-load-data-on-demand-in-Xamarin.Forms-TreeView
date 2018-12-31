using Syncfusion.TreeView.Engine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TreeView
{
public class MusicInfoRepository
{
    private ObservableCollection<MusicInfo> menu;

    public ObservableCollection<MusicInfo> Menu
    {
        get { return menu; }
        set { menu = value; }
    }

    public ICommand TreeViewOnDemandCommand
    {
        get; set;
    }

    public MusicInfoRepository()
    {
        this.Menu = this.GetMenuItems();
        TreeViewOnDemandCommand = new Command(ExecuteOnDemandLoading, CanExecuteOnDemandLoading);
    }

    // CanExecute method is called when each item is initialized and to define whether load-on-demand feature is enabled for this item.
    // Write here the conditions to decide the execution of load on demand and visibility of expander icon. 
    private bool CanExecuteOnDemandLoading(object sender)
    {
        var hasChildNodes = ((sender as TreeViewNode).Content as MusicInfo).HasChildNodes;
        if (hasChildNodes)
            return true;
        else
            return false;
    }

    //Execute method is called when any item is requested for load-on-demand items.
    private void ExecuteOnDemandLoading(object obj)
    {
        var node = obj as TreeViewNode;
        if(node.ChildNodes.Count > 0)
            return;

        node.ShowExpanderAnimation = true;
        MusicInfo musicInfo = node.Content as MusicInfo;
        Device.BeginInvokeOnMainThread(async () =>
        {
            await Task.Delay(2000);
            var items = GetSubMenu(musicInfo.ID);
            node.PopulateChildNodes(items);
            if (items.Count() > 0)
                node.IsExpanded = true;
            node.ShowExpanderAnimation = false;
        });
    }

    private ObservableCollection<MusicInfo> GetMenuItems()
    {
        ObservableCollection<MusicInfo> menuItems = new ObservableCollection<MusicInfo>();
        menuItems.Add(new MusicInfo() { ItemName = "Discover Music", HasChildNodes = true, ID = 1 });
        menuItems.Add(new MusicInfo() { ItemName = "Sales and Events", HasChildNodes = true, ID = 2 });
        menuItems.Add(new MusicInfo() { ItemName = "Categories", HasChildNodes = true, ID = 3 });
        menuItems.Add(new MusicInfo() { ItemName = "MP3 Albums", HasChildNodes = true, ID = 4 });
        menuItems.Add(new MusicInfo() { ItemName = "Fiction Book Lists", HasChildNodes = true, ID = 5 });
        return menuItems;
    }

    public IEnumerable<MusicInfo> GetSubMenu(int iD)
    {
        ObservableCollection<MusicInfo> menuItems = new ObservableCollection<MusicInfo>();
        if (iD == 1)
        {
            menuItems.Add(new MusicInfo() { ItemName = "Hot Singles", HasChildNodes = false, ID = 11 });
            menuItems.Add(new MusicInfo() { ItemName = "Rising Artists", HasChildNodes = false, ID = 12 });
            menuItems.Add(new MusicInfo() { ItemName = "Live Music", HasChildNodes = false, ID = 13 });
            menuItems.Add(new MusicInfo() { ItemName = "More in Music", HasChildNodes = true, ID = 14 });
        }
        else if (iD == 2)
        {
            menuItems.Add(new MusicInfo() { ItemName = "100 Albums - $10 Each", HasChildNodes = false, ID = 21 });
            menuItems.Add(new MusicInfo() { ItemName = "Hip-Hop and R&B Sale", HasChildNodes = false, ID = 22 });
            menuItems.Add(new MusicInfo() { ItemName = "CD Deals", HasChildNodes = false, ID = 23 });
        }
        else if (iD == 3)
        {
            menuItems.Add(new MusicInfo() { ItemName = "Songs", HasChildNodes = false, ID = 31 });
            menuItems.Add(new MusicInfo() { ItemName = "Bestselling Albums", HasChildNodes = false, ID = 32 });
            menuItems.Add(new MusicInfo() { ItemName = "New Releases", HasChildNodes = false, ID = 33 });
            menuItems.Add(new MusicInfo() { ItemName = "MP3 Albums", HasChildNodes = false, ID = 34 });

        }
        else if (iD == 4)
        {
            menuItems.Add(new MusicInfo() { ItemName = "Rock Music", HasChildNodes = false, ID = 41 });
            menuItems.Add(new MusicInfo() { ItemName = "Gospel", HasChildNodes = false, ID = 42 });
            menuItems.Add(new MusicInfo() { ItemName = "Latin Music", HasChildNodes = false, ID = 43 });
            menuItems.Add(new MusicInfo() { ItemName = "Jazz", HasChildNodes = false, ID = 44 });
        }
        else if (iD == 5)
        {
            menuItems.Add(new MusicInfo() { ItemName = "Hunger Games", HasChildNodes = false, ID = 51 });
            menuItems.Add(new MusicInfo() { ItemName = "Pride and Prejudice", HasChildNodes = false, ID = 52 });
            menuItems.Add(new MusicInfo() { ItemName = "Harry Potter", HasChildNodes = false, ID = 53 });
            menuItems.Add(new MusicInfo() { ItemName = "Game Of Thrones", HasChildNodes = false, ID = 54 });
        }
        else if (iD == 14)
        {
            menuItems.Add(new MusicInfo() { ItemName = "Music Trade-In", HasChildNodes = false, ID = 141 });
            menuItems.Add(new MusicInfo() { ItemName = "Redeem a Gift card", HasChildNodes = false, ID = 142 });
            menuItems.Add(new MusicInfo() { ItemName = "Band T-Shirts", HasChildNodes = false, ID = 143 });
        }
        return menuItems;
    }
}
{% endhighlight %}
{% endtabs %}}
