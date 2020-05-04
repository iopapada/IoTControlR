using IoTControlR.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTControlR.ViewModels
{
    public class MainShellViewModel : ShellViewModel
    {
        private readonly NavigationItem DashboardItem = new NavigationItem(0xE80F, "Dashboard", typeof(DashboardViewModel));
        //private readonly NavigationItem CustomersItem = new NavigationItem(0xE716, "Customers", typeof());
        //private readonly NavigationItem OrdersItem = new NavigationItem(0xE8A1, "Orders", typeof());
        //private readonly NavigationItem ProductsItem = new NavigationItem(0xE781, "Products", typeof());
        private readonly NavigationItem AppLogsItem = new NavigationItem(0xE7BA, "Activity Log", typeof(AppLogsViewModel));
        private readonly NavigationItem SettingsItem = new NavigationItem(0x0000, "Settings", typeof(SettingsViewModel));

        public MainShellViewModel(ILoginService loginService, ICommonServices commonServices) : base(loginService, commonServices)
        {
        }
        private object _selectedItem;
        public object SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        private bool _isPaneOpen = true;
        public bool IsPaneOpen
        {
            get => _isPaneOpen;
            set => Set(ref _isPaneOpen, value);
        }

        private IEnumerable<NavigationItem> _items;
        public IEnumerable<NavigationItem> Items
        {
            get => _items;
            set => Set(ref _items, value);
        }

        public override async Task LoadAsync(ShellArgs args)
        {
            Items = GetItems().ToArray();
            await UpdateAppLogBadge();
            await base.LoadAsync(args);
        }
        public void NavigateTo(Type viewModel)
        {
            switch (viewModel.Name)
            {
                case "DashboardViewModel":
                    NavigationService.Navigate(viewModel);
                    break;
                case "AutomationsViewModel":
                    //NavigationService.Navigate(viewModel, new AutomationListArgs);
                    break;
                case "OrganizationViewModel":
                    //NavigationService.Navigate(viewModel, new OgranizationListArgs());
                    break;
                case "SecurityViewModel":
                    //NavigationService.Navigate(viewModel, new SecurityListArgs());
                    break;
                case "AppLogsViewModel":
                    NavigationService.Navigate(viewModel, new AppLogListArgs());
                    //await LogService.MarkAllAsReadAsync();
                    //await UpdateAppLogBadge();
                    break;
                case "SettingsViewModel":
                    NavigationService.Navigate(viewModel, new SettingsArgs());
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        private IEnumerable<NavigationItem> GetItems()
        {
            yield return DashboardItem;
            //yield return CustomersItem;
            //yield return OrdersItem;
            //yield return ProductsItem;
            yield return AppLogsItem;
        }
        private async Task UpdateAppLogBadge()
        {
            //int count = await LogService.GetLogsCountAsync(new DataRequest<AppLog> { Where = r => !r.IsRead });
            //AppLogsItem.Badge = count > 0 ? count.ToString() : null;
        }
    }
}
