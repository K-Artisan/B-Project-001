using ControlzEx.Standard;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zo.Xapp.Wpf.Mvvm;
using Zo.Xapp.Wpf.Views;

namespace Zo.Xapp.Wpf.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private ObservableCollection<MenuItemViewModel> _menuItems;
        private ObservableCollection<MenuItemViewModel> _menuOptionItems;

        public MainViewModel()
        {
            this.CreateMenuItems();
        }

        public void CreateMenuItems()
        {
            //顶部菜单项
            MenuItems = new ObservableCollection<MenuItemViewModel>
            {
                new HomeViewModel(this)
                {
                    Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.Home},
                    Label = "首页",
                    ToolTip = "首页"
                },
               new ShootMonitorViewModel(this)
                {
                    Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.AccessPoint},
                    Label = "射击监控",
                    ToolTip = "射击监控"
                },

                new SerialPortDebugViewModel(this)
                {
                    Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.TestTube},
                    Label = "串口调试",
                    ToolTip = "串口调试"
                },

                new PrivateViewModel(this)
                {
                    Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.AccountCircle},
                    Label = "个人中心",
                    ToolTip = "个人中心"
                },

                new AboutViewModel(this)
                {
                    Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.Help},
                    Label = "关于",
                    ToolTip = "关于"
                }
            };

            //沉底的菜单项
            MenuOptionItems = new ObservableCollection<MenuItemViewModel>
            {
                new SettingsViewModel(this)
                {
                    Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.Cog},
                    Label = "设置",
                    ToolTip = "设置"
                }
            };
        }

        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public ObservableCollection<MenuItemViewModel> MenuOptionItems
        {
            get => _menuOptionItems;
            set => SetProperty(ref _menuOptionItems, value);
        }
    }
}
